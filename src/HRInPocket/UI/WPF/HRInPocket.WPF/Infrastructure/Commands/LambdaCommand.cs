using System;
using HRInPocket.WPF.Infrastructure.Commands.Base;

namespace HRInPocket.WPF.Infrastructure.Commands
{
    internal class LambdaCommand : CommandCore
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            canExecute = CanExecute;
        }

        protected override bool CanExecute(object p) => canExecute?.Invoke(p) ?? true;
        protected override void Execute(object p)
        {
            if (CanExecute(p)) execute(p);
        }
    }
}
