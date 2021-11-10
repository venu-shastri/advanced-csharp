using System;
using System.Collections.Generic;

namespace Code
{
    
   public  class Program
    {
         static void Main(string[] args)
        {
            string[] names = { "Philips", "Siemens", "Abb", "Ge" };
          List<string> result= SearchArray<string>(names, isStringEndsWithParam("s"));
            result = SearchArray<string>(names,isStringEndsWithParam("e"));
            result = SearchArray<string>(names,isStringEndsWithParam("b"));
        }

        static Func<string,bool> isStringEndsWithParam(string endsWith)
        {
            //Func<string, bool> funCommandObj = 
            //    (/* Argument List */string item) => {
                    
            //        /*Method Body*/ return item.EndsWith(endsWith);
            //    };
            //return funCommandObj;
            bool InnerFunction(string item)
            {
                return item.EndsWith(endsWith);
            }
            return InnerFunction;
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
