using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ASynchronousProgram
{
    public class PerofrmanceCode
    {

        static void Main()
        {
            string[] words = { "abcd", "efgh", "ijklm", "nopqr", "stuv", "wxyz" };
            System.Diagnostics.Stopwatch _stopWatch = new System.Diagnostics.Stopwatch();
            _stopWatch.Start();
            //for(int i = 0; i < words.Length; i++)
            //{
            //   string encryptedWord= Encrypt(words[i]);
            //    Console.WriteLine(encryptedWord);
            //}
           // List<string> encryptedWords = new List<string>();
            ConcurrentBag<string> encryptedWords = new ConcurrentBag<string>();
            Parallel.For(0, words.Length, (int i) => {
                string encryptedWord = Encrypt(words[i]);
                
                 encryptedWords.Add( encryptedWord);

            });
            //Parallel.ForEach(words, (word) => {
            //    string encryptedWord = Encrypt(word);
            //    Console.WriteLine(encryptedWord);
            //});
            //words.AsParallel().Select((word) => {return  Encrypt(word); }).ForAll((word)=> {
            //    Console.WriteLine(word);
            //});

            _stopWatch.Stop();
            Console.WriteLine(_stopWatch.ElapsedMilliseconds);

        }
        static string Encrypt(string word)
        {
            System.Threading.Thread.Sleep(2000);
            return word.ToUpper();
        }
    }
}
