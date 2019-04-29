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
    public class IN_LocationHistoryController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        [BasicAuthentication]
        // GET: api/IN_LocationHistory
        public HttpResponseMessage GetIN_LocationHistory(string siteName = "All")
        {
            string getSiteName = siteName.ToLower();
            var checkUserID = db.IN_LocationHistory.Where(e => e.SITE.ToLower() == getSiteName).ToList();
            if (!CheckPermission())
            {
                if (siteName.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_LocationHistory.ToList());
                }
                else if (checkUserID.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_LocationHistory.Where(e => e.SITE.ToLower() == getSiteName).ToList());
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
                      db.IN_LocationHistory.Where(e => e.SITE.ToLower() == getSiteName).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }

        }

        // GET: api/IN_LocationHistory/5
        [ResponseType(typeof(IN_LocationHistory))]
        public IHttpActionResult GetIN_LocationHistory(int id)
        {
            IN_LocationHistory iN_LocationHistory = db.IN_LocationHistory.Find(id);
            if (iN_LocationHistory == null)
            {
                return NotFound();
            }

            return Ok(iN_LocationHistory);
        }

        // PUT: api/IN_LocationHistory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_LocationHistory(int id, IN_LocationHistory iN_LocationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_LocationHistory.Id)
            {
                return BadRequest();
            }

            db.Entry(iN_LocationHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_LocationHistoryExists(id))
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

        // POST: api/IN_LocationHistory
        [ResponseType(typeof(IN_LocationHistory))]
        public IHttpActionResult PostIN_LocationHistory(IN_LocationHistory iN_LocationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_LocationHistory.Add(iN_LocationHistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (IN_LocationHistoryExists(iN_LocationHistory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = iN_LocationHistory.Id }, iN_LocationHistory);
        }

        // DELETE: api/IN_LocationHistory/5
        [ResponseType(typeof(IN_LocationHistory))]
        public IHttpActionResult DeleteIN_LocationHistory(int id)
        {
            IN_LocationHistory iN_LocationHistory = db.IN_LocationHistory.Find(id);
            if (iN_LocationHistory == null)
            {
                return NotFound();
            }

            db.IN_LocationHistory.Remove(iN_LocationHistory);
            db.SaveChanges();

            return Ok(iN_LocationHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_LocationHistoryExists(int id)
        {
            return db.IN_LocationHistory.Count(e => e.Id == id) > 0;
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