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
    public class IN_StockCardSITEController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        [BasicAuthentication]
        public HttpResponseMessage GetIN_StockCard(string siteName = "All")
        {
            string getSiteName = siteName.ToLower();
            var checkUserID = db.IN_StockCard.Where(e => e.SITES.ToLower() == getSiteName).ToList();
            if (!CheckPermission())
            {
                if (siteName.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_StockCard.ToList());
                }
                else if (checkUserID.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_StockCard.Where(e => e.SITES.ToLower() == getSiteName).ToList());
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
                      db.IN_StockCard.Where(e => e.SITES.ToLower() == getSiteName).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                   "Value for your sites is invalid.");
        }

        // GET: api/IN_StockCardSITE/5
        [ResponseType(typeof(IN_StockCard))]
        public IHttpActionResult GetIN_StockCard(int id)
        {
            IN_StockCard iN_StockCard = db.IN_StockCard.Find(id);
            if (iN_StockCard == null)
            {
                return NotFound();
            }

            return Ok(iN_StockCard);
        }

        // PUT: api/IN_StockCardSITE/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_StockCard(int id, IN_StockCard iN_StockCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_StockCard.StockCardID)
            {
                return BadRequest();
            }

            db.Entry(iN_StockCard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_StockCardExists(id))
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

        // POST: api/IN_StockCardSITE
        [ResponseType(typeof(IN_StockCard))]
        public IHttpActionResult PostIN_StockCard(IN_StockCard iN_StockCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_StockCard.Add(iN_StockCard);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_StockCard.StockCardID }, iN_StockCard);
        }

        // DELETE: api/IN_StockCardSITE/5
        [ResponseType(typeof(IN_StockCard))]
        public IHttpActionResult DeleteIN_StockCard(int id)
        {
            IN_StockCard iN_StockCard = db.IN_StockCard.Find(id);
            if (iN_StockCard == null)
            {
                return NotFound();
            }

            db.IN_StockCard.Remove(iN_StockCard);
            db.SaveChanges();

            return Ok(iN_StockCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_StockCardExists(int id)
        {
            return db.IN_StockCard.Count(e => e.StockCardID == id) > 0;
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