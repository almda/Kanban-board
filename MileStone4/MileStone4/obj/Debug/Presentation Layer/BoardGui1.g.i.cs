﻿#pragma checksum "..\..\..\Presentation Layer\BoardGui1.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1757AE78AEFB3BC8E57142D0D96278ECAB2AF733"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MileStone3;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MileStone3 {
    
    
    /// <summary>
    /// BoardGui
    /// </summary>
    public partial class BoardGui : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid g;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl itemcon;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel SwitcherPanel;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox2;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox1;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SwitchButton;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel TaskCreater;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar cc;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ErrorsLabel;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Presentation Layer\BoardGui1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas ColumnCanvas;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MileStone3;component/presentation%20layer/boardgui1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Presentation Layer\BoardGui1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.g = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.itemcon = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 3:
            this.SwitcherPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 4:
            this.ComboBox2 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.ComboBox1 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.SwitchButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Presentation Layer\BoardGui1.xaml"
            this.SwitchButton.Click += new System.Windows.RoutedEventHandler(this.SwitchButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TaskCreater = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 8:
            this.cc = ((System.Windows.Controls.Calendar)(target));
            return;
            case 9:
            
            #line 30 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Create_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 32 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Column_Sort);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 33 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Title_Sort);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 34 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Des_Sort);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 35 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Des_Sort);
            
            #line default
            #line hidden
            return;
            case 14:
            this.ErrorsLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 15:
            this.ColumnCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 16:
            
            #line 45 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 47 "..\..\..\Presentation Layer\BoardGui1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.add_column);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

