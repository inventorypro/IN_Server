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
    public class IN_ProductController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        //get: string parameters
        [BasicAuthentication]
        public HttpResponseMessage GetIN_Product(string sites = "All") {
            string getSites = sites.ToLower();
            var checkSites = db.IN_Product.Where(e => e.SITES.ToLower() == getSites).ToList();
            if (!CheckPermission()) {
                if (sites.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_Product.ToList());
                }
                else if (checkSites.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_Product.Where(e => e.SITES.ToLower() == getSites).ToList());
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                          "Value for your sites is invalid.");
                }
            }
            else if (checkSites.Count() != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                      db.IN_Product.Where(e => e.SITES.ToLower() == getSites).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                   "Value for your sites is invalid.");
        }



        // GET: api/IN_Product
    /*    [BasicAuthentication]
       public IQueryable<IN_Product> GetIN_Product()
        {
           var result = db.IN_Product;
       
            if (!CheckPermission())
            {
                return result;
            }
            else {
                return null;
          }
           

          
        }*/

        // GET: api/IN_Product/5
        [ResponseType(typeof(IN_Product))]
        [BasicAuthentication]
        public IHttpActionResult GetIN_Product(int id)
        {

            IN_Product iN_Product = db.IN_Product.Find(id);
            if (iN_Product == null)
            {
                return NotFound();
            }

            if (!CheckPermission())
            {
                return Ok(iN_Product);
            }
            else
            {
                return NotFound();
            }
            
        }

        // PUT: api/IN_Product/5
        [ResponseType(typeof(void))]
        [BasicAuthentication]
        public IHttpActionResult PutIN_Product(int id, IN_Product iN_Product)
        {
         
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Product.ProductID)
            {
                return BadRequest();
            }


            if (!CheckPermission())
            {


                db.Entry(iN_Product).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return Ok(iN_Product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IN_ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            else {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IN_Product
        [ResponseType(typeof(IN_Product))]
        [BasicAuthentication]
        public IHttpActionResult PostIN_Product(IN_Product iN_Product)
        {
            if (!CheckPermission())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.IN_Product.Add(iN_Product);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = iN_Product.ProductID }, iN_Product);
            }
            else {
                return NotFound();
            }
   

          
        }

        // DELETE: api/IN_Product/5
        [ResponseType(typeof(IN_Product))]
        [BasicAuthentication]
        public IHttpActionResult DeleteIN_Product(int id)
        {
            if (!CheckPermission())
            {
                IN_Product iN_Product = db.IN_Product.Find(id);
                if (iN_Product == null)
                {
                    return NotFound();
                }

                db.IN_Product.Remove(iN_Product);
                db.SaveChanges();

                return Ok(iN_Product);
            }
            else {
                return NotFound();
            }

              
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_ProductExists(int id)
        {
            return db.IN_Product.Count(e => e.ProductID == id) > 0;
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
            else {
                return true;
            }
           
        }
    }
}