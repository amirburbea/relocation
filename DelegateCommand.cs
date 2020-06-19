using System;
using System.Windows.Input;

namespace Relocation
{
    public sealed class DelegateCommand : ICommand
    {
        private readonly Func<bool>? _canExecute;
        private readonly Action _execute;

        public DelegateCommand(Action execute, Func<bool>? canExecute = null)
        {
            (this._execute, this._canExecute) = (execute, canExecute);
        }

        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object parameter) => this.CanExecute();

        public bool CanExecute() => this._canExecute == null || this._canExecute();

        void ICommand.Execute(object parameter) => this.Execute();

        public void Execute()
        {
            if (this.CanExecute())
            {
                this._execute();
            }
        }
    }
}
