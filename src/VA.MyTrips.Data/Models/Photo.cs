using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.MyTrips.Data.Models
{
    public class Photo
    {
        [JsonProperty(PropertyName = "tripid")]
        public string TripId { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string PhotoId { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }        
        [JsonProperty(PropertyName = "archived")]
        public bool Archived { get; set; }

    }
}
