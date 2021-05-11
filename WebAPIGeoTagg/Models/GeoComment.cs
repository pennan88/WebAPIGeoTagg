using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIGeoTagg.Models
{
    public class GeoComment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public double Logitude { get; set; }

        public double Latitude { get; set; }
    }
}
