using System.Collections.Generic;


namespace LemmanaClient.Models
{
    class GetDocumentsResponse
    {
        public string statusCode { get; set; }
        public string responseStatus { get; set; }
        public string responseMessage { get; set; }
        public List <Document> document { get; set; }
    }
}
