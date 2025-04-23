using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace cat.itb.NF3EA1_VillodresAdrian.Model
{
    public class Grade
    {
        public Oid? _id { get; set; }
        public NumberInt? student_id { get; set; }
        public List<Score> scores { get; set; }
        public NumberInt? class_id { get; set; }
    }
}
