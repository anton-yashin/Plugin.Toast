using Foundation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class Permission : IPermission, IInitialization
    {
        bool? approved;
        IDictionary<string, string>? lastError;

        public Permission() { }

        public Task InitializeAsync() => RequestAuthorizationAsync();
        public bool IsApproved => approved == true;
        Lazy<Task<Tuple<bool, NSError>>> authorizationRequest = new(()
           => UNUserNotificationCenter.Current.RequestAuthorizationAsync(UNAuthorizationOptions.Alert),
            LazyThreadSafetyMode.ExecutionAndPublication);

        public async Task RequestAuthorizationAsync()
        {
            if (this.approved == true)
                return;
            if (this.approved == false)
                throw new Exceptions.NotificationException("not authorized", lastError ?? new Dictionary<string, string>());
            var (approved, err) = await authorizationRequest.Value;
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
