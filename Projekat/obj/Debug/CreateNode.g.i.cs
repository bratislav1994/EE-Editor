﻿#pragma checksum "..\..\CreateNode.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A28B5856A35756774EA41657F52DEA45E64D816C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Projekat;
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


namespace Projekat {
    
    
    /// <summary>
    /// CreateNode
    /// </summary>
    public partial class CreateNode : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_box1;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_box2;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_box4;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combo_box;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button1;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\CreateNode.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_box5;
        
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
            System.Uri resourceLocater = new System.Uri("/Projekat;component/createnode.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CreateNode.xaml"
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
            this.text_box1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\CreateNode.xaml"
            this.text_box1.GotFocus += new System.Windows.RoutedEventHandler(this.text_box1_GotFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.text_box2 = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\CreateNode.xaml"
            this.text_box2.GotFocus += new System.Windows.RoutedEventHandler(this.text_box2_GotFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.text_box4 = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\CreateNode.xaml"
            this.text_box4.GotFocus += new System.Windows.RoutedEventHandler(this.text_box4_GotFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.combo_box = ((System.Windows.Controls.ComboBox)(target));
            
            #line 18 "..\..\CreateNode.xaml"
            this.combo_box.GotFocus += new System.Windows.RoutedEventHandler(this.combo_box_GotFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\CreateNode.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.Create);
            
            #line default
            #line hidden
            return;
            case 6:
            this.button1 = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\CreateNode.xaml"
            this.button1.Click += new System.Windows.RoutedEventHandler(this.Cancel);
            
            #line default
            #line hidden
            return;
            case 7:
            this.text_box5 = ((System.Windows.Controls.TextBox)(target));
            
            #line 109 "..\..\CreateNode.xaml"
            this.text_box5.GotFocus += new System.Windows.RoutedEventHandler(this.text_box5_GotFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

