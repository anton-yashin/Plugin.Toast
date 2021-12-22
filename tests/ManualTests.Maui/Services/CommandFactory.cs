using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace ManualTests.Maui.Services
{
    sealed class CommandFactory : ICommandFactory
    {
        public ICommand Create<T>(Action<T> execute, Func<T, bool> canExecute)
            => new Command<T>(execute, canExecute);
    }
}
