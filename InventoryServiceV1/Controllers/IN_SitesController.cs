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
    public class IN_SitesController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_Sites
        public IQueryable<IN_Sites> GetIN_Sites()
        {
            return db.IN_Sites;
        }

        // GET: api/IN_Sites/5
        [ResponseType(typeof(IN_Sites))]
        public IHttpActionResult GetIN_Sites(int id)
        {
            IN_Sites iN_Sites = db.IN_Sites.Find(id);
            if (iN_Sites == null)
            {
                return NotFound();
            }

            return Ok(iN_Sites);
        }

        // PUT: api/IN_Sites/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Sites(int id, IN_Sites iN_Sites)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Sites.SITESID)
            {
                return BadRequest();
            }

            db.Entry(iN_Sites).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_SitesExists(id))
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

        // POST: api/IN_Sites
        [ResponseType(typeof(IN_Sites))]
        public IHttpActionResult PostIN_Sites(IN_Sites iN_Sites)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Sites.Add(iN_Sites);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Sites.SITESID }, iN_Sites);
        }

        // DELETE: api/IN_Sites/5
        [ResponseType(typeof(IN_Sites))]
        public IHttpActionResult DeleteIN_Sites(int id)
        {
            IN_Sites iN_Sites = db.IN_Sites.Find(id);
            if (iN_Sites == null)
            {
                return NotFound();
            }

            db.IN_Sites.Remove(iN_Sites);
            db.SaveChanges();

            return Ok(iN_Sites);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_SitesExists(int id)
        {
            return db.IN_Sites.Count(e => e.SITESID == id) > 0;
        }
    }
}