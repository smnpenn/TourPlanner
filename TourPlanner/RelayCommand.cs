using System;
using System.Windows.Input;

namespace TourPlanner.UI
{
    internal class RelayCommand : ICommand
    {
        private Action<object> _execute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}