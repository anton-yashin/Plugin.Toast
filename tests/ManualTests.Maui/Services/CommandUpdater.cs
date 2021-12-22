using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace ManualTests.Maui.Services
{
    sealed class CommandUpdater : ICommandUpdater
    {
        private readonly ICoreDispatcher dispatcher;

        public CommandUpdater(ICoreDispatcher dispatcher) => this.dispatcher = dispatcher;

        public void CanExecuteChanged(ICommand command)
        {
            if (command is not Command cmd)
                throw new ArgumentException($"argument type is not {nameof(Command)}", nameof(command));
            if (dispatcher.IsDispatchRequired)
                dispatcher.Dispatch(cmd.ChangeCanExecute);
            else
                cmd.ChangeCanExecute();
        }
    }
}
