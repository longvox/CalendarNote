﻿#pragma checksum "..\..\..\MyUserControl\CalendarNote.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "824CCED49087736150D9B77707625046399306C7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CalendarNote.MyUserControl;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace CalendarNote.MyUserControl {
    
    
    /// <summary>
    /// CalendarNote
    /// </summary>
    public partial class CalendarNote : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\MyUserControl\CalendarNote.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnToDay;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\MyUserControl\CalendarNote.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPreviousMonth;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\MyUserControl\CalendarNote.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextMonth;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\MyUserControl\CalendarNote.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TxbMonthCurrent;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\MyUserControl\CalendarNote.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txbTest;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\MyUserControl\CalendarNote.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grCalendar;
        
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
            System.Uri resourceLocater = new System.Uri("/CalendarNote;component/myusercontrol/calendarnote.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MyUserControl\CalendarNote.xaml"
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
            this.btnToDay = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\MyUserControl\CalendarNote.xaml"
            this.btnToDay.Click += new System.Windows.RoutedEventHandler(this.click_btnToday);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnPreviousMonth = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\MyUserControl\CalendarNote.xaml"
            this.btnPreviousMonth.Click += new System.Windows.RoutedEventHandler(this.click_btnPreviousMonth);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnNextMonth = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\MyUserControl\CalendarNote.xaml"
            this.btnNextMonth.Click += new System.Windows.RoutedEventHandler(this.click_btnNextMonth);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TxbMonthCurrent = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txbTest = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.grCalendar = ((System.Windows.Controls.Grid)(target));
            
            #line 154 "..\..\..\MyUserControl\CalendarNote.xaml"
            this.grCalendar.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OnPreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
