using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface ISnackbarExtension : INotificationBuilderExtension<ISnackbarExtension>
    {
        ISnackbarExtension WithAction(string actionText);
        ISnackbarExtension WithAction(string actionText, int colorResource);
        ISnackbarExtension WithDuration(int duration);
        ISnackbarExtension WithDuration(SnackbarDuration duration);
    }
}
