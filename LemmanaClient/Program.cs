using System;
using LemmanaClient.API;
using System.Linq;
using LemmanaClient.Models;

/*
 * Lemmana API Sample Code - NOT FOR PRODUCTION USE
 * This program is intended to assist in the development process for working with the Lemmana REST service
 * It is up to the program developer to test any implementation they use in production.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE.
 * 
 * Makes use of NewtonSoft for JSON De-Serialization : License details - https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md
 * Makes use of RestSharp for HTTP Client : License details - https://github.com/restsharp/RestSharp/blob/master/LICENSE.txt
 * Use of PostMan is recommended for building and testing the API endpoints : https://www.getpostman.com/
 * 
 * Please ensure you/your company have the legal rights to use NewtonSoft, RestSharp and Postman in your software builds. 
 * This is not the responsibility of Lemmana.
 */

namespace LemmanaClient
{
    class Program
    {
      
        static void Main(string[] args)
        {
            // obtain your token from your account page on https://api.lemmana.com
			string token = "";

            // create our URL object
            URL url = new URL("https://api.lemmana.com");

            // upload a document
            UploadDocumentResponse uploadResponse = UploadDocument.uploadFile(url, token, 30000, false, true, true, "./TestFiles/Test.pdf");

            // deal with the response
            if (uploadResponse.statusCode == "Created")
            {
                Console.Out.WriteLine("\n---------------------------------------------------------------------------");
                Console.Out.WriteLine("Status Code    : " + uploadResponse.statusCode);
                Console.Out.WriteLine("Classification : " + uploadResponse.document.classification.name);
                Console.Out.WriteLine("Document ID    : " + uploadResponse.document.id);
                Console.Out.WriteLine("---------------------------------------------------------------------------\n");

                var entities = uploadResponse.document.classification.entities;

                foreach(Entity e in entities)
                {
                    Console.Out.WriteLine("Name        : " + e.name + "\nValue       : " + e.value + "\nProbability : " + e.probability + "\nMultiple    : " + e.multiple + "\n"); 
                }
            }
            else
            {
                Console.Out.WriteLine("\n\nError Uploading. Exiting.");
                Console.In.ReadLine();
                return;
            }

            GetDocumentsResponse getDocumentsResponse = GetDocuments.getDocuments(url, token, 10000);

            // deal with the response
            if (getDocumentsResponse.statusCode == "OK")
            {
                Console.Out.WriteLine("\n---------------------------------------------------------------------------");
                Console.Out.WriteLine("Status Code     : " + getDocumentsResponse.statusCode);
                Console.Out.WriteLine("No of Documents : " + getDocumentsResponse.document.Count());
                Console.Out.WriteLine("---------------------------------------------------------------------------\n");

                var documents = getDocumentsResponse.document;

                foreach (Document document in documents)
                {
                    Console.Out.WriteLine("Name           : " + document.fileName);
                    Console.Out.WriteLine("Classification : " + document.classification.name);
                    Console.Out.WriteLine("Doc ID         : " + document.id);
                    Console.Out.WriteLine("Training       : " + document.training.ToString() + "\n");

                    var entities = document.classification.entities;

                    foreach (Entity e in entities)
                    {
                        Console.Out.WriteLine("   Name        : " + e.name + "\n   Value       : " + e.value + "\n   Probability : " + e.probability + "\n   Multiple    : " + e.multiple + "\n");
                    }

                    // Delete any documents from the server that are not defined as true for training
                    // !!! Only uncomment and run this if you want to permanently delete these documents !!!
                    /*
                    if(document.training.ToString() == "false")
                    {
                        Console.Out.WriteLine("   About to delete " + document.id);
                        if(DeleteDocument.deleteDocument(url, token, document.id, 10000))
                        {
                            Console.Out.WriteLine("   Successfully deleted!\n");
                        }
                        else
                        {
                            Console.Out.WriteLine("   Failed to delete!\n");
                        }

                    }
                    */
                }

            }
            else
            {
                Console.Out.WriteLine("\n\nError Uploading. Exiting.");
                Console.In.ReadLine();
                return;
            }

            // keep the console up. Press any key to close.
            Console.In.ReadLine();
        }
    }
}
