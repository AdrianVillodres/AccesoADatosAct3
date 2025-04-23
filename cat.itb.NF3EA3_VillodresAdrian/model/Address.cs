using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.NF3EA1_VillodresAdrian.Model
{
    [Serializable]
    public class Address
    {
        public string building { get; set; }
        public List<double> coord { get; set; }
        public string street { get; set; }
        public string zipcode { get; set; }
    }
}
