using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ManualTests.Tests.Base
{
    public abstract class AbstractTest<T> : INotifyPropertyChanged, IAbstractTest
        where T : AbstractTest<T>
    {
        private readonly Command<AbstractTest<T>> runTestCommand = new Command<AbstractTest<T>>(
            async _ => await _.RunAsync(), _ => _?.Result != TestResult.Running);

        protected readonly ILogger<T> logger;
        protected readonly IServiceProvider serviceProvider;

        protected AbstractTest(IServiceProvider serviceProvider, string requiredAction, string shortDescription)
        {
            this.logger = serviceProvider.GetRequiredService<ILogger<T>>();
            this.serviceProvider = serviceProvider;
            RequiredAction = requiredAction;
            ShortDescription = shortDescription;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public string RequiredAction { get; }
        public string ShortDescription { get; }
        public TestResult Result { get; private set; }
        public ICommand RunTestCommand => runTestCommand;
        public virtual bool IsAvailable => true;

        protected void Assert(bool condition) => SetResult(condition ? TestResult.Passed : TestResult.Failed);

        void SetResult(TestResult result)
        {
            Result = result;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            runTestCommand.ChangeCanExecute();
        }

        protected abstract Task DoRunAsync();

        public async Task RunAsync()
        {
            SetResult(TestResult.Running);
            try
            {
                await DoRunAsync();
            }
            catch (Exception ex)
            {
                SetResult(TestResult.Failed);
                logger.LogError(ex, "test {Activation} failed", ShortDescription);
            }
        }
    }
}
