using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class CustomCommand : ICommand
    {
        public Action<object> ExecuteMethod { get; set; }
        public Func<object, bool> CanExecuteMethod { get; set; }

        public CustomCommand(Action<object> exe, Func<object, bool> can = null)
        {
            ExecuteMethod = exe;

            CanExecuteMethod = (can == null) ? (p => true) : can;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteMethod(parameter);
        }
    }
}
