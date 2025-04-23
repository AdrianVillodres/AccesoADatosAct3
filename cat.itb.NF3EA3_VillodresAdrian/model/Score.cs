using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.NF3EA1_VillodresAdrian.Model
{
    [Serializable]
    public class Score
    {
        public string type { get; set; }
        public NumberDouble score { get; set; }
    }
}
