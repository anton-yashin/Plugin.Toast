using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Plugin.Toast.Exceptions
{
    public sealed class NotificationException : Exception
    {
        static readonly IReadOnlyDictionary<string, string> EmptyData
            = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

        public NotificationException()
        {
        }

        public NotificationException(string message) : base(message)
        {
        }

        public NotificationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotificationException(IDictionary<string, string> additionalData)
        {
            AdditionalData = new ReadOnlyDictionary<string, string>(additionalData);
        }

        public NotificationException(string message, IDictionary<string, string> additionalData) : base(message)
        {
            AdditionalData = new ReadOnlyDictionary<string, string>(additionalData);
        }

        public NotificationException(string message,
                                     Exception innerException,
                                     IDictionary<string, string> additionalData) : base(message, innerException)
        {
            AdditionalData = new ReadOnlyDictionary<string, string>(additionalData);
        }

        /// <summary>
        /// Platform dependent additional data. Key is field name, value is field value.
        /// </summary>
        public IReadOnlyDictionary<string, string> AdditionalData { get; } = EmptyData;
    }
}
