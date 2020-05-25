using System;
using System.Collections.Generic;

namespace VA.MyTrips.Business.Models
{
    public class TripModel
    {
        public string TripId { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public string GeoLocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PhotoModel> Photos { get; set; }
    }
}
