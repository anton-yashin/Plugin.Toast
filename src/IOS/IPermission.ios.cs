using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    public interface IPermission
    {
        /// <summary>
        /// This function invokes <see cref="UNUserNotificationCenter.Current.RequestAuthorizationAsync(UNAuthorizationOptions)"/>
        /// </summary>
        /// <exception cref="Exceptions.NotificationException">
        /// When request isn't approved. See additional data for reason.
        /// </exception>
        Task RequestAuthorizationAsync();
    }
}