using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASynchronousProgram
{
    class Assignment
    {
        static void Main_()
        {
            Console.WriteLine("Main started");
            string content = "Hello World!!";
            //SanitizeDocumentContentAsync(content);
            SignDocumnetUsingDSAsync(content);

            while (true)
            {
                Console.WriteLine("Main Continued");
                System.Threading.Thread.Sleep(2000);
            }
        }

        static void SignDocumnetUsingDS(string documentContent)
        {
            string santizedDocContent = SantizeDocumentContent(documentContent);
            string digest = HashSantizedDocumentContent(santizedDocContent);
            string signedDocumentContent = EncryptDigestUsingPrivateKey(digest);
            Console.WriteLine(signedDocumentContent);

        }

        static async void SignDocumnetUsingDSAsync(string documentContent)
        {
            string sanitizedDocContent = await SanitizeDocumentContentAsync(documentContent);
            Console.WriteLine(sanitizedDocContent);

            string digest = await HashSantizedDocumentContentAsync(sanitizedDocContent);
            Console.WriteLine(digest);

            string signedDocumentContent = await EncryptDigestUsingPrivateKeyAsync(digest);
            Console.WriteLine(signedDocumentContent);

        }

        static Task<string> SanitizeDocumentContentAsync(string documentContent)
        {
            return Task.Run<string>(() => { return SantizeDocumentContent(documentContent); });
        }
        static string SantizeDocumentContent(string documentContent)
        {
            Task.Delay(3000).Wait();
            return documentContent + " Santized ";
        }

        static Task<string> HashSantizedDocumentContentAsync(string santizedDocumentContent)
        {
            return Task.Run<string>(() => { return HashSantizedDocumentContent(santizedDocumentContent); });
        }
        static string HashSantizedDocumentContent(string santizedDocumentContent)
        {
            Task.Delay(3000).Wait();
            return santizedDocumentContent + " Hashed ";
        }

        static Task<string> EncryptDigestUsingPrivateKeyAsync(string digest)
        {
            return Task.Run<string>(() => { return EncryptDigestUsingPrivateKey(digest); });
        }
        static string EncryptDigestUsingPrivateKey(string digest)
        {
            Task.Delay(3000).Wait();
            return digest + " Encrypted ";
        }
    }
}
