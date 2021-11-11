using System;

namespace PicTestRunner
{
    class Program
    {
        static void Main(string[] args)
        {

            string codeUnderTestLibPath = @"C:\Users\user\source\repos\Code\CalculatorLib.Tests\bin\Debug\net5.0\CalculatorLib.Tests.dll";
            //Load Library
          System.Reflection.Assembly _library=  System.Reflection.Assembly.LoadFile(codeUnderTestLibPath);
            //Search For public Classes - Annotated -> TestSuiteAttribute
           System.Type[] types= _library.GetTypes();
            for(int i = 0; i < types.Length; i++)
            {
                if(types[i].IsClass && types[i].IsPublic)
                {
                  PicTestLib.TestSuiteAttribute[] testSuiteAttributes= types[i].
                        GetCustomAttributes(typeof(PicTestLib.TestSuiteAttribute), true) 
                        as PicTestLib.TestSuiteAttribute[];
                    if (testSuiteAttributes.Length >= 1)
                    {
                        Console.WriteLine($"TestSuiteClassName {types[i].FullName} ,TestSuiteName {testSuiteAttributes[i].Name}");
                        //Search For Test Methods (public , return ->void , 0 arguments, non static)
                        // Annotate -> TestAttribute
                      System.Reflection.MethodInfo[] methods=
                            types[i].GetMethods(System.Reflection.BindingFlags.Public | 
                            System.Reflection.BindingFlags.Instance);
                        object instance = System.Activator.CreateInstance(types[i]);
                        for(int j = 0; j < methods.Length; j++)
                        {
                            if(methods[j].ReturnType==typeof(void) && methods[j].GetParameters().Length == 0)
                            {
                                //TestAttribute based
                              PicTestLib.TestAttribute[] testAttributes=  methods[j].GetCustomAttributes(typeof(PicTestLib.TestAttribute), true) as PicTestLib.TestAttribute[];
                                if (testAttributes.Length >= 1)
                                {
                                    Console.WriteLine($"Method Name {methods[j].Name}, Test Method Name {testAttributes[0].Name}");
                                    methods[j].Invoke(instance,new object[] { });
                                }
                            }

                        }
                    }
                }
            }

            
        }
    }
}
