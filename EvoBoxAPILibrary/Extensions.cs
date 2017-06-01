using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    //https://stackoverflow.com/questions/18165460/how-to-search-hierarchical-data-with-linq
    public static class Linq
    {
        public static IEnumerable<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> selector)
        {
            return selector(source).SelectMany(c => Flatten(c, selector))
                                   .Concat(new[] { source });
        }
    }
}
