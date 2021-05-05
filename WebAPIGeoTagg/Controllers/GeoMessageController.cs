using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGeoTagg.Models;

namespace GeoTaggV1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GeoMessageController : ControllerBase
    {

        [HttpGet("[action]")]
        [SwaggerOperation(
            Summary = "Överblick på GeoTagg Meddelanden",
            Description = "Här kan du se GeoTagg Meddelanden"
            )]
        public IEnumerable<GeoMessage> GetMessages()
        {
            return Enumerable.Range(1, 3).Select(index => new GeoMessage
            {
                Message = "",
                Latitude = 0,
                Logitude = 0

            });
        }

        /// <summary>
        ///     Överblick på GeoTagg Meddelanden
        /// </summary>
        /// 
        /// <Param name="tagg" example="">
        ///     <para>Här kan du se GeoTagg Meddelanden</para>
        /// </Param>
        /// 
        /// <returns></returns>
        [HttpGet("[action]")]
        public IEnumerable<GeoMessage> GetMessagesTest(string tagg)
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
namespace GeoTaggV2
{
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GeoMessageController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<GeoMessage> GetMessageId(int id)
        {
            return Enumerable.Range(1, 3).Select(index => new GeoMessage
            {
                Message = "",
                Latitude = 0,
                Logitude = 0

            });
        }

        [HttpGet("[action]")]
        public IEnumerable<GeoMessage> GetMessages()
        {
            return Enumerable.Range(1, 3).Select(index => new GeoMessage
            {
                Message = "",
                Latitude = 0,
                Logitude = 0

            });
        }

        [HttpPost]
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
