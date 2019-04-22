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
using InventoryDataAccess;

namespace InventoryServiceV1.Controllers
{
    public class IN_TopicController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_Topic
        public IQueryable<IN_Topic> GetIN_Topic()
        {
            return db.IN_Topic;
        }

        // GET: api/IN_Topic/5
        [ResponseType(typeof(IN_Topic))]
        public IHttpActionResult GetIN_Topic(int id)
        {
            IN_Topic iN_Topic = db.IN_Topic.Find(id);
            if (iN_Topic == null)
            {
                return NotFound();
            }

            return Ok(iN_Topic);
        }

        // PUT: api/IN_Topic/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Topic(int id, IN_Topic iN_Topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Topic.Id)
            {
                return BadRequest();
            }

            db.Entry(iN_Topic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_TopicExists(id))
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

        // POST: api/IN_Topic
        [ResponseType(typeof(IN_Topic))]
        public IHttpActionResult PostIN_Topic(IN_Topic iN_Topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Topic.Add(iN_Topic);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Topic.Id }, iN_Topic);
        }

        // DELETE: api/IN_Topic/5
        [ResponseType(typeof(IN_Topic))]
        public IHttpActionResult DeleteIN_Topic(int id)
        {
            IN_Topic iN_Topic = db.IN_Topic.Find(id);
            if (iN_Topic == null)
            {
                return NotFound();
            }

            db.IN_Topic.Remove(iN_Topic);
            db.SaveChanges();

            return Ok(iN_Topic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_TopicExists(int id)
        {
            return db.IN_Topic.Count(e => e.Id == id) > 0;
        }
    }
}