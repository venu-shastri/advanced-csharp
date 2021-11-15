using System;
using System.Threading;

namespace ConsoleApp1
{
    
    public class DBWriter
    {
        object _syncObjForWriters = new object();
        object _syncObjForLog = new object();
        //public void Insert(DBWriter this){}
        public void Insert() {
            Console.WriteLine("Insert Started...");
            try
            {
                Monitor.Enter(_syncObjForWriters);
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Inserting Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    System.Threading.Thread.Sleep(1000);
                    if (i == 2) { return; }
                }
                
            }
            finally
            {
                Monitor.Exit(_syncObjForWriters);
            }
            Console.WriteLine("Insert End...");
        }
    
        public void Update() {
            //public void Update(DBWriter this){}
            Console.WriteLine("Update Started...");
            //Monitor.Enter(_syncObjForWriters);
            lock (_syncObjForWriters)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Updating Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            //Monitor.Exit(_syncObjForWriters);
            Console.WriteLine("Update End...");

        }
        //public void Delete(DBWriter this){}
        public void Delete() {
            Console.WriteLine("Delete Started...");
            Monitor.Enter(_syncObjForWriters);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Deleting Data...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
            Monitor.Exit(_syncObjForWriters);
            Console.WriteLine("Delete End...");
        }
        public void Log()
        {
            Monitor.Enter(_syncObjForLog);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Logging  Operation...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
            Monitor.Exit(_syncObjForLog);

        }

    }
    class Program
    {
        static ManualResetEvent _waitHanlde = new ManualResetEvent(false);
        static AutoResetEvent _waitHanldeBgTask1 = new AutoResetEvent(false);
        static AutoResetEvent _waitHanldeBgTask2 = new AutoResetEvent(false);
        static AutoResetEvent _waitHanldeBgTask3 = new AutoResetEvent(false);

        static void Main_old(string[] args)
        {
            Console.WriteLine($"Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            System.Threading.ThreadStart _sendSmsFunRef = new System.Threading.ThreadStart(Program.SendSms);
            System.Threading.Thread _smsThread = new System.Threading.Thread(_sendSmsFunRef);
            
            _smsThread.Start();
            //SendSms();
            //System.Threading.ThreadStart _sendEmailFunRef = new System.Threading.ThreadStart(Program.SendEmail);
            //System.Threading.Thread _emailThread = new System.Threading.Thread(_sendEmailFunRef);
            //_emailThread.IsBackground = true;
            //_emailThread.Start();
            System.Threading.WaitCallback _emailMethodRef = new System.Threading.WaitCallback(SendEmailWrapper);
            System.Threading.ThreadPool.QueueUserWorkItem(_emailMethodRef);
            //SendEmail();
            Console.WriteLine($"End Of Main method -Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");

        }

        static void Main_new_old()
        {
            DBWriter _dbWriterRef = new DBWriter();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Insert)).Start() ;
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Update)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Delete)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Log)).Start();
            new System.Threading.Thread(new ThreadStart(_dbWriterRef.Log)).Start();
        }
        static void SendSms() { 
        
            for(int i = 0; i < 10; i++) {
                Console.WriteLine($"Sending Sms...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }
        static void SendEmailWrapper(object arg)
        {
            SendEmail();
        }
        static void SendEmail() {
        for(int i = 0; i < 20; i++)
            {
                Console.WriteLine($"Sending Email...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }

        static void Main()
        {
            //WaitCallback _bgTaskAddress = new WaitCallback(BgTask);
            //ThreadPool.QueueUserWorkItem(_bgTaskAddress);
            ThreadPool.QueueUserWorkItem(BgTaskOne);
            ThreadPool.QueueUserWorkItem(BgTaskTwo);
            ThreadPool.QueueUserWorkItem(BgTaskThree);
            Thread.Sleep(3000);
            _waitHanlde.Set();
            WaitHandle.WaitAny(new WaitHandle[] { _waitHanldeBgTask1,_waitHanldeBgTask2,_waitHanldeBgTask3 });
            Console.WriteLine("Main Completed");
        }
        static void BgTask(object args)
        {
            for(int i = 0; i < 20; i++)
            {
                Console.WriteLine($"BgTask...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }


        static void BgTaskOne(object args)
        {
            Console.WriteLine($"BgTask1...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId} Awaiting For Signal from Main Thread");
            _waitHanlde.WaitOne();
            
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"BgTask1...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
                //5 iteration
                if (i == 5)
                {
                    _waitHanldeBgTask1.Set();

                }
            }
        }
        static void BgTaskTwo(object args)
        {
          
            Console.WriteLine($"BgTask2...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId} Awaiting For Signal from Main Thread");
            _waitHanlde.WaitOne();
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"BgTask2...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
                //10
                if (i == 10) { _waitHanldeBgTask2.Set(); }
            }
        }
        static void BgTaskThree(object args)
        {
            
            Console.WriteLine($"BgTask3...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId} Awaiting For Signal from Main Thread");
            _waitHanlde.WaitOne();
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"BgTask3...-Thread Id {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
                //12
                if (i == 12)
                {
                    _waitHanldeBgTask3.Set();

                }
            }
        }

    }
}
