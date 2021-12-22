using System;
using System.Windows.Input;

namespace ManualTests
{
    public interface ICommandUpdater
    {
        void CanExecuteChanged(ICommand command);
    }
}
