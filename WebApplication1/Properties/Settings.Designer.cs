﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\temp\\products.ndb")]
        public string Ndatabase {
            get {
                return ((string)(this["Ndatabase"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\temp\\events.csv")]
        public string csvFile {
            get {
                return ((string)(this["csvFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\r\nhttps://hashtagmcb.documents.azure.com:443/")]
        public string endpointUrl {
            get {
                return ((string)(this["endpointUrl"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("gMltCWXA3FFtAPRyU4nTq1tcx6790px7uqFnVmsPE9VwlMLAeG1dzHkKnX28otloycn5EnP3b39ufqHyE" +
            "TnzFA==")]
        public string authorizationKey {
            get {
                return ((string)(this["authorizationKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("events")]
        public string databaseId {
            get {
                return ((string)(this["databaseId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("eventcollection")]
        public string collectionId {
            get {
                return ((string)(this["collectionId"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public string initdatabase {
            get {
                return ((string)(this["initdatabase"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hashtagmcb")]
        public string repository {
            get {
                return ((string)(this["repository"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("todd")]
        public string blogStorageCollection {
            get {
                return ((string)(this["blogStorageCollection"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tPymIUi6utGvLyLAfroW1YBBHZ5oKR86hXDohI3nSVFxhuwO7fPmwNC7CF8DHHI7Dk2mfqn17UmjBHPJk" +
            "3mozA==")]
        public string secondaryKey {
            get {
                return ((string)(this["secondaryKey"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AccountEndpoint=https://hashtagmcb.documents.azure.com:443/;AccountKey=gMltCWXA3F" +
            "FtAPRyU4nTq1tcx6790px7uqFnVmsPE9VwlMLAeG1dzHkKnX28otloycn5EnP3b39ufqHyETnzFA==;")]
        public string primaryConnectionString {
            get {
                return ((string)(this["primaryConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AccountEndpoint=https://hashtagmcb.documents.azure.com:443/;AccountKey=tPymIUi6ut" +
            "GvLyLAfroW1YBBHZ5oKR86hXDohI3nSVFxhuwO7fPmwNC7CF8DHHI7Dk2mfqn17UmjBHPJk3mozA==;")]
        public string secondaryConnectionString {
            get {
                return ((string)(this["secondaryConnectionString"]));
            }
        }
    }
}
