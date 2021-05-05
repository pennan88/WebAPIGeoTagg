using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGeoTagg.Models;

namespace WebAPIGeoTagg.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class GeoMessageController : ControllerBase
    {
        [HttpGet]
        [Route("/api/v1/geo-comments/{id}")]
        public IEnumerable<GeoMessage> GetMessageId()
        {
            return Enumerable.Range(1, 3).Select(index => new GeoMessage
            {
                Message = "",
                Latitude = 0,
                Logitude = 0

            });
        }

        [HttpGet]
        [Route("/api/v1/geo-comments")]
        public IEnumerable<GeoMessage> GetMessages()
        {
            return Enumerable.Range(1, 3).Select(index => new GeoMessage
            {
                Message = "",
                Latitude = 0,
                Logitude = 0

            });
        }

        [Authorize]
        [HttpPost]
        [Route("/api/v1/geo-comments")]
        public IEnumerable<GeoMessage> PostMessage()
        {
            return Enumerable.Range(1, 3).Select(index => new GeoMessage
            {
                Message = "",
                Latitude = 0,
                Logitude = 0

            });
        }
        

    }
}
