using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web_APIs.Models;

namespace Web_APIs.Controllers
{
    public class Tbl_MovieController : ApiController
    {
        private MovieBookingEntities1 db = new MovieBookingEntities1();

        // GET: api/Tbl_Movie
        public IQueryable<Tbl_Movie> GetTbl_Movie()
        {
            return db.Tbl_Movie;
        }

        // GET: api/Tbl_Movie/5
        [ResponseType(typeof(Tbl_Movie))]
        public IHttpActionResult GetTbl_Movie(int id)
        {
            Tbl_Movie tbl_Movie = db.Tbl_Movie.Find(id);
            if (tbl_Movie == null)
            {
                return NotFound();
            }

            return Ok(tbl_Movie);
        }

        // PUT: api/Tbl_Movie/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Movie(int id, Tbl_Movie tbl_Movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Movie.Movie_ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Movie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tbl_Movie
        [ResponseType(typeof(Tbl_Movie))]
        public IHttpActionResult PostTbl_Movie(Tbl_Movie tbl_Movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Movie.Add(tbl_Movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Movie.Movie_ID }, tbl_Movie);
        }

        // DELETE: api/Tbl_Movie/5
        [ResponseType(typeof(Tbl_Movie))]
        public IHttpActionResult DeleteTbl_Movie(int id)
        {
            Tbl_Movie tbl_Movie = db.Tbl_Movie.Find(id);
            if (tbl_Movie == null)
            {
                return NotFound();
            }

            db.Tbl_Movie.Remove(tbl_Movie);
            db.SaveChanges();

            return Ok(tbl_Movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_MovieExists(int id)
        {
            return db.Tbl_Movie.Count(e => e.Movie_ID == id) > 0;
        }
    }
}