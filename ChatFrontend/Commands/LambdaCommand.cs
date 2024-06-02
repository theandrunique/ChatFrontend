using ShopContent.Commands.Base;
using System;

namespace ShopContent.Commands
{
    public class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (_canExecute != null && !_canExecute(parameter))
            {
                return;
            }

            _execute(parameter);
        }
    }
}
