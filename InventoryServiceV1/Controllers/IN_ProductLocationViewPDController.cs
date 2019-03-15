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
    public class IN_ProductLocationViewPDController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_ProductLocationViewPD
        public IQueryable<IN_ProductLocation> GetIN_ProductLocation()
        {
            return db.IN_ProductLocation;
        }

        public HttpResponseMessage GetIN_ProductLocation(int id)
        {

                return Request.CreateResponse(HttpStatusCode.OK,
             db.IN_ProductLocation.Where(e => e.LocationID == id).ToList());
     


        }

  
        // PUT: api/IN_ProductLocationViewPD/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_ProductLocation(int id, IN_ProductLocation iN_ProductLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_ProductLocation.ProductLocationID)
            {
                return BadRequest();
            }

            db.Entry(iN_ProductLocation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_ProductLocationExists(id))
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

        // POST: api/IN_ProductLocationViewPD
        [ResponseType(typeof(IN_ProductLocation))]
        public IHttpActionResult PostIN_ProductLocation(IN_ProductLocation iN_ProductLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_ProductLocation.Add(iN_ProductLocation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_ProductLocation.ProductLocationID }, iN_ProductLocation);
        }

        // DELETE: api/IN_ProductLocationViewPD/5
        [ResponseType(typeof(IN_ProductLocation))]
        public IHttpActionResult DeleteIN_ProductLocation(int id)
        {
            IN_ProductLocation iN_ProductLocation = db.IN_ProductLocation.Find(id);
            if (iN_ProductLocation == null)
            {
                return NotFound();
            }

            db.IN_ProductLocation.Remove(iN_ProductLocation);
            db.SaveChanges();

            return Ok(iN_ProductLocation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_ProductLocationExists(int id)
        {
            return db.IN_ProductLocation.Count(e => e.ProductLocationID == id) > 0;
        }
    }
}