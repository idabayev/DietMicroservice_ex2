using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ex1_ApiForMeals
{
    public class NameRoutingObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
