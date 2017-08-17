using RestSharp;
using System;
using System.IO;
using LemmanaClient.Models;
using Newtonsoft.Json;

namespace LemmanaClient.API
{
    class UploadDocument
    {


        public static UploadDocumentResponse uploadFile(URL url, string token, int timeOut, bool training, bool classification, bool entities, string filePath)
        {


            Console.Out.WriteLine("\nStarting upload of file : " + filePath);
            Console.Out.WriteLine("\nLocal file exists : " + File.Exists(filePath));

            var client = new RestClient(url.serverURLNoPort + "/documents?training=" + training.ToString() + "&classification=" + classification.ToString() + "&entities=" + entities.ToString());
            client.Timeout = timeOut;

            // use the lemmana certificate
            client = Certificate.setCertificate(client);

            // uncomment to see the token is passed
            //Console.Out.WriteLine("Token : " + token);

            var request = new RestRequest(Method.POST);
            request.AddHeader("lemmana-token", Guid.NewGuid().ToString());
            request.AddHeader("accept-language", "en-US,en;q=0.8");
            request.AddHeader("accept-encoding", "gzip, deflate, br");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", "Lemmana/v1 (Sample App)");
            request.AddHeader("x-auth-token", token);
            request.AddHeader("host", url.serverAddress + ":" + url.port);
            request.AddFile("file", filePath, "application/pdf");

            Console.Out.WriteLine("\nFiles for upload : " + request.Files.Count);

            IRestResponse response = client.Execute(request);

            // uncomment to see the JSON response
            //Console.Out.WriteLine("\nResponse Content : " + response.Content);

            UploadDocumentResponse uploadResponse = new UploadDocumentResponse();
            uploadResponse.statusCode = response.StatusCode.ToString();
            uploadResponse.responseStatus = response.ResponseStatus.ToString();

            if (response.StatusCode.ToString() == "Created")
            {
                uploadResponse.document = JsonConvert.DeserializeObject<Document>(response.Content);
            }
 
            return uploadResponse;

        }
			//return item;

    }
}
