namespace Haushaltsplan.Presentation
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        private Action Action { get; }
        private Func<Boolean> CanExecuteFunc { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action action, Func<Boolean> canExecute = null)
        {
            Action = action;
            CanExecuteFunc = canExecute;
        }

        public Boolean CanExecute(Object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public void Execute(Object parameter)
        {
            Action();
        }
    }
}
