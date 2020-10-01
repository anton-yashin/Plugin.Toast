using System;
using System.Runtime.Serialization;

namespace Plugin.Toast
{
    /// <summary>
    /// An identifier that you can obtain after notification is shown or scheduled
    /// See also: 
    /// <seealso cref="IHistory"/><br/>
    /// <seealso cref="INotification.ShowAsync(out ToastId, System.Threading.CancellationToken)"/><br/>
    /// <seealso cref="IScheduledToastCancellation.ToastId"/><br/>
    /// </summary>
    [DataContract]
    public sealed partial class ToastId
    {
        private ToastId() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
    }
}
