﻿#pragma checksum "C:\Users\kcrol\Desktop\studia\Wójcik\OiM\OiM_UWP\Views\TSPView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "981B8E909D74B74335B116E3A71E136F"
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
    partial class TSPView : 
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
            case 2: // Views\TSPView.xaml line 102
                {
                    this.vertexATextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // Views\TSPView.xaml line 104
                {
                    this.vertexBTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4: // Views\TSPView.xaml line 106
                {
                    this.holesQuantityTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5: // Views\TSPView.xaml line 108
                {
                    this.randomRadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 6: // Views\TSPView.xaml line 109
                {
                    this.holesRadioButton = (global::Windows.UI.Xaml.Controls.RadioButton)(target);
                }
                break;
            case 7: // Views\TSPView.xaml line 81
                {
                    global::Windows.UI.Xaml.Controls.Button element7 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element7).Click += this.generatePoints;
                }
                break;
            case 8: // Views\TSPView.xaml line 39
                {
                    this.treeScrollViewer = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 9: // Views\TSPView.xaml line 51
                {
                    this.pointsTable = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 10: // Views\TSPView.xaml line 40
                {
                    this.matrixGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
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

