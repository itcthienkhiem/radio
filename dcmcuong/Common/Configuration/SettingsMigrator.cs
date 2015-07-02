#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using System.Configuration;

namespace ClearCanvas.Common.Configuration
{
    internal class UserSettingsUpgradeStepFactory : IUserUpgradeStepFactory
    {
        public ICollection<UserUpgradeStep> CreateSteps()
        {
            var upgradeSteps = new List<UserUpgradeStep>();

			if (UpgradeSettings.IsUserUpgradeEnabled())
			{
				foreach (var group in SettingsGroupDescriptor.ListInstalledSettingsGroups(false))
				{
					try
					{
						UserSettingsUpgradeStep step = UserSettingsUpgradeStep.Create(group);
						if (step != null)
							upgradeSteps.Add(step);
					}
					catch (Exception e)
					{
						Platform.Log(LogLevel.Warn, e, "Unable to migrate user settings: {0}", group.Name);
					}
				}
			}

        	return upgradeSteps;
        }
    }

    internal class UserSettingsUpgradeStep : UserUpgradeStep
    {
        private UserSettingsUpgradeStep(ApplicationSettingsBase settings)
        {
            Platform.CheckForNullReference(settings, "settings");
            Settings = settings;
        }

        private ApplicationSettingsBase Settings { get; set; }

        public override string Identifier
        {
			get { return Settings.GetType().FullName; }
        }

        protected override bool PerformUpgrade()
        {
			ApplicationSettingsExtensions.MigrateUserSettings(Settings);
        	return true;
        }

		public static UserSettingsUpgradeStep Create(SettingsGroupDescriptor settingsGroup)
		{
			return Create(ApplicationSettingsHelper.GetSettingsClass(settingsGroup));
		}

    	public static UserSettingsUpgradeStep Create(Type settingsClass)
        {
			//Important: this line first so we can't get into infinite recursion issues with trying
			//to migrate the UpgradeSettings class on first access (SettingsStoreSettingsProvider does that).
			if (!ApplicationSettingsHelper.IsUserSettingsMigrationEnabled(settingsClass))
				return null;

			if (!new SettingsGroupDescriptor(settingsClass).HasUserScopedSettings)
				return null; //no point

			if (!UpgradeSettings.IsUserUpgradeEnabled())
				return null;

			if (UpgradeSettings.Default.IsUserUpgradeStepCompleted(settingsClass.FullName))
				return null;

			ApplicationSettingsBase settings = ApplicationSettingsHelper.GetSettingsClassInstance(settingsClass);
			return new UserSettingsUpgradeStep(settings);
		}
    }

    public static class SettingsMigrator
    {
		public static bool MigrateUserSettings(SettingsGroupDescriptor settingsGroup)
		{
			return MigrateUserSettings(ApplicationSettingsHelper.GetSettingsClass(settingsGroup));
		}

    	public static bool MigrateUserSettings(Type settingsClass)
        {
			UserSettingsUpgradeStep step = UserSettingsUpgradeStep.Create(settingsClass);
			return step != null && step.Run();
        }

		public static bool MigrateSharedSettings(SettingsGroupDescriptor settingsGroup, string previousExeConfigFilename)
		{
			return MigrateSharedSettings(ApplicationSettingsHelper.GetSettingsClass(settingsGroup), previousExeConfigFilename);
		}

    	public static bool MigrateSharedSettings(Type settingsClass, string previousExeConfigFilename)
        {
			if (!ApplicationSettingsHelper.IsSharedSettingsMigrationEnabled(settingsClass))
				return false;

			ApplicationSettingsBase settings = ApplicationSettingsHelper.GetSettingsClassInstance(settingsClass);
			ApplicationSettingsExtensions.MigrateSharedSettings(settings, previousExeConfigFilename);
    		return true;
        }
    }
}