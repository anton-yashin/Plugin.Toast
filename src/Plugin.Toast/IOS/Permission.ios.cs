using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class Permission : IPermission, IInitialization
    {
        private readonly IToastOptions toastOptions;
        bool approved;
        IDictionary<string, string>? lastError;

        public Permission(IToastOptions toastOptions) => this.toastOptions = toastOptions;

        public Task InitializeAsync() => RequestAuthorizationAsync();

        public async Task RequestAuthorizationAsync()
        {
            if (this.approved)
                return;
            if (this.lastError != null && toastOptions.MultipleAuthorizationRequests == false)
                throw new Exceptions.NotificationException("not authorized", lastError);
            var (approved, err) = await UNUserNotificationCenter.Current.RequestAuthorizationAsync(UNAuthorizationOptions.Alert);
            this.approved = approved;
            if (approved == false)
            {
                lastError = ToDictionary(err);
                throw new Exceptions.NotificationException("not authorized", lastError);
            }
        }

        IDictionary<string, string> ToDictionary(NSError? error)
        {
            var result = new Dictionary<string, string>();
            if (error != null)
            {
                result.Add(nameof(error.LocalizedRecoveryOptions), string.Join("||", error.LocalizedRecoveryOptions));
                result.Add(nameof(error.LocalizedRecoverySuggestion), error.LocalizedRecoverySuggestion);
                result.Add(nameof(error.HelpAnchor), error.HelpAnchor);
                result.Add(nameof(error.LocalizedDescription), error.LocalizedDescription);
                result.Add(nameof(error.LocalizedFailureReason), error.LocalizedFailureReason);
                result.Add(nameof(error.Domain), error.Domain);
            }
            return result;
        }
    }
}
