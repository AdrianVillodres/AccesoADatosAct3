using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

using Newtonsoft.Json;
using System;

namespace cat.itb.NF3EA1_VillodresAdrian.Model
{
    [Serializable]
    public class Date
    {
        [JsonProperty("$date")]
        public long date { get; set; }
    }
}
