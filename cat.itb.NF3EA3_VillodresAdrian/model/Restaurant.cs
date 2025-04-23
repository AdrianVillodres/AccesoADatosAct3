using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.NF3EA1_VillodresAdrian.Model
{
    public class Restaurant
    {
        public Address address { get; set; }
        public string borough { get; set; }
        public string cuisine { get; set; }
        public List<Grade2> grades { get; set; }
        public string name { get; set; }
        public string restaurant_id { get; set; }
    }
}
