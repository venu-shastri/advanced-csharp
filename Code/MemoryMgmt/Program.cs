using System;

namespace MemoryMgmt
{
    public class A {

        B obj = new B();
    }
    public class C { }
    public class B { }
    class Program
    {
        static A globalRefForA;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Allocate();
         
            GC.Collect();
        }
        static void Allocate()
        {
            A obj = new A();
           
           

            byte[] largeObject = new byte[86000];
            System.Collections.ArrayList _arrayList = new System.Collections.ArrayList();
            _arrayList.Add(obj);
            globalRefForA = obj;


            // Console.WriteLine("Object A Generation " +GC.GetGeneration(obj));//0
            // Console.WriteLine("Byte Array Object Generation " + GC.GetGeneration(largeObject));//2

            obj = null;
            GC.Collect();
            //Console.WriteLine("Object A Generation " + GC.GetGeneration(obj));//1
            //Console.WriteLine("Byte Array Object Generation " + GC.GetGeneration(largeObject));//2
            _arrayList.Clear();
            GC.Collect();
            //Console.WriteLine("Object A Generation " + GC.GetGeneration(obj));//2
            //Console.WriteLine("Byte Array Object Generation " + GC.GetGeneration(largeObject));//2
         
            GC.Collect(0);
            C c_obj = new C();
            c_obj = null;
            GC.Collect(2,GCCollectionMode.Forced);

            GC.Collect();
            GC.WaitForFullGCComplete();
            
            //Console.WriteLine("Object A Generation " + GC.GetGeneration(obj));//2
            //Console.WriteLine("Byte Array Object Generation " + GC.GetGeneration(largeObject));//2


        }
    }
}
