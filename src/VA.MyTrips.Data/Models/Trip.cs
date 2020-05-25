using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VA.MyTrips.Data.Models
{
    public class Trip
    {
        [JsonProperty(PropertyName = "id")]
        public string TripId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "destination")]
        public string Destination { get; set; }

        [JsonProperty(PropertyName = "geolocation")]
        public string GeoLocation { get; set; }

        [JsonProperty(PropertyName = "startdate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "enddate")]
        public DateTime EndDate { get; set; }
    }
}
