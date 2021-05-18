using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding
{
    public class Person : INotifyPropertyChanged
    {
       
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        private int _alter;
        public int Alter
        {
            get => _alter;
            set
            {
                _alter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Alter)));
            }
        }

        public List<DateTime> WichtigeTage { get; set; } = new List<DateTime>()
        {
            new DateTime(2002, 12, 3)
        };

        public override string ToString()
        {
            return $"{Vorname} {Nachname}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
