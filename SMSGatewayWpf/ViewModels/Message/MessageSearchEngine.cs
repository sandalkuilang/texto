using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.ViewModels.Message
{
    public static class MessageSearchEngine
    {
        /// <summary>
        /// Filters the source by the specified search text.
        /// </summary>
        /// <param name="source">The songs to search.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns>The filtered sequence of songs.</returns>
        public static IList<T> Filter<T>(IList<T> source, string searchText) where T : BaseMessageModel
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
                            item.Message.ContainsIgnoreCase(keyword) ||
                            item.Sender.ContainsIgnoreCase(keyword)
                    )
                ).ToList();
        }
         
        private static bool ContainsIgnoreCase(this string value, string other)
        {
            return value.IndexOf(other, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}
