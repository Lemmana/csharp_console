using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmanaClient.API
{
    class DeleteDocument
    {

        public static bool deleteDocument(URL url, string token, string docId, int timeOut)
        {


            Console.Out.WriteLine("\nStarting delete of document : " + docId);

            var client = new RestClient(url.serverURLNoPort + "/documents/" + docId);
            client.Timeout = timeOut;

            // use the lemmana certificate
            client = Certificate.setCertificate(client);

            // uncomment to see the token is passed
            //Console.Out.WriteLine("Token : " + token);

            var request = new RestRequest(Method.DELETE);
            request.AddHeader("lemmana-token", Guid.NewGuid().ToString());
            request.AddHeader("accept-language", "en-US,en;q=0.8");
            request.AddHeader("accept-encoding", "gzip, deflate, br");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("user-agent", "Lemmana/v1 (Sample App)");
            request.AddHeader("x-auth-token", token);
            request.AddHeader("host", url.serverAddress + ":" + url.port);

            IRestResponse response = client.Execute(request);

            // uncomment to see the response NOTE: JSON is not returned
            //Console.Out.WriteLine("\nResponse Content : " + response.Content + "    StatusCode : " + response.StatusCode);

            if (response.StatusCode.ToString() == "OK")
                {
                return true;
                }
            else
                {
                return false;
                }

            //return documentsResponse;
        }
    }
}
