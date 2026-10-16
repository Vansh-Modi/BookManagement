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
    public class Tbl_BookingController : ApiController
    {
        private MovieBookingEntities1 db = new MovieBookingEntities1();

        // GET: api/Tbl_Booking
        public IQueryable<Tbl_Booking> GetTbl_Booking()
        {
            return db.Tbl_Booking;
        }

        // GET: api/Tbl_Booking/5
        [ResponseType(typeof(Tbl_Booking))]
        public IHttpActionResult GetTbl_Booking(int id)
        {
            Tbl_Booking tbl_Booking = db.Tbl_Booking.Find(id);
            if (tbl_Booking == null)
            {
                return NotFound();
            }

            return Ok(tbl_Booking);
        }

        // PUT: api/Tbl_Booking/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTbl_Booking(int id, Tbl_Booking tbl_Booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Booking.booking_ID)
            {
                return BadRequest();
            }

            db.Entry(tbl_Booking).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tbl_BookingExists(id))
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

        // POST: api/Tbl_Booking
        [ResponseType(typeof(Tbl_Booking))]
        public IHttpActionResult PostTbl_Booking(Tbl_Booking tbl_Booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tbl_Booking.Add(tbl_Booking);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Booking.booking_ID }, tbl_Booking);
        }

        // DELETE: api/Tbl_Booking/5
        [ResponseType(typeof(Tbl_Booking))]
        public IHttpActionResult DeleteTbl_Booking(int id)
        {
            Tbl_Booking tbl_Booking = db.Tbl_Booking.Find(id);
            if (tbl_Booking == null)
            {
                return NotFound();
            }

            db.Tbl_Booking.Remove(tbl_Booking);
            db.SaveChanges();

            return Ok(tbl_Booking);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tbl_BookingExists(int id)
        {
            return db.Tbl_Booking.Count(e => e.booking_ID == id) > 0;
        }
    }
}