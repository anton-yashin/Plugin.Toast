using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Style for generating large-format notifications that include a list of (up to 5) strings.
    /// </summary>
    /// <remarks>
    /// Portions of this page are reproduced from work created and shared by
    /// the Android Open Source Project and used according to terms described
    /// in the Creative Commons 2.5 Attribution License. 
    /// </remarks>
    public interface IInboxStyle : IDroidStyleBuilder
    {
        /// <summary>
        /// Append a line to the digest section of the Inbox notification. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IInboxStyle AddLine(string cs);
        /// <summary>
        /// Overrides ContentTitle in the big form of the template.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IInboxStyle SetBigContentTitle(string title);
        /// <summary>
        /// Set the first line of text after the detail section in the big form of the template. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IInboxStyle SetSummaryText(string cs);
    }
}
