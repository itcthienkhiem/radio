#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Diagnostics;

namespace ClearCanvas.Common
{
	public sealed class FatalExceptionHandlerExtensionPoint : ExtensionPoint<IFatalExceptionHandler>
	{}

	public interface IFatalExceptionHandler
	{
		bool Handle(Exception exception);
	}

	public abstract class FatalExceptionHandler : IFatalExceptionHandler
	{
		private static readonly object _syncLock = new object();
		private static IFatalExceptionHandler _handler = Create();

		#region IFatalExceptionHandler Members

		public abstract bool Handle(Exception exception);

		#endregion

		private static IFatalExceptionHandler Create()
		{
			try
			{
				return (IFatalExceptionHandler)new FatalExceptionHandlerExtensionPoint().CreateExtension();
			}
			catch (NotSupportedException)
			{
			}

			return null;
		}

		private static void Log(Exception e)
		{
			const string message = "Fatal exception occurred in application.";
			Platform.Log(LogLevel.Fatal, e, message);
			Console.WriteLine(message);
			Console.WriteLine(e.Message);
		}

		private static void OnFatalException(Exception e)
		{
			IFatalExceptionHandler handler;
			lock (_syncLock)
			{
				//Call the real handler exactly once; all subsequent exceptions are just logged.
				handler = _handler;
				_handler = null;
			}

			if (handler == null)
			{
				Log(e);
				if (Debugger.IsAttached)
					Environment.Exit(-1);
				
				return;
			}

			bool handled = false;
			try
			{
				handled = handler.Handle(e);
			}
			catch (Exception ex)
			{
				Platform.Log(LogLevel.Error, ex);
			}

			if (!handled)
				Log(e);

			bool suppressSillyCrashDialog = handled;
			//Whether or not to skip showing the silly crash dialog is based
			//on whether or not the very first fatal exception was "handled".
			if (suppressSillyCrashDialog)
				Environment.Exit(-1);
		}

		internal static void Initialize()
		{
			AppDomain.CurrentDomain.UnhandledException += 
				(sender, e) => OnFatalException((Exception)e.ExceptionObject);
		}
	}
}
