using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ManualTests.Xamarin.Services
{
    sealed class CommandFactory : ICommandFactory
    {
        public ICommand Create<T>(Action<T> execute, Func<T, bool> canExecute)
            => new Command<T>(execute, canExecute);
    }
}
