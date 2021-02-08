using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using aPBDs17245.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aPBDs17245.Controllers
{
    [Route("api/artist")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly s17245Context _context;

        public ArtistController(s17245Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetArtist()
        {
            return Ok(_context.Artist.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetArtist(int id)
        {

            //           select a.nickname,  e.name
            //              from dbo.artist a
            //              join dbo.artist_event ae on a.idartist = ae.idartist
            //              join dbo.event e on e.idevent=ae.idevent
            try
            {
                var db = new s17245Context();

                var res = from Nickname in db.Artist
                          where Nickname.IdArtist == id
                          join IdArtist in db.ArtistEvent on Nickname.IdArtist equals IdArtist.IdArtist
                          join Name in db.Event on IdArtist.IdEvent equals Name.IdEvent
                          orderby Name.StartDate descending
                          select new { Nickname, Name };


                //ontext.Artist.Join(_context.ArtistEvent, e =>e._context.Artist =?)
                //Where(e => e.IdArtist == id).Single();
                return Ok(res);

            }
            catch (Exception ex)
            {
                throw new Exception("brak takiego artysty " + ex);
            }

        }


        [HttpPost("{idArtist,idEvent,idEvent}")]
        public IActionResult UpdateTime(int idArtist, int idEvent, DateTime date)
        {

            try
            {

                var d = new ArtistEvent
                {
                    IdArtist = idArtist,
                    IdEvent = idEvent,
                    PerformanceDate = date
                };

            _context.Attach(d);
            _context.Entry(d).Property("PerformanceDate").IsModified = true;
            _context.SaveChanges();

            return Ok(d);
            }
            catch (Exception ex)
            {
                throw new Exception("brak takiego artysty, albo eventu " + ex);
            }

        }
    }
}
