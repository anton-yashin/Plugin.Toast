using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Plugin.Toast.Exceptions
{
    /// <summary>
    /// Exception that thrown when the library can't show a notification.
    /// For example: application has no permission on iOS.
    /// </summary>
    public sealed class NotificationException : Exception
    {
        static readonly IReadOnlyDictionary<string, string> EmptyData
            = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationException"/> class.
        /// </summary>
        public NotificationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes error.</param>
        public NotificationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception or null.</param>
        public NotificationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationException"/> class.
        /// </summary>
        /// <param name="additionalData">The data that represents platform error.</param>
        public NotificationException(IDictionary<string, string> additionalData)
        {
            AdditionalData = new ReadOnlyDictionary<string, string>(additionalData);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes error.</param>
        /// <param name="additionalData">The data that represents platform error.</param>
        public NotificationException(string message, IDictionary<string, string> additionalData) : base(message)
        {
            AdditionalData = new ReadOnlyDictionary<string, string>(additionalData);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationException"/> class.
        /// </summary>
        /// <param name="message">The message that describes error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception or null.</param>
        /// <param name="additionalData">The data that represents platform error.</param>
        public NotificationException(string message,
                                     Exception innerException,
                                     IDictionary<string, string> additionalData) : base(message, innerException)
        {
            AdditionalData = new ReadOnlyDictionary<string, string>(additionalData);
        }

        /// <summary>
        /// The data that represents platform error. Key is field name, value is field value.
        /// </summary>
        public IReadOnlyDictionary<string, string> AdditionalData { get; } = EmptyData;
    }
}
