﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        //CustomEvent
        public static readonly RoutedEvent TapEvent = EventManager.RegisterRoutedEvent("Tap", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPicker));

        //Handleranmeldung
        public event RoutedEventHandler Tap
        {
            add { AddHandler(TapEvent, value); }
            remove { RemoveHandler(TapEvent, value); }
        }

        //Methode zum Feuern des Eventhandlers
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ColorPicker.TapEvent);
            RaiseEvent(newEventArgs);
        }

        public ColorPicker()
        {
            InitializeComponent();

            //EventRaising
            Tbl_Output.PreviewMouseDown += (s, e) => RaiseTapEvent();

            //Erstellen einer neuen Bindung (Fill-Eigenschaft des Rechtecks an PickedColor-Eigenschaft)
            //Initialisierung mit Übergabe der zu Bindenden Quell-Eigenschaft
            Binding binding = new Binding("Fill");
            //Setzen des Quell-Objekts
            binding.Source = Rct_Output;
            //Setzen des Bindings-Modes
            binding.Mode = BindingMode.OneWay;
            //Setzen des UpdateSourceTriggers
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            //Erstellen der Bindung mit Übergabe des Ziel-Objekts, der Ziel-Eigenschaft und des Bindungs-Elements
            BindingOperations.SetBinding(this, PickedColorProperty, binding);
        }

        //Damit der Control eine Ausgabe hat, an welche die User andere Properties binden können muss für jede dieser Ausgaben eine DependencyProperty
        //erstellt werden, welche im Konstruktor des UserControlls manuell an die entsprechnende Property eines Teilelements gebunden wird.
        //DependencyProperties sind spezielle WPF-Element-Properties, welche nicht in den Objekten selbst gespeichert werden. Stattdessen werden diese
        //in einer eigenen Liste abgelegt. Durch diese Mechanik werden Bindungen und Co in WPF erst möglich.

        //Getter/Setter der DependencyProperty
        public SolidColorBrush PickedColor
        {
            get { return (SolidColorBrush)GetValue(PickedColorProperty); }
            set { SetValue(PickedColorProperty, value); }
        }

        //Registrierung für neue Bindungen an der DependencyProperty
        public static readonly DependencyProperty PickedColorProperty =
            DependencyProperty.Register("PickedColor", typeof(SolidColorBrush), typeof(ColorPicker), new PropertyMetadata(default(SolidColorBrush)));


        //AttachedProperty
        public static int GetCount(DependencyObject obj)
        {
            return (int)obj.GetValue(CountProperty);
        }

        public static void SetCount(DependencyObject obj, int value)
        {
            obj.SetValue(CountProperty, value);
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.RegisterAttached("Count", typeof(int), typeof(ColorPicker), new PropertyMetadata(0));


    }
}