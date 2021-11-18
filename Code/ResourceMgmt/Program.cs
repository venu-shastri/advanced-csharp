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
        private bool disposedValue;

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
            if (disposedValue)
            {
                throw new ObjectDisposedException("ResourceWrapper");
            }
            Resource.Instance.UseResource();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    Console.WriteLine($"Resouce Released Using Dispose Method , By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                }
                
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                Resource.Instance.IsBusy = false;
                Signals.ResourceAvailablityHandle.Set();

                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
         ~ResourceWrapper()
         {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method

            Dispose(disposing: false);
            Console.WriteLine($"Resouce Released Using Finalize Method , By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
     

    }
    class Program
    {
        static void Main(string[] args)
        {
            new System.Threading.Thread(Client1).Start();
            new System.Threading.Thread(Client1).Start();
            while (true)
            {
                GC.Collect();
                System.Threading.Thread.Sleep(3000);
            }
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

        static void Client111()
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

        static void Client1()
        {
            ResourceWrapper _rw = new ResourceWrapper();
            try
            {
                _rw.Operation();
              //  _rw.Dispose();
               // _rw.Operation();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
               // _rw.Dispose();
            }
            finally
            {
              //  _rw.Dispose();
            }
            _rw = null;
          

        }
    }
}
