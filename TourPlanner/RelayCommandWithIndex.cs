using System;
using System.Windows.Input;

namespace TourPlanner.UI
{
    internal class RelayCommandIndex : ICommand
    {
        private Action<int> _execute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommandIndex(Action<int> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is int index)
            {
                _execute(index);
            }

        }
    }
}