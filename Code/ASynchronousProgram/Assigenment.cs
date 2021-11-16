using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASynchronousProgram
{
    public class Assigenment
    {


        static void SignDocumnetUsingDS(string documentContent)
        {
           string santizedDocContent= SantizeDocumentContent(documentContent);
           string digest= HashSantizedDocumentContent(santizedDocContent);
           string signedDocumentContent = EncryptDigestUsingPrivateKey(digest);
            Console.WriteLine(signedDocumentContent);
        }

        static void SignDocumnetUsingDSAsync(string documentContent)
        {


        }
       static string SantizeDocumentContent(string documentContent)
        {
            Task.Delay(3000).Wait();
            return documentContent + " Santized ";
        }
        static string HashSantizedDocumentContent(string santizedDocumentContent)
        {
            Task.Delay(3000).Wait();
            return santizedDocumentContent + " Hashed ";
        }
        static string EncryptDigestUsingPrivateKey(string digest)
        {
            Task.Delay(3000).Wait();
            return digest  + " Encrypted ";
        }
    }
}
