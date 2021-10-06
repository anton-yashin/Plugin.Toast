using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Style for generating large-format notifications that include a lot of text.
    /// </summary>
    /// <remarks>
    /// Portions of this page are reproduced from work created and shared by
    /// the Android Open Source Project and used according to terms described
    /// in the Creative Commons 2.5 Attribution License. 
    /// </remarks>
    public interface IBigTextStyle : IDroidStyleBuilder
    {
        /// <summary>
        /// Provide the longer text to be displayed in the big form of the template in place of the content text. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public IBigTextStyle BigText(string cs);
        /// <summary>
        /// Overrides ContentTitle in the big form of the template. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public IBigTextStyle SetBigContentTitle(string title);
        /// <summary>
        /// Set the first line of text after the detail section in the big form of the template. 
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        public IBigTextStyle SetSummaryText(string cs);
    }
}
