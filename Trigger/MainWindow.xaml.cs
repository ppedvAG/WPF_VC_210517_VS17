using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Trigger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool boolVal;

        public bool BoolVal
        {
            get { return boolVal; }
            set { boolVal = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BoolVal))); }
        }


        public MainWindow()
        {
            InitializeComponent();

            this.BoolVal = true;

            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Btn_Aendern_Click(object sender, RoutedEventArgs e)
        {
            BoolVal = !BoolVal;
        }
    }
}
