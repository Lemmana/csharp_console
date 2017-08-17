using RestSharp;
using System;


namespace LemmanaClient.API
{
    class TestService
    {

        public static bool testServiceUp(URL url, int timeOut)
        {
            // Hit the api docs to check if the service is up and we have internat access
            // if it is not we return false. For a general connectivity test you could use http://www.google.com

            Console.Out.WriteLine("\nTesting " + url.serverURLNoPort + "/v2/api-docs  is running");

            // Create the client 
            RestClient client = new RestClient(url.serverURLNoPort + "/v2/api-docs");
           
            // time out needs to be long enough for a test
            client.Timeout = timeOut;

            // use the lemmana https certificate
            client = Certificate.setCertificate(client);

            var request = new RestRequest(Method.GET);
            request.AddHeader("lemmana-token", Guid.NewGuid().ToString());
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString().ToUpper() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
