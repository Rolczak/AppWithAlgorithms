
﻿#pragma checksum "C:\Users\kcrol\Desktop\studia\Wójcik\OiM\OiM_UWP\Views\HuffmanView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E5D09699C4AE58981BB1CB510C3EFE45"

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OiM_UWP.Views
{
    partial class HuffmanView : 
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
            case 2: // Views\HuffmanView.xaml line 79
                {
                    this.messageTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // Views\HuffmanView.xaml line 71
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.calculate;
                }
                break;
            case 4: // Views\HuffmanView.xaml line 40
                {
                    this.treeScrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 5: // Views\HuffmanView.xaml line 50
                {
                    this.codingTable = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 6: // Views\HuffmanView.xaml line 41
                {
                    this.resultGrid = (global::Windows.UI.Xaml.Controls.Canvas)(target);
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

