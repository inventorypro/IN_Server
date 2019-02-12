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
    public class IN_RequisitionController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_Requisition
        //    public IQueryable<IN_Requisition> GetIN_Requisition()
        //  {
        //  return db.IN_Requisition;
        //}

        [BasicAuthentication]
        public HttpResponseMessage GetIN_Requisition(string userID = "All", string SITES = "All")
        {
            string getUserID = userID.ToLower();
            string getSITES = SITES.ToLower();
            var checkUserID = db.IN_Requisition.Where(e => e.UserID.ToLower() == getUserID).ToList();
            var checkAdmin = db.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == userID && e.Permission.ToLower() == "admin").ToList();
            if (!CheckPermission())
            {
                if (userID.ToLower() == "all" && SITES.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_Requisition.ToList());
                }
                else if (checkUserID.Count() == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_Requisition.Where(e => e.SITES.ToLower() == getSITES).ToList());
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
                    db.IN_Requisition.Where(e => e.UserID.ToLower() == getUserID).ToList());
                }
                else
                {
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