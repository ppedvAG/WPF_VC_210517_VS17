using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Localisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private TestEnum _selecedEnumValue;
        public TestEnum SelecedEnumValue { get => _selecedEnumValue; set { _selecedEnumValue = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelecedEnumValue))); } }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            else
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            new MainWindow().Show();

            this.Close();
        }
    }

    public enum TestEnum { TestEnum_1, TestEnum_2, TestEnum_3 }

    //Spezielles Markup-Extension-Objekt
    public sealed class EnumerateExtension : MarkupExtension
    {
        //Type = Enum-Typ
        public Type Type { get; set; }
        //Der Übergabe-Parameter wird in XAML direkt hinter den Aufruf der Markupextension gesetzt (vgl. XAML)
        public EnumerateExtension(Type type)
        {
            this.Type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //Umwandlung der Enums in lokalisierte Strings und Rückgabe dieser an den Aufrufer (hier wird eine ItemsSource-Property erwartet)
            string[] names = Enum.GetNames(Type);
            string[] values = new string[names.Length];

            for (int i = 0; i < names.Length; i++)
                values[i] = Loc.Resource.ResourceManager.GetString(names[i]);

            return values;
        }
    }

    //Converter zum Umwandeln des Enum-Werts in ComboBoxEintrag-String
    public sealed class EnumToStringConverter : IValueConverter
    {
        //Enum -> lokalisierter ComboBoxEintrag
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            //Zugriff auf ResX
            return Loc.Resource.ResourceManager.GetString(value.ToString());
        }

        //lokalisierter ComboBoxEintrag -> Enum
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string str = (string)value;

            foreach (object enumValue in Enum.GetValues(targetType))
            {
                if (str == Loc.Resource.ResourceManager.GetString(enumValue.ToString()))
                    return enumValue;
            }

            throw new ArgumentException(null, "value");
        }
    }
}
