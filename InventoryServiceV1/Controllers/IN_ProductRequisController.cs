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
    public class IN_ProductRequisController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_ProductRequis
        // [BasicAuthentication]
        // public IQueryable<IN_ProductRequis> GetIN_ProductRequis()
        // {
        //    return db.IN_ProductRequis;
        //  }
    /*    [BasicAuthentication]
        public HttpResponseMessage GetIN_Product(string userID = "All")
        {
            string getUserID = userID.ToLower();
            var checkUserID = db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUserID).ToList();
            if (!CheckPermission())
            {
                if (userID.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_ProductRequis.ToList());
                }
                else if (checkUserID.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUserID).ToList());
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                          "Value for your sites is invalid.");
                }
            }
            else if (checkUserID.Count() != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                      db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUserID).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                   "Value for your sites is invalid.");
        }
        */
        [BasicAuthentication]
        public HttpResponseMessage GetIN_Product(string userID = "All", string RequisStatus = "All")
        {
            string getUserID = userID.ToLower();
            string getRequisStatus = RequisStatus.ToLower();
            var checkUserID = db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUserID && e.RequisStatus.ToLower() == getRequisStatus).ToList();
            var checkAdmin = db.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == userID && e.Permission.ToLower() == "admin").ToList();
            if (!CheckPermission())
            {
                if (userID.ToLower() == "all" && RequisStatus.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_ProductRequis.ToList());
                }
                else if (checkUserID.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUserID && e.RequisStatus.ToLower() == getRequisStatus).ToList());
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                          "Value for your sites is invalid.");
                }
            }
            else if (checkUserID.Count() != 0)
            {
                if (checkAdmin.Count() == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUserID && e.RequisStatus.ToLower() == getRequisStatus).ToList());
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
              "Value for your sites is invalid.");
                }
              
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                   "Value for your sites is invalid.");
        }

        // GET: api/IN_ProductRequis/5
        [ResponseType(typeof(IN_ProductRequis))]
        [BasicAuthentication]
        public IHttpActionResult GetIN_ProductRequis(int id)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            string getUsername = username.ToLower();
            var checkAdmin = db.IN_ProductRequis.Where(e => e.UserID.ToLower() == getUsername && e.RequisID == id).ToList();
 
            if (!CheckPermission())
            {
                IN_ProductRequis iN_ProductRequis = db.IN_ProductRequis.Find(id);
                if (iN_ProductRequis == null)
                {
                    return NotFound();
                }

                return Ok(iN_ProductRequis);
            }
            else {

                if (checkAdmin.Count() != 0)
                {
              
                    IN_ProductRequis iN_ProductRequis = db.IN_ProductRequis.Find(id);
                    if (iN_ProductRequis == null)
                    {
                        return NotFound();
                    }

                    return Ok(iN_ProductRequis);
                }
                else
                {
                    return NotFound();

                }
            }



        }

        // PUT: api/IN_ProductRequis/5
        [ResponseType(typeof(void))]
        [BasicAuthentication]
        public IHttpActionResult PutIN_ProductRequis(int id, IN_ProductRequis iN_ProductRequis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_ProductRequis.RequisID)
            {
                return BadRequest();
            }

            db.Entry(iN_ProductRequis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_ProductRequisExists(id))
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

        // POST: api/IN_ProductRequis
        [ResponseType(typeof(IN_ProductRequis))]
        [BasicAuthentication]
        public IHttpActionResult PostIN_ProductRequis(IN_ProductRequis iN_ProductRequis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_ProductRequis.Add(iN_ProductRequis);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_ProductRequis.RequisID }, iN_ProductRequis);
        }

        // DELETE: api/IN_ProductRequis/5
        [ResponseType(typeof(IN_ProductRequis))]

        [BasicAuthentication]
        public IHttpActionResult DeleteIN_ProductRequis(int id)
        {
            IN_ProductRequis iN_ProductRequis = db.IN_ProductRequis.Find(id);
            if (iN_ProductRequis == null)
            {
                return NotFound();
            }

            db.IN_ProductRequis.Remove(iN_ProductRequis);
            db.SaveChanges();

            return Ok(iN_ProductRequis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_ProductRequisExists(int id)
        {
            return db.IN_ProductRequis.Count(e => e.RequisID == id) > 0;
        }
        private bool CheckPermission()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            string getUsername = username.ToLower();
            var checkPermiss = db.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername && e.Permission.ToLower() == "admin").ToList();
            if (checkPermiss.Count() != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}