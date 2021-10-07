using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Images
{
    /// <summary>
    /// Style for generating large-format notifications that include a large image attachment.
    /// </summary>
    /// <remarks>
    /// Portions of this page are reproduced from work created and shared by
    /// the Android Open Source Project and used according to terms described
    /// in the Creative Commons 2.5 Attribution License. 
    /// </remarks>
    public interface IBigPictureStyle : IDroidStyleBuilder
    {
        /// <summary>
        /// Override the large icon when the big notification is shown. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IBigPictureStyle BigLargeIcon(ToastImageSource b);
        /// <summary>
        /// Provide the bitmap to be used as the payload for the BigPicture notification. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IBigPictureStyle BigPicture(ToastImageSource b);
        /// <summary>
        /// Overrides ContentTitle in the big form of the template. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IBigPictureStyle SetBigContentTitle(string title);
        /// <summary>
        /// Set the first line of text after the detail section in the big form of the template. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IBigPictureStyle SetSummaryText(string cs);

    }
}
