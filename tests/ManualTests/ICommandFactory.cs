using System;
using System.Windows.Input;

namespace ManualTests
{
    public interface ICommandFactory
    {
        ICommand Create<T>(Action<T> execute, Func<T, bool> canExecute);
    }
}
