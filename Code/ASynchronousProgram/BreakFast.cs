using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASynchronousProgram
{
   
        class Program
        {
            static void Main(string[] args)
            {
          
            //PrepareBreakFastSynchronously();
            // PrepareBreakFastAsynchronously();
            //PrepareBreakfastAsyncUsingStateMachine();
            //PrepareBreakfastAsyncUsingAwaitEfficiently_v1();
            PrepareBreakfastAsyncUsingAwaitEfficiently_v2();
            while (true)
            {
                Console.WriteLine("Main Continued");
                System.Threading.Thread.Sleep(2000);
            }


            }

         static void PrepareBreakFastSynchronously()
         {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Egg eggs = FryEggs(2);
            Console.WriteLine("eggs are ready");

            Bacon bacon = FryBacon(3);
            Console.WriteLine("bacon is ready");


            Toast toast = ToastBread(2);
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("toast is ready");

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");

        }

         static void PrepareBreakFastAsynchronously()
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            Task<Egg> eggTask = new Task<Egg>((object obj) => { return FryEggs(Convert.ToInt32(obj)); }, 2,System.Threading.CancellationToken.None, TaskCreationOptions.None);
            eggTask.Start();
            eggTask.Wait();
           Egg eggs =eggTask.Result;
            //Egg eggs = FryEggs(2);
            Console.WriteLine("eggs are ready");

            Task<Bacon> baconTask= new Task<Bacon>((object obj) => { return FryBacon(Convert.ToInt32(obj)); }, 3, System.Threading.CancellationToken.None, TaskCreationOptions.None);
            baconTask.Start();
            baconTask.Wait();
           Bacon bacon= baconTask.Result;
            // Bacon bacon = FryBacon(3);
           Console.WriteLine("bacon is ready");

            Task<Toast> toastTask = new Task<Toast>((object obj) => { return ToastBread(Convert.ToInt32(obj)); }, 2, System.Threading.CancellationToken.None, TaskCreationOptions.None);
            toastTask.Start();
            toastTask.Wait();
           Toast toast= toastTask.Result;
            //Toast toast = ToastBread(2);
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("toast is ready");

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        /*
         * async - Verify Method Signature
         *              should return either void  | Task | Task<Rt>
         *         async method allowed use "await' statements
         */ 
          static async void PrepareBreakfastAsyncUsingStateMachine()
        {

            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            //Episode 
            Egg eggs = await FryEggsAsync(2);
            Console.WriteLine("eggs are ready");

            Bacon bacons = await FryBaconAsync(3);
            Console.WriteLine("Bacons  are ready");

            Toast toasts = await ToastBreadAsync(2);
            ApplyButter(toasts);
            ApplyJam(toasts);
            Console.WriteLine("Toats   are ready");

            Juice _juice = PourOJ();
            Console.WriteLine("Juice is  ready");

            Console.WriteLine("BreakFast is Ready");





        }

        static async void PrepareBreakfastAsyncUsingAwaitEfficiently_v1()
        {

            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            //Episode 
            Task<Egg>   fryEggsTaskRef= FryEggsAsync(2);
            Task<Bacon> fryBaconsTaskRef =FryBaconAsync(3);
            Task<Toast> toastBreadTaskRef = ToastBreadAsync(2);

            List<Task> breakfastTasks = new List<Task> { fryBaconsTaskRef,fryEggsTaskRef,toastBreadTaskRef };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == fryEggsTaskRef)
                {
                    Console.WriteLine("Eggs are ready");
                }
                else if (finishedTask == fryBaconsTaskRef)
                {
                    Console.WriteLine("Bacon is ready");
                }
                else if (finishedTask == toastBreadTaskRef)
                {
                    Console.WriteLine("Toast is ready");
                }
                breakfastTasks.Remove(finishedTask);
            }


            Juice _juice = PourOJ();
            Console.WriteLine("Juice is  ready");

            Console.WriteLine("BreakFast is Ready");





        }
        static async void PrepareBreakfastAsyncUsingAwaitEfficiently_v2()
        {

            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            //Episode 
            Task<Egg> fryEggsTaskRef = FryEggsAsync(2);
            Task<Bacon> fryBaconsTaskRef = FryBaconAsync(3);
            Task<Toast> toastBreadTaskRef = ToastBreadAsync(2);

            List<Task> breakfastTasks = new List<Task> { fryBaconsTaskRef, fryEggsTaskRef, toastBreadTaskRef };
            await Task.WhenAll(breakfastTasks.ToArray());
            Console.WriteLine("Eggs , Bacon , ToastBread is Ready");
            Juice _juice = PourOJ();
            Console.WriteLine("Juice is  ready");

            Console.WriteLine("BreakFast is Ready");





        }

        private static Juice PourOJ()
            {
                Console.WriteLine("Pouring orange juice");
                return new Juice();
            }

            private static void ApplyJam(Toast toast) =>
                Console.WriteLine("Putting jam on the toast");

            private static void ApplyButter(Toast toast) =>
                Console.WriteLine("Putting butter on the toast");

        private static Task<Toast> ToastBreadAsync(int slices)
        {
            return Task.Run<Toast>(() => {
                return ToastBread(slices);
            });
        }
            private static Toast ToastBread(int slices)
            {
                for (int slice = 0; slice < slices; slice++)
                {
                    Console.WriteLine("Putting a slice of bread in the toaster");
                }
                Console.WriteLine("Start toasting...");
                Task.Delay(3000).Wait();
                Console.WriteLine("Remove toast from toaster");

                return new Toast();
            }

        private static  Task<Bacon> FryBaconAsync(int slices)
        {
            return Task.Run<Bacon>(() => {

                return FryBacon(slices);
            });
        }
            private static Bacon FryBacon(int slices)
            {
                Console.WriteLine($"putting {slices} slices of bacon in the pan");
                Console.WriteLine("cooking first side of bacon...");
                Task.Delay(3000).Wait();
                for (int slice = 0; slice < slices; slice++)
                {
                    Console.WriteLine("flipping a slice of bacon");
                }
                Console.WriteLine("cooking the second side of bacon...");
                Task.Delay(3000).Wait();
                Console.WriteLine("Put bacon on plate");

                return new Bacon();
            }

            private static Task<Egg> FryEggsAsync(int howMany) {

            return Task.Run<Egg>(() => {
                return  FryEggs(howMany);
            });
            
        }
            private static Egg FryEggs(int howMany)
            {
                Console.WriteLine("Warming the egg pan...");
                Task.Delay(3000).Wait();
                Console.WriteLine($"cracking {howMany} eggs");
                Console.WriteLine("cooking the eggs ...");
                Task.Delay(3000).Wait();
                Console.WriteLine("Put eggs on plate");

                return new Egg();
            }

            private static Coffee PourCoffee()
            {
                Console.WriteLine("Pouring coffee");
                return new Coffee();
            }
        }

    internal class Coffee
    {
    }
}

