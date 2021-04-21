using Google.Android.Material.Snackbar;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    sealed class SnackbarNotification : INotification
    {
        private readonly IToastOptions options;
        private readonly ISnackbarBuilder snackbarBuilder;

        public SnackbarNotification(IToastOptions options, ISnackbarBuilder snackbarBuilder)
        {
            this.options = options;
            this.snackbarBuilder = snackbarBuilder;
        }

        public Task<NotificationResult> ShowAsync(out ToastId toastId, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<NotificationResult>();
            var view = options.Activity.FindViewById(global::Android.Resource.Id.Content);
            var snackbar = Snackbar.Make(view, snackbarBuilder.Text, snackbarBuilder.SnackbarDuration);
            if (snackbarBuilder.ActionText != null)
                snackbar.SetAction(snackbarBuilder.ActionText, v => tcs.TrySetResult(NotificationResult.Activated));
            if (snackbarBuilder.ActionTextColor != null)
                snackbar.SetActionTextColor(snackbarBuilder.ActionTextColor.Value);
            snackbar.AddCallback(new Callback(tcs));
            snackbar.Show();
            toastId = ToastId.New();
            if (cancellationToken.CanBeCanceled)
                return tcs.WatchCancellationAsync(cancellationToken, () => snackbar.Dismiss());
            return tcs.Task;
        }

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            var view = options.Activity.FindViewById(global::Android.Resource.Id.Content);
            var snackbar = Snackbar.Make(view, snackbarBuilder.Text, snackbarBuilder.SnackbarDuration);
            if (snackbarBuilder.ActionText != null)
                snackbar.SetAction(snackbarBuilder.ActionText, v => { });
            if (snackbarBuilder.ActionTextColor != null)
                snackbar.SetActionTextColor(snackbarBuilder.ActionTextColor.Value);
            snackbar.Show();
            return new ScheduledCancellation(snackbar);
        }

        sealed class Callback : Snackbar.Callback
        {
            private readonly HiddenReference<TaskCompletionSource<NotificationResult>> hrTcs;

            public Callback(TaskCompletionSource<NotificationResult> tcs) 
                => this.hrTcs = new HiddenReference<TaskCompletionSource<NotificationResult>>(tcs);

            public override void OnDismissed(Snackbar transientBottomBar, int @event)
            {
                base.OnDismissed(transientBottomBar, @event);
                switch (@event)
                {
                    case DismissEventAction:
                    case DismissEventConsecutive:
                    case DismissEventManual:
                    case DismissEventSwipe:
                        hrTcs.Value.TrySetResult(NotificationResult.UserCanceled);
                        break;
                    case DismissEventTimeout:
                        hrTcs.Value.TrySetResult(NotificationResult.TimedOut);
                        break;
                }
            }
        }

        sealed class ScheduledCancellation : IScheduledToastCancellation
        {
            private readonly Snackbar snackbar;

            public ScheduledCancellation(Snackbar snackbar) => this.snackbar = snackbar;

            public ToastId ToastId { get; } = ToastId.New();

            public void Dispose() => snackbar.Dismiss();
        }
    }
}
