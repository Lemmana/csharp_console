using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemmanaClient.Models
{
    class Document
    {
        public string id { get; set; }
        public string fileName { get; set; }
        public string training { get; set; }
        public Classification classification { get; set; }
    }
}
