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
    public class IN_PackageController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_Package
        public IQueryable<IN_Package> GetIN_Package()
        {
            return db.IN_Package;
        }

        // GET: api/IN_Package/5
        [ResponseType(typeof(IN_Package))]
        public IHttpActionResult GetIN_Package(int id)
        {
            IN_Package iN_Package = db.IN_Package.Find(id);
            if (iN_Package == null)
            {
                return NotFound();
            }

            return Ok(iN_Package);
        }

        // PUT: api/IN_Package/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Package(int id, IN_Package iN_Package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Package.PackageID)
            {
                return BadRequest();
            }

            db.Entry(iN_Package).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_PackageExists(id))
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

        // POST: api/IN_Package
        [ResponseType(typeof(IN_Package))]
        public IHttpActionResult PostIN_Package(IN_Package iN_Package)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Package.Add(iN_Package);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Package.PackageID }, iN_Package);
        }

        // DELETE: api/IN_Package/5
        [ResponseType(typeof(IN_Package))]
        public IHttpActionResult DeleteIN_Package(int id)
        {
            IN_Package iN_Package = db.IN_Package.Find(id);
            if (iN_Package == null)
            {
                return NotFound();
            }

            db.IN_Package.Remove(iN_Package);
            db.SaveChanges();

            return Ok(iN_Package);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_PackageExists(int id)
        {
            return db.IN_Package.Count(e => e.PackageID == id) > 0;
        }
    }
}