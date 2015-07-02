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
using System.Xml;
using ClearCanvas.Common.Configuration;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.Common
{
	// these settings must be stored in a config file, not in the configuration store
	[SettingsProvider(typeof(ApplicationCriticalSettingsProvider))]
	[SharedSettingsMigrationDisabled]
    internal sealed partial class ExtensionSettings
	{
        /// <summary>
        /// Orders the extensions according to the order specified by the XML document.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ordered"></param>
        /// <param name="remainder"></param>
        public void OrderExtensions(IEnumerable<ExtensionInfo> input,
            out List<ExtensionInfo> ordered, out List<ExtensionInfo> remainder)
        {
            ordered = new List<ExtensionInfo>();
            remainder = new List<ExtensionInfo>(input);

            // order the items based on the order in the XML
            foreach (XmlElement element in ListExtensionNodes())
            {
                string className = element.GetAttribute("class");

                // ignore entries that don't specify the "class" attribute
                // (these shouldn't exist in a well-formed doc)
                if (string.IsNullOrEmpty(className))
                    continue;

                // find the extensions corresponding to this class
                // (yes, it is possible that that are multiple extensions implemented by the same class)
                List<ExtensionInfo> items = CollectionUtils.Select(remainder,
                    delegate(ExtensionInfo x) { return Equals(Type.GetType(className), x.ExtensionClass); });

                // add these to the ordered list and remove them from the remainder
                foreach (ExtensionInfo ext in items)
                {
                    ordered.Add(ext);
                    remainder.Remove(ext);
                    if (remainder.Count == 0)
                        break;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified extension class is enabled.
        /// </summary>
        /// <param name="extensionClass"></param>
        /// <param name="defaultEnablement"></param>
        /// <returns></returns>
        public bool IsEnabled(Type extensionClass, bool defaultEnablement)
        {
            // find an XML entry for this class
            XmlElement extensionNode = (XmlElement)CollectionUtils.SelectFirst(ListExtensionNodes(),
                delegate(object node)
                {
                    string className = ((XmlElement)node).GetAttribute("class");
                    return !string.IsNullOrEmpty(className) ?
                        Equals(Type.GetType(className), extensionClass) : false;
                });

            // if an entry exists, check if it specifies an enablement override
            if(extensionNode != null)
            {
                string enabledString = extensionNode.GetAttribute("enabled");
                if(!string.IsNullOrEmpty(enabledString))
                    return Convert.ToBoolean(enabledString);
            }

            // return default
            return defaultEnablement;
        }


        /// <summary>
        /// List the stored extensions in the XML doc
        /// </summary>
        /// <returns>A list of "extension" element</returns>
        private XmlNodeList ListExtensionNodes()
        {
            return this.ExtensionConfigurationXml.SelectNodes(String.Format("/extensions/extension"));
        }

    }
}
