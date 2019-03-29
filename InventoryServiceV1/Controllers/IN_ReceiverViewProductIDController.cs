using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using InventoryDataAccess;
using InventoryService;

namespace InventoryServiceV1.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class IN_ReceiverViewProductIDController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_ReceiverViewProductID
        [BasicAuthentication]
        public IQueryable<IN_Receiver> GetIN_Receiver()
        {
            return db.IN_Receiver;
        }

        // GET: api/IN_ReceiverViewProductID/5
        [BasicAuthentication]
        public HttpResponseMessage GetIN_ReceiverProductID(int id)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            string getUsername = username.ToLower();

            return Request.CreateResponse(HttpStatusCode.OK,
         db.IN_Receiver.Where(e => e.ProductID == id && e.ESSUSR_Name == username).ToList());



        }

        [BasicAuthentication]
        // PUT: api/IN_ReceiverViewProductID/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Receiver(int id, IN_Receiver iN_Receiver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Receiver.ReceiverID)
            {
                return BadRequest();
            }

            db.Entry(iN_Receiver).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_ReceiverExists(id))
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
        [BasicAuthentication]
        // POST: api/IN_ReceiverViewProductID
        [ResponseType(typeof(IN_Receiver))]
        public IHttpActionResult PostIN_Receiver(IN_Receiver iN_Receiver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Receiver.Add(iN_Receiver);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Receiver.ReceiverID }, iN_Receiver);
        }

        // DELETE: api/IN_ReceiverViewProductID/5
        [ResponseType(typeof(IN_Receiver))]
        public IHttpActionResult DeleteIN_Receiver(int id)
        {
            IN_Receiver iN_Receiver = db.IN_Receiver.Find(id);
            if (iN_Receiver == null)
            {
                return NotFound();
            }

            db.IN_Receiver.Remove(iN_Receiver);
            db.SaveChanges();

            return Ok(iN_Receiver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_ReceiverExists(int id)
        {
            return db.IN_Receiver.Count(e => e.ReceiverID == id) > 0;
        }
    }
}