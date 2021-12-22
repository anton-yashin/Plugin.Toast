using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ManualTests.Xamarin.Services
{
    sealed class CommandUpdater : ICommandUpdater
    {
        public void CanExecuteChanged(ICommand command)
        {
            if (command is not Command cmd)
                throw new ArgumentException($"argument type is not {nameof(Command)}", nameof(command));
            cmd.ChangeCanExecute();
        }
    }
}
