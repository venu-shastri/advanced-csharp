using System;

namespace ResourceMgmt
{
    public static class Signals
    {
        public static System.Threading.AutoResetEvent ResourceAvailablityHandle = new System.Threading.AutoResetEvent(false);
    }
    public class Resource
    {
        public static readonly Resource Instance;

        static Resource()
        {
            Instance = new Resource();
        }
        public  bool IsBusy=false;
        private  Resource()
        {
            
        }
        public void UseResource()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Using Resouce By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }

    public class ResourceWrapper:IDisposable
    {
        
        public ResourceWrapper()
        {
            lock (Signals.ResourceAvailablityHandle)
            {
                if (Resource.Instance.IsBusy)
                {
                    //Wait.....
                    Console.WriteLine($"Thread {System.Threading.Thread.CurrentThread.ManagedThreadId} awaiting For Resource");
                    Signals.ResourceAvailablityHandle.WaitOne();
                    Console.WriteLine($"Resouce Owned By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    Resource.Instance.IsBusy = true;
                }
                else
                {

                    Console.WriteLine($"Resouce Owned By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    Resource.Instance.IsBusy = true;
                }
            }

        }
        public void Operation()
        {
            Resource.Instance.UseResource();
        }
        public void Dispose()
        {
            Resource.Instance.IsBusy = false;
            Signals.ResourceAvailablityHandle.Set();
            Console.WriteLine($"Resouce Released By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            new System.Threading.Thread(Client1).Start();
            new System.Threading.Thread(Client1).Start();
        }

        static void Client11()
        {

            //ResourceWrapper _resourceWrapper = null;
            //try
            //{
            //    _resourceWrapper = new ResourceWrapper();
            //    _resourceWrapper.Operation();
            //}
            //finally
            //{
            //    if (_resourceWrapper is IDisposable)
            //    {
            //        _resourceWrapper.Dispose();
            //    }
            //}
            ////_resourceWrapper.Dispose();
            //_resourceWrapper = null; //mark for collection
            using(ResourceWrapper resourceWrapper=new ResourceWrapper())
            {
                resourceWrapper.Operation();
            }
            GC.Collect();

        }

        static void Client1()
        {
            ResourceWrapper _rw = new ResourceWrapper();
            try
            {
                _rw.Operation();
                _rw.Dispose();
            }
            catch(Exception ex)
            {
                _rw.Dispose();
            }
            finally
            {
                _rw.Dispose();
            }
            
            
            _rw.Operation();//Restrict
        }
    }
}
