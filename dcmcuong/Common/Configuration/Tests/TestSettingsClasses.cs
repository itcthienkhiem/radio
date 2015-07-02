#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

#if	UNIT_TESTS
#pragma warning disable 1591

using System.Collections.Generic;
using System.Configuration;
using System;
using System.Reflection;
using System.Xml;

namespace ClearCanvas.Common.Configuration.Tests
{
	internal abstract class TestApplicationSettingsBase : ApplicationSettingsBase
	{
		private static readonly Type _testType = typeof(TestApplicationSettingsBase);

		public static void ResetAll()
		{
			foreach(Type type in _testType.Assembly.GetTypes())
			{
				if (type.IsSubclassOf(_testType) && !type.IsAbstract)
					ApplicationSettingsHelper.GetSettingsClassInstance(type).Reset();
			}
		}
	}

	[SettingsProvider(typeof(TestSettingsProvider))]
	internal class SimpleUserSettings : TestApplicationSettingsBase
	{
		public const string PropertyUser = "User";

		private static readonly SimpleUserSettings _default = 
			(SimpleUserSettings) Synchronized(new SimpleUserSettings());
		
		private SimpleUserSettings()
		{
		}

		public static SimpleUserSettings Default { get { return _default; } }

		[UserScopedSetting]
		[DefaultSettingValue(PropertyUser)]
		public string User
		{
			get { return (string)base[PropertyUser]; }
			set { base[PropertyUser] = value; }
		}
	}

	[SettingsProvider(typeof(TestSettingsProvider))]
	[SharedSettingsMigrationDisabled]
	internal class NonMigratableSharedSettings : TestApplicationSettingsBase
	{
		public const string PropertyApp = "App";

		private static readonly NonMigratableSharedSettings _default =
			(NonMigratableSharedSettings)Synchronized(new NonMigratableSharedSettings());

		private NonMigratableSharedSettings()
		{
		}

		public static NonMigratableSharedSettings Default { get { return _default; } }

		[ApplicationScopedSetting]
		[DefaultSettingValue(PropertyApp)]
		public string App
		{
			get { return (string)base[PropertyApp]; }
			set { base[PropertyApp] = value; }
		}
	}

	[SettingsProvider(typeof(TestSettingsProvider))]
	internal class MixedScopeSettings : MixedScopeSettingsBase
	{
		private static readonly MixedScopeSettings _default =
			(MixedScopeSettings)Synchronized(new MixedScopeSettings());

		private MixedScopeSettings()
		{
		}
		
		public static MixedScopeSettings Default { get { return _default; } }
	}

	[SettingsProvider(typeof(TestSettingsProvider))]
	internal class InstanceMixedScopeSettings : MixedScopeSettingsBase
	{
	}

	[SettingsProvider(typeof(TestSettingsProvider))]
	internal class CustomMigrationMixedScopeSettings : MixedScopeSettingsBase, IMigrateSettings
	{
		private static readonly CustomMigrationMixedScopeSettings _default =
			(CustomMigrationMixedScopeSettings) Synchronized(new CustomMigrationMixedScopeSettings());

		private CustomMigrationMixedScopeSettings()
		{
		}

		public static CustomMigrationMixedScopeSettings Default { get { return _default; } }

		#region IMigrateSettings Members

		public void MigrateSettingsProperty(SettingsPropertyMigrationValues migrationValues)
		{
			switch (migrationValues.PropertyName)
			{
				case "User1":
					if (migrationValues.MigrationScope == MigrationScope.User)
						migrationValues.CurrentValue = "CustomUser1";
					else 
						migrationValues.CurrentValue = migrationValues.PreviousValue;
					break;
				case "User2":
					if (migrationValues.MigrationScope == MigrationScope.User)
						migrationValues.CurrentValue = migrationValues.PreviousValue;
					//otherwise, not migrated
					break;
				case "App1":
					break;
				case "App2":
					migrationValues.CurrentValue = "CustomApp2";
					break;
			}
		}

		#endregion
	}

	[SettingsProvider(typeof(LocalFileSettingsProvider))]
	internal class LocalXmlSettings : TestApplicationSettingsBase
	{
		public const string PropertyApp = "App";
		public const string PropertyUser = "User";

		public const string DefaultValueUser = null;
		public const string DefaultValueApp = "<?xml version=\"1.0\" encoding=\"utf-16\"?><test-xml-values><test>App</test></test-xml-values>";

		[ApplicationScopedSetting]
		[DefaultSettingValueAttribute(DefaultValueApp)]
		public XmlDocument App
		{
			get { return (XmlDocument)base[PropertyApp]; }
			set { base[PropertyApp] = value; }
		}

		[UserScopedSetting]
		//[DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?><test-xml-values><test>App</test></test-xml-values>")]
		public XmlDocument User
		{
			get { return (XmlDocument)base[PropertyUser]; }
			set { base[PropertyUser] = value; }
		}
	}
	[SettingsProvider(typeof(LocalFileSettingsProvider))]
	internal class LocalMixedScopeSettings : MixedScopeSettingsBase
	{
		//private static readonly LocalMixedScopeSettings _default =
		//    (LocalMixedScopeSettings)Synchronized(new LocalMixedScopeSettings());

		public LocalMixedScopeSettings()
		{
		}

		//public static LocalMixedScopeSettings Default { get { return _default; } }
	}

	internal abstract class MixedScopeSettingsBase : TestApplicationSettingsBase
	{
		public const string PropertyUser1 = "User1";
		public const string PropertyUser2 = "User2";
		public const string PropertyApp1 = "App1";
		public const string PropertyApp2 = "App2";

		[ApplicationScopedSetting]
		[DefaultSettingValue(PropertyApp1)]
		public string App1
		{
			get { return (string)base[PropertyApp1]; }
			set { base[PropertyApp1] = value; }
		}

		[ApplicationScopedSetting]
		[DefaultSettingValue(PropertyApp2)]
		public string App2
		{
			get { return (string)base[PropertyApp2]; }
			set { base[PropertyApp2] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue(PropertyUser1)]
		public string User1
		{
			get { return (string)base[PropertyUser1]; }
			set { base[PropertyUser1] = value; }
		}

		[UserScopedSetting]
		[DefaultSettingValue(PropertyUser2)]
		public string User2
		{
			get { return (string)base[PropertyUser2]; }
			set { base[PropertyUser2] = value; }
		}
	}
}

#endif