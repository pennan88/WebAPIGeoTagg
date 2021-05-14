using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIGeoTagg.Data;
using WebAPIGeoTagg.Models;

namespace GeoTaggV1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GeoCommentController : ControllerBase
    {
        private readonly GeoCommentDbContext _context;

        public GeoCommentController(GeoCommentDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        [SwaggerOperation(
            Summary = "V1 Överblick på GeoTagg Meddelanden",
            Description = "Här kan du se alla GeoTagg Meddelanden"
            )]
        public async Task<ActionResult<IEnumerable<DTOList.GetDTOV1>>> GetCommentV1()
        {
            List<GeoCommentVersion2> V2_list = await _context.GeoComment2.Include(a => a.Message).ToListAsync();
            List<DTOList.GetDTOV1> v1list = new List<DTOList.GetDTOV1>();

            foreach (var item in V2_list)
            {
                DTOList.GetDTOV1 geoMessage = new DTOList.GetDTOV1 { Message = item.Message.Body, Latitude = item.Latitude, Longitude = item.Longitude };
                v1list.Add(geoMessage);
            }

            return v1list;
        }


        [HttpGet("[action]/{id}")]
        [SwaggerOperation(
            Summary = "V1 Överblick på specifika GeoTagg Meddelanden",
            Description = "Här kan du se specifika GeoTagg Meddelanden med hjälp av ett ID"
            )]
        public async Task<ActionResult<DTOList.GetDTOV1>> GetCommentV1(int id)
        {
            var geoMessages = await _context.GeoComment2.Include(a => a.Message).FirstOrDefaultAsync(b => b.Id == id);

            if (geoMessages == null)
            {
                return NotFound();
            }
            var v1model = new DTOList.GetDTOV1
            {
                Message = geoMessages.Message.Body,
                Latitude = geoMessages.Latitude,
                Longitude = geoMessages.Longitude
            };
            return v1model;
        }


        [HttpPost("[action]")]
        [Authorize]
        [SwaggerOperation(
            Summary = "V1 En post för nya GeoComments",
            Description = "Här kan du lägga till nya GeoComments"
            )]
        public async Task<ActionResult<GeoComment>> PostCommentV1(DTOList.GetDTOV1 geoComment)
        {
            var V1_model = new GeoCommentVersion2
            {
                Message = new Message
                {
                    Body = geoComment.Message
                },
                Latitude = geoComment.Latitude,
                Longitude = geoComment.Longitude
            };
            _context.GeoComment2.Add(V1_model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentV1", new { id = V1_model.Id }, V1_model);

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
        private readonly GeoCommentDbContext _context;

        public GeoMessageController(GeoCommentDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        [SwaggerOperation(
            Summary = "V2 Överblick på GeoTagg Meddelanden, nu med Min/Max begränsningar",
            Description = "Här kan du se alla GeoTagg Meddelanden och söker efter min/max värden"
            )]
        public async Task<ActionResult<IEnumerable<DTOList.GetDTOV2>>> GetCommentV2(double minLon, double minLat, double maxLon, double maxLat)
        {
            var GetInfo = await _context.GeoComment2.Include(a => a.Message)
                .Where(o => (o.Longitude <= maxLon && o.Longitude >= minLon) && (o.Latitude <= maxLat && o.Latitude >= minLat))
                .ToListAsync();

            List<DTOList.GetDTOV2> DTOV2List = new List<DTOList.GetDTOV2>();
            foreach (var item in GetInfo)
            {
                DTOList.GetDTOV2 Geo_DTO = new DTOList.GetDTOV2()
                {
                    Message = new DTOList.MessageDTOV2()
                    {
                        Author = item.Message.Author,
                        Body = item.Message.Body,
                        Title = item.Message.Title
                    },
                    Latitude = item.Latitude,
                    Longitude = item.Longitude
                };
                DTOV2List.Add(Geo_DTO);
            }
            return DTOV2List;
        }


        [HttpGet("[action]/{id}")]
        [SwaggerOperation(
            Summary = "V2 Överblick på specifika GeoTagg Meddelanden",
            Description = "Här kan du se specifika GeoTagg Meddelanden med hjälp av ett ID"
            )]
        public async Task<ActionResult<DTOList.GetDTOV2>> GetCommentV2(int id)
        {
            var GetInfo = await _context.GeoComment2.Include(a => a.Message).FirstOrDefaultAsync(o => o.Id == id);

            DTOList.GetDTOV2 Geo_DTO1 = new DTOList.GetDTOV2()
            {

                Message = new DTOList.MessageDTOV2()
                {
                    Author = GetInfo.Message.Author,
                    Body = GetInfo.Message.Body,
                    Title = GetInfo.Message.Title,
                },
                Latitude = GetInfo.Latitude,
                Longitude = GetInfo.Longitude

            };
            return Geo_DTO1;
        }


        [HttpPost("[action]")]
        [Authorize]
        [SwaggerOperation(
            Summary = "V1 En post för nya GeoComments",
            Description = "Här kan du lägga till nya GeoComments"
            )]


        public async Task<ActionResult<GeoCommentVersion2>> PostCommentV2(DTOList.PostDTOV2 geoComment)
        {
            GeoCommentVersion2 geoCommentVersion2 = new GeoCommentVersion2()
            {
                Message = new Message()
                {
                    Body = geoComment.Message.Body,
                    Title = geoComment.Message.Title,
                    Author = geoComment.Message.Author,
                },
                Latitude = geoComment.Latitude,
                Longitude = geoComment.Longitude
            };

            _context.GeoComment2.Add(geoCommentVersion2);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCommentV2", new { id = geoCommentVersion2.Id }, geoCommentVersion2);
        }

    };

}


