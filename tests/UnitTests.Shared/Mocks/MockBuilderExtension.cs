using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mocks
{
    sealed class MockBuilderExtension : IBuilderExtension<MockBuilderExtension>
    {
        public MockBuilderExtension Add<T1>(T1 a1)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2>(T1 a1, T2 a2)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension AddDescription(string description)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension AddTitle(string title)
        {
            throw new NotImplementedException();
        }

        public MockBuilderExtension Use(IExtensionConfiguration<MockBuilderExtension> visitor)
        {
            throw new NotImplementedException();
        }
    }
}
