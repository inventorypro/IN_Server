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
    public class IN_RequisitionController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_Requisition
        public IQueryable<IN_Requisition> GetIN_Requisition()
        {
            return db.IN_Requisition;
        }

        // GET: api/IN_Requisition/5
        [ResponseType(typeof(IN_Requisition))]
        public IHttpActionResult GetIN_Requisition(int id)
        {
            IN_Requisition iN_Requisition = db.IN_Requisition.Find(id);
            if (iN_Requisition == null)
            {
                return NotFound();
            }

            return Ok(iN_Requisition);
        }

        // PUT: api/IN_Requisition/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Requisition(int id, IN_Requisition iN_Requisition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Requisition.RequisID)
            {
                return BadRequest();
            }

            db.Entry(iN_Requisition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_RequisitionExists(id))
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

        // POST: api/IN_Requisition
        [ResponseType(typeof(IN_Requisition))]
        public IHttpActionResult PostIN_Requisition(IN_Requisition iN_Requisition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Requisition.Add(iN_Requisition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Requisition.RequisID }, iN_Requisition);
        }

        // DELETE: api/IN_Requisition/5
        [ResponseType(typeof(IN_Requisition))]
        public IHttpActionResult DeleteIN_Requisition(int id)
        {
            IN_Requisition iN_Requisition = db.IN_Requisition.Find(id);
            if (iN_Requisition == null)
            {
                return NotFound();
            }

            db.IN_Requisition.Remove(iN_Requisition);
            db.SaveChanges();

            return Ok(iN_Requisition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_RequisitionExists(int id)
        {
            return db.IN_Requisition.Count(e => e.RequisID == id) > 0;
        }
    }
}