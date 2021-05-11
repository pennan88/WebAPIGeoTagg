using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIGeoTagg.Models
{
    public class GeoCommentVersion2 : GeoComment
    {
        public new Message Message { get; set; }
    }
}
