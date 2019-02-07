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
    public class IN_CategoryController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_Category
        public IQueryable<IN_Category> GetIN_Category()
        {
            return db.IN_Category;
        }

        // GET: api/IN_Category/5
        [ResponseType(typeof(IN_Category))]
        public IHttpActionResult GetIN_Category(int id)
        {
            IN_Category iN_Category = db.IN_Category.Find(id);
            if (iN_Category == null)
            {
                return NotFound();
            }

            return Ok(iN_Category);
        }

        // PUT: api/IN_Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIN_Category(int id, IN_Category iN_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != iN_Category.CategoryID)
            {
                return BadRequest();
            }

            db.Entry(iN_Category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IN_CategoryExists(id))
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

        // POST: api/IN_Category
        [ResponseType(typeof(IN_Category))]
        public IHttpActionResult PostIN_Category(IN_Category iN_Category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IN_Category.Add(iN_Category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iN_Category.CategoryID }, iN_Category);
        }

        // DELETE: api/IN_Category/5
        [ResponseType(typeof(IN_Category))]
        public IHttpActionResult DeleteIN_Category(int id)
        {
            IN_Category iN_Category = db.IN_Category.Find(id);
            if (iN_Category == null)
            {
                return NotFound();
            }

            db.IN_Category.Remove(iN_Category);
            db.SaveChanges();

            return Ok(iN_Category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IN_CategoryExists(int id)
        {
            return db.IN_Category.Count(e => e.CategoryID == id) > 0;
        }
    }
}