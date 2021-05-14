using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIGeoTagg.Models
{
    public class DTOList
    {
        public class GetDTOV1
        {
            public string Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class GetDTOV2
        {
            public MessageDTOV2 Message { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class MessageDTOV2
        {
            public string Title { get; set; }
            public string Body { get; set; }
            public string Author { get; set; }
        }

        public class PostMessageV2
        {
            public string Body { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
        }

        public class PostDTOV2
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public PostMessageV2 Message { get; set; }
        }

    }
}
