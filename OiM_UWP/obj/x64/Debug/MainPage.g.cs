﻿#pragma checksum "C:\Users\games\source\repos\OiM_UWP\OiM\OiM_UWP\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "151CB22284D00498ACBA0A98CEC7015A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OiM_UWP
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 12
                {
                    this.nvSample = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.nvSample).ItemInvoked += this.nvSample_ItemInvoked;
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.nvSample).BackRequested += this.nvSample_BackRequested;
                }
                break;
            case 3: // MainPage.xaml line 43
                {
                    this.contentFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                    ((global::Windows.UI.Xaml.Controls.Frame)this.contentFrame).NavigationFailed += this.contentFrame_NavigationFailed;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

