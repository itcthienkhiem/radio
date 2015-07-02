#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;

namespace ClearCanvas.Common.Shreds
{
	/// <summary>
	/// Abstract base implementation of <see cref="IShred"/>.  Shred implementations should inherit
	/// from this class rather than implement <see cref="IShred"/> directly.
	/// </summary>
    public abstract class Shred : MarshalByRefObject, IShred
    {
		///<summary>
		///Obtains a lifetime service object to control the lifetime policy for this instance.
		///</summary>
		public override object InitializeLifetimeService()
        {
            // cause lifetime lease to never expire
            return null;
        }

        #region IShred Members

		/// <summary>
		/// Called to start the shred.
		/// </summary>
		/// <remarks>
		/// This method should perform any initialization of the shred, and then return immediately.
		/// </remarks>
        public abstract void Start();

		/// <summary>
		/// Called to stop the shred.
		/// </summary>
		/// <remarks>
		/// This method should perform any necessary clean-up, and then return immediately.
		/// </remarks>
        public abstract void Stop();

		/// <summary>
		/// Gets the display name of the shred.
		/// </summary>
		/// <returns></returns>
        public abstract string GetDisplayName();

		/// <summary>
		/// Gets a description of the shred.
		/// </summary>
		/// <returns></returns>
        public abstract string GetDescription();

        #endregion       
    }
}
