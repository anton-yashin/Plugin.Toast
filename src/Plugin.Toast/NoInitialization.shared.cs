using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed class NoInitialization : IInitialization
    {
        public Task InitializeAsync() => Task.CompletedTask;
    }
}
