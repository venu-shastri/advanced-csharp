using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
 public   static class CustomExtensionMethods
    {
        public static IEnumerable<T> Search<T>(this IEnumerable<T> source, Func<T, bool> functionObj)
        {
            List<T> result = new List<T>();
            foreach (T item in source)
            {
                if (functionObj.Invoke(item))
                {
                    result.Add(item);
                }

            }
            //IEnumerator<T> iterator= source.GetEnumerator();
            // try
            // {
            //     while (iterator.MoveNext())
            //     {
            //         T item=iterator.Current;
            //         if (functionObj.Invoke(item))
            //         {
            //             result.Add(item);
            //         }
            //     }
            // }
            // finally
            // {
            //     if (iterator is IDisposable)
            //     {
            //         iterator.Dispose();
            //     }
            // }


            return result;
        }

        public static void Ext(this int x) { }
    }
}
