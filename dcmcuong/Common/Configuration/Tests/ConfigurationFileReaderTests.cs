#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

#if UNIT_TESTS

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace ClearCanvas.Common.Configuration.Tests
{
	[TestFixture]
	public class ConfigurationFileReaderTests : SettingsTestBase
	{
		[Test]
		public void TestReadNoSettings()
		{
			Type settingsClass = typeof(LocalMixedScopeSettings);
			SystemConfigurationHelper.RemoveSettingsValues(SystemConfigurationHelper.GetExeConfiguration(), settingsClass);

			var reader = new ConfigurationFileReader(SystemConfigurationHelper.GetExeConfiguration().FilePath);
			var path = new ConfigurationSectionPath(typeof(LocalMixedScopeSettings), SettingScope.Application);
			var values = reader.GetSettingsValues(path);
			Assert.AreEqual(0, values.Count);

			path = new ConfigurationSectionPath(typeof(LocalMixedScopeSettings), SettingScope.User);
			values = reader.GetSettingsValues(path);
			Assert.AreEqual(0, values.Count);
		}

		[Test]
		public void TestReadStringSettings()
		{
			Type settingsClass = typeof (LocalMixedScopeSettings);
			var settings = ApplicationSettingsHelper.GetSettingsClassInstance(settingsClass);

			ApplicationSettingsExtensions.SetSharedPropertyValue(settings, LocalMixedScopeSettings.PropertyApp1, "TestApp1");
			ApplicationSettingsExtensions.SetSharedPropertyValue(settings, LocalMixedScopeSettings.PropertyApp2, "TestApp2");
			ApplicationSettingsExtensions.SetSharedPropertyValue(settings, LocalMixedScopeSettings.PropertyUser1, "TestUser1");
			ApplicationSettingsExtensions.SetSharedPropertyValue(settings, LocalMixedScopeSettings.PropertyUser2, "TestUser2");

			var reader = new ConfigurationFileReader(SystemConfigurationHelper.GetExeConfiguration().FilePath);
			var path = new ConfigurationSectionPath(typeof(LocalMixedScopeSettings), SettingScope.Application);
			var values = reader.GetSettingsValues(path);
			Assert.AreEqual(2, values.Count);
			Assert.AreEqual("TestApp1", values[LocalMixedScopeSettings.PropertyApp1]);
			Assert.AreEqual("TestApp2", values[LocalMixedScopeSettings.PropertyApp2]);

			path = new ConfigurationSectionPath(typeof(LocalMixedScopeSettings), SettingScope.User);
			values = reader.GetSettingsValues(path);
			Assert.AreEqual(2, values.Count);
			Assert.AreEqual("TestUser1", values[LocalMixedScopeSettings.PropertyUser1]);
			Assert.AreEqual("TestUser2", values[LocalMixedScopeSettings.PropertyUser2]);
			
			SystemConfigurationHelper.RemoveSettingsValues(SystemConfigurationHelper.GetExeConfiguration(), settingsClass);
		}

		[Test]
		public void TestReadXmlSettings()
		{
			Type settingsClass = typeof(LocalXmlSettings);
			var settings = ApplicationSettingsHelper.GetSettingsClassInstance(settingsClass);

			var appValue = @"<test><app/></test>";
			XmlDocument appDocument = new XmlDocument();
			appDocument.LoadXml(appValue);
			ApplicationSettingsExtensions.SetSharedPropertyValue(settings, LocalXmlSettings.PropertyApp, appDocument);

			var userValue = @"<test><user/></test>";
			XmlDocument userDocument= new XmlDocument();
			userDocument.LoadXml(userValue);
			ApplicationSettingsExtensions.SetSharedPropertyValue(settings, LocalXmlSettings.PropertyUser, userDocument);

			var reader = new ConfigurationFileReader(SystemConfigurationHelper.GetExeConfiguration().FilePath);
			var path = new ConfigurationSectionPath(typeof(LocalXmlSettings), SettingScope.Application);
			var values = reader.GetSettingsValues(path);
			Assert.AreEqual(1, values.Count);

			XmlDocument testDocument = new XmlDocument();
			testDocument.LoadXml(values[LocalXmlSettings.PropertyApp]);
			Assert.AreEqual(appDocument.InnerXml, testDocument.InnerXml);

			path = new ConfigurationSectionPath(typeof(LocalXmlSettings), SettingScope.User);
			values = reader.GetSettingsValues(path);
			Assert.AreEqual(1, values.Count);

			testDocument = new XmlDocument();
			testDocument.LoadXml(values[LocalXmlSettings.PropertyUser]);
			Assert.AreEqual(userDocument.InnerXml, testDocument.InnerXml);

			SystemConfigurationHelper.RemoveSettingsValues(SystemConfigurationHelper.GetExeConfiguration(), settingsClass);
		}
	}
}

#endif