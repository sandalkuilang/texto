using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Contact
{
    public static class ContactSearchEngine
    {
        /// <summary>
        /// Filters the source by the specified search text.
        /// </summary>
        /// <param name="source">The songs to search.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns>The filtered sequence of songs.</returns>
        public static IList<T> Filter<T>(IList<T> source, string searchText) where T : BaseContactModel
        {
            if (String.IsNullOrWhiteSpace(searchText))
                return source;

            IEnumerable<string> keyWords = searchText.Split(' ');

            return source
                .AsParallel()
                .Where
                (
                    item => keyWords.All
                    (
                        keyword =>
                            item.Name.ContainsIgnoreCase(keyword) ||
                            item.PhoneNumber.ContainsIgnoreCase(keyword)
                    )
                ).ToList();
        }

        private static bool ContainsIgnoreCase(this string value, string other)
        {
            return value.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}
