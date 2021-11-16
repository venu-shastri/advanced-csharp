using System;
using System.Threading;
using System.Threading.Tasks;

namespace ASynchronousProgram
{
    class Program_old
    {
        static void Main_old(string[] args)
        {
            Console.WriteLine("Hello World!");
            //string fileContent= GetFileContent();//Synchronous Call
            // Console.WriteLine(fileContent);
            Task _faultTask = new Task(FaultTask);
            _faultTask.Start();
            try
            {
                _faultTask.Wait();
            }
            catch(AggregateException ex)
            {
               
                
                Console.WriteLine(ex.InnerException.Message);
            }
            
        }
        static void SearchAndTimerCode()
        {

            Task<string> _searchTask = new Task<string>(GetFileContent);
            Task _timerTask = new Task(RunTimer);

            //_searchTask.Start();
            //_timerTask.Start();

            Console.WriteLine("Statement N....");
            Console.WriteLine(_searchTask.Status);
            Thread.Sleep(2000);
            Console.WriteLine(_searchTask.Status);
            //do
            //{

            //    Console.WriteLine(_searchTask.Status);
            //    Thread.Sleep(1000);
            //} while (_searchTask.Status != TaskStatus.RanToCompletion);

            //_searchTask.Wait();
            string taskReturnValue = _searchTask.Result;
            Console.WriteLine(taskReturnValue);
            Console.WriteLine(_searchTask.Status);
            _timerTask.Wait();
        }

        static string GetFileContent()
        {
            //File Read
            Thread.Sleep(5000);
            return "File Content ";

        }

        static void RunTimer()
        {
            while (true)
            {
                Console.WriteLine("Timer Tick....");
                Thread.Sleep(1000);
            }
        }

        static void FaultTask()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine("FaultTask Running...");
                if (i == 5)
                {
                    throw new InvalidOperationException("Unable to proceed further.....");
                }
                Thread.Sleep(1000);
            }
        }
    }
}
