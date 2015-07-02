#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using ClearCanvas.Common.Configuration;

namespace ClearCanvas.Common
{
	/// <summary>
	/// Provides some basic information about the product, such as the name and version.
	/// </summary>
	public static class ProductInformation
	{
		private static string _product;
		private static string _component;
		private static string _edition;
		private static Version _version;
		private static string _versionSuffix;
		private static string _copyright;
		private static string _license;
		private static bool? _diagnosticRelease;

		static ProductInformation()
		{
			ProductSettings.Default.PropertyChanged += OnSettingPropertyChanged;
		}

		private static void OnSettingPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			_product = null;
			_component = null;
			_edition = null;
			_version = null;
			_versionSuffix = null;
			_copyright = null;
			_license = null;
			_diagnosticRelease = null;
		}

		/// <summary>
		/// Gets the component name.
		/// </summary>
		[Obsolete("It is recommended that the Component property be used to avoid ambiguity.")]
		public static string Name
		{
			get { return Component; }
		}

		/// <summary>
		/// Gets the component name.
		/// </summary>
		public static string Component
		{
			get
			{
				if (_component == null)
					_component = Decrypt(ProductSettings.Default.Component);
				return _component;
			}
		}

		/// <summary>
		/// Gets the product name.
		/// </summary>
		public static string Product
		{
			get
			{
				if (_product == null)
					_product = Decrypt(ProductSettings.Default.Product);
				return _product;
			}
		}

		/// <summary>
		/// Gets the product edition.
		/// </summary>
		public static string Edition
		{
			get
			{
				if (_edition == null)
					_edition = Decrypt(ProductSettings.Default.Edition);
				return _edition;
			}
		}

		/// <summary>
		/// Gets the product version.
		/// </summary>
		public static Version Version
		{
			get
			{
				if (_version == null)
				{
					string version = Decrypt(ProductSettings.Default.Version);
					try
					{
						if (String.IsNullOrEmpty(version))
							_version = Assembly.GetExecutingAssembly().GetName().Version;
						else
							_version = new Version(version);
					}
					catch (Exception)
					{
						// don't allow a poorly formatted version string to kill the app
						_version = Assembly.GetExecutingAssembly().GetName().Version;
					}
				}
				return _version;
			}
		}

		/// <summary>
		/// Gets the product version suffix (e.g. "SP1").
		/// </summary>
		public static string VersionSuffix
		{
			get
			{
				if (_versionSuffix == null)
				{
					string versionSuffix = Decrypt(ProductSettings.Default.VersionSuffix);
					if (String.IsNullOrEmpty(versionSuffix) || versionSuffix[0] != '*')
						_versionSuffix = "Unverified Build";
					else
						_versionSuffix = versionSuffix.Substring(1);
				}
				return _versionSuffix;
			}
		}

		/// <summary>
		/// Gets the product copyright (e.g. "Copyright 2009 ClearCanvas Inc.").
		/// </summary>
		public static string Copyright
		{
			get
			{
				if (_copyright == null)
					_copyright = Decrypt(ProductSettings.Default.Copyright);
				return _copyright;
			}
		}

		/// <summary>
		/// Gets the product license.
		/// </summary>
		public static string License
		{
			get
			{
				if (_license == null)
					_license = Decrypt(ProductSettings.Default.License);
				return _license;
			}
		}

		/// <summary>
		/// Gets a value indicating whether or not this product version is suitable for diagnostic use.
		/// </summary>
		public static bool AllowDiagnosticUse
		{
			get
			{
				if (!_diagnosticRelease.HasValue)
					_diagnosticRelease = string.Equals("True", Decrypt(ProductSettings.Default.AllowDiagnosticUse), StringComparison.InvariantCultureIgnoreCase);
				return _diagnosticRelease.Value;
			}
		}

		private static string Decrypt(string @string)
		{
			if (String.IsNullOrEmpty(@string))
				return @string;

			string result;
			try
			{
				byte[] bytes = Convert.FromBase64String(@string);
				using (MemoryStream dataStream = new MemoryStream(bytes))
				{
					RC2CryptoServiceProvider cryptoService = new RC2CryptoServiceProvider();
					cryptoService.Key = Encoding.UTF8.GetBytes("ClearCanvas");
					cryptoService.IV = Encoding.UTF8.GetBytes("IsSoCool");
					cryptoService.UseSalt = false;
					using (CryptoStream cryptoStream = new CryptoStream(dataStream, cryptoService.CreateDecryptor(), CryptoStreamMode.Read))
					{
						using (StreamReader reader = new StreamReader(cryptoStream, Encoding.UTF8))
						{
							result = reader.ReadToEnd();
							reader.Close();
						}
						cryptoStream.Close();
					}
					dataStream.Close();
				}
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		/// <summary>
		/// Gets the component name, optionally with the product edition.
		/// </summary>
		/// <param name="includeEdition">A value indciating whether or not the include the product edition in the name.</param>
		public static string GetName(bool includeEdition)
		{
			if (includeEdition && !string.IsNullOrEmpty(Edition) && !string.IsNullOrEmpty(Component))
				return string.Format("{0} {1}", Component, Edition);
			return Component;
		}

		/// <summary>
		/// Gets a string containing both the component name, product edition and version.
		/// </summary>
		/// <param name="includeBuildAndRevision">Specifies whether to include the build and revision numbers in the version; false means only the major and minor numbers are included.</param>
		/// <param name="includeVersionSuffix">Specifies whether to include the version suffix.</param>
		public static string GetNameAndVersion(bool includeBuildAndRevision, bool includeVersionSuffix)
		{
			return GetNameAndVersion(includeBuildAndRevision, includeVersionSuffix, true);
		}

		/// <summary>
		/// Gets a string containing both the component name and version, optionally with the product edition.
		/// </summary>
		/// <param name="includeBuildAndRevision">Specifies whether to include the build and revision numbers in the version; false means only the major and minor numbers are included.</param>
		/// <param name="includeVersionSuffix">Specifies whether to include the version suffix.</param>
		/// <param name="includeEdition">A value indciating whether or not the include the product edition in the name.</param>
		public static string GetNameAndVersion(bool includeBuildAndRevision, bool includeVersionSuffix, bool includeEdition)
		{
			return string.Format("{0} {1}", GetName(includeEdition), GetVersion(includeBuildAndRevision, includeVersionSuffix));
		}

		/// <summary>
		/// Gets the version as a string, formatted based on the input options.
		/// </summary>
		/// <param name="includeBuildAndRevision">Specifies whether to include the build and revision numbers in the version; false means only the major and minor numbers are included.</param>
		/// <param name="includeVersionSuffix">Specifies whether to include the version suffix.</param>
		public static string GetVersion(bool includeBuildAndRevision, bool includeVersionSuffix)
		{
			string versionString;
			Version version = Version;

			if (includeBuildAndRevision)
				versionString = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
			else
				versionString = String.Format("{0}.{1}", version.Major, version.Minor);

			if (includeVersionSuffix && !String.IsNullOrEmpty(VersionSuffix))
				return String.Format("{0} {1}", versionString, VersionSuffix);

			return versionString;
		}
	}

	[SettingsGroupDescription("Settings that describe the product, such as the product name and version.")]
	[SettingsProvider(typeof (ApplicationCriticalSettingsProvider))]
	[SharedSettingsMigrationDisabled]
	internal sealed partial class ProductSettings
	{
		private ProductSettings() {}
	}
}