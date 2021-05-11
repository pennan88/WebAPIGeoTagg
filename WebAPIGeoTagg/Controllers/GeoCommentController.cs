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
            Summary = "Överblick på GeoTagg Meddelanden",
            Description = "Här kan du se alla GeoTagg Meddelanden"
            )]

        public async Task<ActionResult<IEnumerable<GeoComment>>> GetCommentV1()
        {
            List<GeoCommentVersion2> v2list = await _context.GeoComment2.Include(a => a.Message).ToListAsync();
            List<GeoComment> v1list = new List<GeoComment>();

            foreach (var item in v2list)
            {
                GeoComment geoMessage = new GeoComment { Message = item.Message.Body, Latitude = item.Latitude, Logitude = item.Logitude };
                v1list.Add(geoMessage);
            }

            return v1list;
        }


        [HttpGet("[action]/{id}")]
        [SwaggerOperation(
            Summary = "Överblick på specifika GeoTagg Meddelanden",
            Description = "Här kan du se specifika GeoTagg Meddelanden med hjälp av ett ID"
            )]
        public async Task<ActionResult<GeoComment>> GetCommentV1(int id)
        {
            var geoMessages = await _context.GeoComment2.Include(a => a.Message).FirstOrDefaultAsync(b => b.Id == id);

            if (geoMessages == null)
            {
                return NotFound();
            }
            var v2model = new GeoComment
            {
                Message = geoMessages.Message.Body,
                Latitude = geoMessages.Latitude,
                Logitude = geoMessages.Logitude
            };
            return v2model;
        }


        [HttpPost("[action]")]
        [Authorize]
        [SwaggerOperation(
            Summary = "En post för nya GeoComments",
            Description = "Här kan du lägga till nya GeoComments"
            )]
        public async Task<ActionResult<GeoComment>> PostCommentV1(GeoComment geoComment)
        {
            var v2model = new GeoCommentVersion2
            {
                Message = new Message
                {
                    Body = geoComment.Message
                },
                Latitude = geoComment.Latitude,
                Logitude = geoComment.Logitude
            };
            _context.GeoComment2.Add(v2model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentV1", new { id = geoComment.Id }, geoComment);

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

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<GeoCommentVersion2>> GetCommentV2(int id)
        {
            return await _context.GeoComment2.Include(a => a.Message).FirstOrDefaultAsync(o => o.Id == id);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<GeoCommentVersion2>>> GetCommentV2(double minLon, double minLat, double maxLon, double maxLat)
        {
            return await _context.GeoComment2.Include(a => a.Message)
                .Where(o => (o.Logitude <= maxLon && o.Logitude >= minLon) && (o.Latitude <= maxLat && o.Latitude >= minLat))
                .ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<GeoCommentVersion2>> PostCommentV2(GeoCommentVersion2 geoComment)
        {
            _context.GeoComment2.Add(geoComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentV2", new { id = geoComment.Id }, geoComment);
        }

    };

}


