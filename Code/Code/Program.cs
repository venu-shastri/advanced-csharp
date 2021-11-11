using System;
using System.Collections.Generic;
using System.Linq;
//using Code.Linq;

namespace Code
{
    
   public  class Program
    {
         static void Main(string[] args)
        {
            int x = 10;
            x.Ext();//Code.Linq.CustomExtensionMethods.Ext(x);
            string[] names = { "Philips", "Siemens", "Abb", "Ge" };

            //IEnumerable<string> result = Search<string>(names, isStringEndsWithParam("s"));
            //result = Search<string>(names,isStringEndsWithParam("e"));
            //result = Search<string>(names,isStringEndsWithParam("b"));
            names.Search(isStringEndsWithParam("s"));//Code.Linq.CustomExtensionMethods.Search(names,isStringEndsWithParam("s"));

            List<string> nameList = new List<string>(names);
            nameList.Search(isStringEndsWithParam("e"));
          
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
       
        //Linq
       
       

    }

   
}
