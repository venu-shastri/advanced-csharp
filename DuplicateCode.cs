using System;
using System.Collections.Generic;

namespace Code
{
    
   public  class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Philips", "Siemens", "Abb", "Ge" };
          List<string> result= SearchArray<string>(names, new Func<string, bool>(Program.isStringEndswiths));
            result = SearchArray<string>(names, new Func<string, bool>(Program.isStringEndswithe));
            result = SearchArray<string>(names, new Func<string, bool>(Program.isStringEndswithb));


        }
         public static bool isStringEndswiths(string item)
        {
           
            return item.EndsWith("s");
        }
        public static bool isStringEndswithb(string item)
        {

            return item.EndsWith("b");
        }
        public static bool isStringEndswithe(string item)
        {

            return item.EndsWith("e")
        }

        static List<T> SearchArray<T>(T[] source,Func<T,bool> functionObj)
        {
            List<T> result = new List<T>();
            for(int i = 0; i < source.Length; i++)
            {
                if (functionObj.Invoke(source[i]))
                {
                    result.Add(source[i]);
                }
            }

            return result;
        }
       

    }

   
}
