using RestSharp;
using System;
using System.Collections.Generic;
using LemmanaClient.Models;
using Newtonsoft.Json;

namespace LemmanaClient.API
{
    class GetDocuments
    {
        public static GetDocumentsResponse getDocuments(URL url, string token, int timeOut)
        {


            Console.Out.WriteLine("\nStarting getting Document list");

            var client = new RestClient(url.serverURLNoPort + "/documents");
            client.Timeout = timeOut;

            // use the lemmana certificate
            client = Certificate.setCertificate(client);

            // uncomment to see the token is passed
            //Console.Out.WriteLine("Token : " + token);

            var request = new RestRequest(Method.GET);
            request.AddHeader("lemmana-token", Guid.NewGuid().ToString());
            request.AddHeader("accept-language", "en-US,en;q=0.8");
            request.AddHeader("accept-encoding", "gzip, deflate, br");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", "Lemmana/v1 (Sample App)");
            request.AddHeader("x-auth-token", token);
            request.AddHeader("host", url.serverAddress + ":" + url.port);

            IRestResponse response = client.Execute(request);

            // uncomment to see the JSON response
            //Console.Out.WriteLine("\nResponse Content : " + response.Content);

            GetDocumentsResponse documentsResponse = new GetDocumentsResponse();
            documentsResponse.statusCode = response.StatusCode.ToString();
            documentsResponse.responseStatus = response.ResponseStatus.ToString();

            if (response.StatusCode.ToString() == "OK")
            {
                documentsResponse.document = JsonConvert.DeserializeObject<List<Document>>(response.Content);
            }

            return documentsResponse;
        }
    }
}
