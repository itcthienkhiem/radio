#region License

// Copyright (c) 2006-2008, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System.Configuration;
using ClearCanvas.Common.Configuration;

namespace ClearCanvas.Common
{
	/// <summary>
	/// Provides access to the current license key.
	/// </summary>
	public static class LicenseInformation
	{
		/// <summary>
		/// Gets and sets the license key string from the local app.config file.
		/// </summary>
		/// <remarks>
		/// Use this instead of the LicenseKey property in <see cref="LicenseSettings"/>
		/// </remarks>
		/// <returns></returns>
		public static string LicenseKey
		{
			get { return ApplicationSettingsExtensions.GetSharedPropertyValue(new LicenseSettings(), "LicenseKey").ToString(); }
			set { ApplicationSettingsExtensions.SetSharedPropertyValue(new LicenseSettings(), "LicenseKey", value); }
		}
	}

	[SettingsGroupDescription("Settings that store the license information.")]
	[SettingsProvider(typeof(LocalFileSettingsProvider))]
	[SharedSettingsMigrationDisabled]
	internal sealed partial class LicenseSettings
	{
	}
}
