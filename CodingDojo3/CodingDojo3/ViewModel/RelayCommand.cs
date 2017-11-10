using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CodingDojo3.ViewModel
{
    public class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            _execute = executeMethod;
            _canExecute = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return false;

            return _canExecute();
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
        }
    }
}
