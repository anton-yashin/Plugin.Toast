using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Abstracton layer for android person builder.
    /// </summary>
    public interface IDroidPersonBuilder
    {
        /// <summary>
        /// Sets whether or not this Person represents a machine rather than a human. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidPersonBuilder SetBot(bool bot);
        /// <summary>
        /// Set an icon for this Person.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidPersonBuilder SetIcon(ToastImageSource icon);
        /// <summary>
        /// Sets whether this is an important person. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidPersonBuilder SetImportant(bool important);
        /// <summary>
        /// Set a unique identifier for this Person.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidPersonBuilder SetKey(string key);
        /// <summary>
        /// Give this Person a name to use for display. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidPersonBuilder SetName(string name);
        /// <summary>
        /// Set a URI for this Person which can be any of the following:
        /// <list type="bullet">
        /// <item>
        /// The String representation of a
        /// <see href="https://developer.android.com/reference/android/provider/ContactsContract.Contacts.html#CONTENT_LOOKUP_URI">ContactsContract.Contacts.CONTENT_LOOKUP_URI</see>
        /// </item>
        /// <item>
        /// A mailto: schema*
        /// </item>
        /// <item>
        /// A tel: schema*
        /// </item>
        /// </list>
        /// * Note for these schemas, the path portion of the URI must exist in the
        /// contacts database in their appropriate column, otherwise the reference will be discarded. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidPersonBuilder SetUri(string uri);

#if __ANDROID__
        /// <summary>
        /// Build android <see cref="AndroidX.Core.App.Person"/>
        /// </summary>
        AndroidX.Core.App.Person Build();
#endif 
    }
}
