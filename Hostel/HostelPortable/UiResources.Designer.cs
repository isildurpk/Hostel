﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HostelPortable {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class UiResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal UiResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("HostelPortable.UiResources", typeof(UiResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Загрузка....
        /// </summary>
        internal static string DefaultBusyMessage {
            get {
                return ResourceManager.GetString("DefaultBusyMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Редактирование записи.
        /// </summary>
        internal static string EditorName {
            get {
                return ResourceManager.GetString("EditorName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Дата не может быть больше текущей.
        /// </summary>
        internal static string ErrorDateMoreThanNow {
            get {
                return ResourceManager.GetString("ErrorDateMoreThanNow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Длина поля не должна быть больше {0}.
        /// </summary>
        internal static string ErrorMaxLengthFormat {
            get {
                return ResourceManager.GetString("ErrorMaxLengthFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле не должно быть пустым.
        /// </summary>
        internal static string ErrorRequired {
            get {
                return ResourceManager.GetString("ErrorRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Недопустимые символы.
        /// </summary>
        internal static string ErrorUnavailableSymbols {
            get {
                return ResourceManager.GetString("ErrorUnavailableSymbols", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Карточка студента.
        /// </summary>
        internal static string StudentCardName {
            get {
                return ResourceManager.GetString("StudentCardName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Студенты.
        /// </summary>
        internal static string StudentWorkspaceName {
            get {
                return ResourceManager.GetString("StudentWorkspaceName", resourceCulture);
            }
        }
    }
}
