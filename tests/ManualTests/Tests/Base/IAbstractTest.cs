using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ManualTests.Tests.Base
{
    public interface IAbstractTest
    {
        string RequiredAction { get; }
        TestResult Result { get; }
        string ShortDescription { get; }
        ICommand RunTestCommand { get; }
        bool IsAvailable { get; }
        Task RunAsync();
    }
}