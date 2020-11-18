using System;
using System.Windows.Input;

namespace HRInPocket.WPF.Infrastructure.Commands.Base
{
    internal abstract class CommandCore : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        bool ICommand.CanExecute(object p) => CanExecute(p);
        void ICommand.Execute(object p) => Execute(p);

        protected virtual bool CanExecute(object p) => true;
        protected abstract void Execute(object p);
    }
}
