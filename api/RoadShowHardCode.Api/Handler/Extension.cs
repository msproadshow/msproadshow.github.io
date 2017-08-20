namespace Arvi.Omt.Api.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    /// <summary>
    /// The extension.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// The to pairs.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static IEnumerable<KeyValuePair<string, string>> ToPairs(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return collection.AllKeys.Select(key => new KeyValuePair<string, string>(key, collection[(string)key]));
        }
    }
}