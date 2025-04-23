using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UF3_test.model

{
    [Serializable]
    public class Person
    {
        //[JsonProperty("isActive")]
        public bool IsActive { get; set; }

        //[JsonProperty("balance")]
        public string Balance { get; set; }

        //[JsonProperty("picture")]
        public string Picture { get; set; }

        //[JsonProperty("age")]
        public int Age { get; set; }

        //[JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("company")]
        public string Company { get; set; }

       //[JsonProperty("phone")]
        public string Phone { get; set; }

        //[JsonProperty("email")]
        public string Email { get; set; }

        //[JsonProperty("address")]
        public string Address { get; set; }

        //[JsonProperty("about")]
        public string About { get; set; }

        //[JsonProperty("registered")]
        public string Registered { get; set; }

        //[JsonProperty("latitude")]
        public double Latitude { get; set; }

        //[JsonProperty("tags")]
        public string[] Tags { get; set; }

        //[JsonProperty("friends")]
        public List<Friend> Friends { get; set; }

        //[JsonProperty("gender")]
        public string Gender { get; set; }

        //[JsonProperty("randomArrayItem")]
        public string RandomArrayItem { get; set; }
    }
}
