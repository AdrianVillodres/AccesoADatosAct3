using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.NF3EA1_VillodresAdrian.Model
{
    public class Student
    {
        public Oid? _id { get; set; }
        public string? firstname { get; set; }
        public string? lastname1 { get; set; }
        public string? lastname2 { get; set; }
        public string? dni { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? phone_aux { get; set; }
        public int birth_year { get; set; }
    }
}
