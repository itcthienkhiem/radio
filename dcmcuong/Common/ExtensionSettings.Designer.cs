﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClearCanvas.Common {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class ExtensionSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static ExtensionSettings defaultInstance = ((ExtensionSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new ExtensionSettings())));
        
        public static ExtensionSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<extensions />")]
        public global::System.Xml.XmlDocument ExtensionConfigurationXml {
            get {
                return ((global::System.Xml.XmlDocument)(this["ExtensionConfigurationXml"]));
            }
        }
    }
}
