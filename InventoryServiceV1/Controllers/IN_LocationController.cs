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
    public class IN_LocationController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        [BasicAuthentication]
        public HttpResponseMessage GetIN_Location(string siteName = "All")
        {
            string getSiteName = siteName.ToLower();
            var checkUserID = db.IN_Location.Where(e => e.SITES.ToLower() == getSiteName).ToList();
            if (!CheckPermission())
            {
                if (siteName.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_Location.ToList());
                }
                else if (checkUserID.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_Location.Where(e => e.SITES.ToLower() == getSiteName).ToList());
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
                      db.IN_Location.Where(e => e.SITES.ToLower() == getSiteName).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                   "Value for your sites is invalid.");
        }

        // GET: api/IN_Location
    /*    public IQueryable<IN_Location> GetIN_Location()
        {
            return db.IN_Location;
        }
*/
        // GET: api/IN_Location/5
        [ResponseType(typeof(IN_Location))]
        public IHttpActionResult GetIN_Location(int id)
        {
            IN_Location iN_Location = db.IN_Location.Find(id);
            if (iN_Location == null)
            {
                return NotFound();
            }

            return Ok(iN_Location);
        }

        // PUT: api/IN_Location/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Location(int id, IN_Location iN_Location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Location.LocationID)
            {
                return BadRequest();
            }

            db.Entry(iN_Location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_LocationExists(id))
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

        // POST: api/IN_Location
        [ResponseType(typeof(IN_Location))]
        public IHttpActionResult PostIN_Location(IN_Location iN_Location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Location.Add(iN_Location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Location.LocationID }, iN_Location);
        }

        // DELETE: api/IN_Location/5
        [ResponseType(typeof(IN_Location))]
        public IHttpActionResult DeleteIN_Location(int id)
        {
            IN_Location iN_Location = db.IN_Location.Find(id);
            if (iN_Location == null)
            {
                return NotFound();
            }

            db.IN_Location.Remove(iN_Location);
            db.SaveChanges();

            return Ok(iN_Location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_LocationExists(int id)
        {
            return db.IN_Location.Count(e => e.LocationID == id) > 0;
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