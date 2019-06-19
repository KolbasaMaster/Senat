using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenatApi.UnitTests
{
    public static class EnumarableExtensioins
    {
        public static IEnumerable<T> RandomSubset<T>(this IEnumerable<T> set)
        {
            var random = new Random();
            foreach (var item in set)
            {
                if (random.Next(0, 100) >= 50)
                {
                    yield return item;
                }
            }
        }
    }
}
