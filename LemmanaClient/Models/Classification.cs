using System.Collections.Generic;

namespace LemmanaClient.Models
{
    public class Classification
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Entity> entities { get; set; }
    }

    /*
    id":"5978966a70f78c04411ad144",
    "name":"AddressChange",
    "entiitiies"
    */
}
