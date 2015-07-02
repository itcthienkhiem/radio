#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Reflection;

namespace ClearCanvas.Common
{
	/// <summary>
	/// Conveys information about plugins as they are loaded.
	/// </summary>
	/// <remarks>
	/// This class is used internally by the framework.
	/// </remarks>
	/// <see cref="PluginManager"/>
	public class PluginLoadedEventArgs : EventArgs
	{
		string _message;
		Assembly _pluginAssembly;

		internal PluginLoadedEventArgs(string message, Assembly pluginAssembly)
		{
			_message = message;
			_pluginAssembly = pluginAssembly;
		}

		/// <summary>
		/// Gets a user-friendly message describing the plugin that was loaded.
		/// </summary>
		/// <remarks>
		/// This is typically just the full name of the plugin assembly.
		/// </remarks>
		public string Message
		{
			get
			{
				return _message;
			}
		}

		/// <summary>
		/// Gets the plugin assembly that was loaded, if any.
		/// </summary>
		/// <remarks>
		/// Null if no actual assembly was loaded for this particular event.
		/// </remarks>
		public Assembly PluginAssembly
		{
			get
			{
				return _pluginAssembly;
			}
		}
	}
}
