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
    public class IN_UnitTypeController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        
        // GET: api/IN_UnitType


        public IQueryable<IN_UnitType> GetIN_UnitType()
       {
            return db.IN_UnitType;
       }

        // GET: api/IN_UnitType/5
        [ResponseType(typeof(IN_UnitType))]
        public IHttpActionResult GetIN_UnitType(int id)
        {
            IN_UnitType iN_UnitType = db.IN_UnitType.Find(id);
            if (iN_UnitType == null)
            {
                return NotFound();
            }

            return Ok(iN_UnitType);
        }

        // PUT: api/IN_UnitType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_UnitType(int id, IN_UnitType iN_UnitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_UnitType.UnitTypeID)
            {
                return BadRequest();
            }

            db.Entry(iN_UnitType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_UnitTypeExists(id))
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

        // POST: api/IN_UnitType
        [ResponseType(typeof(IN_UnitType))]
        public IHttpActionResult PostIN_UnitType(IN_UnitType iN_UnitType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_UnitType.Add(iN_UnitType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_UnitType.UnitTypeID }, iN_UnitType);
        }

        // DELETE: api/IN_UnitType/5
        [ResponseType(typeof(IN_UnitType))]
        public IHttpActionResult DeleteIN_UnitType(int id)
        {
            IN_UnitType iN_UnitType = db.IN_UnitType.Find(id);
            if (iN_UnitType == null)
            {
                return NotFound();
            }

            db.IN_UnitType.Remove(iN_UnitType);
            db.SaveChanges();

            return Ok(iN_UnitType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_UnitTypeExists(int id)
        {
            return db.IN_UnitType.Count(e => e.UnitTypeID == id) > 0;
        }
    }
}