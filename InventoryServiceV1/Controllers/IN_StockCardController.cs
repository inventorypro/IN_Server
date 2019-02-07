using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using InventoryDataAccess;
using InventoryService;

namespace InventoryServiceV1.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class IN_StockCardController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/IN_StockCard
        [BasicAuthentication]
        public IQueryable<IN_StockCard> GetIN_StockCard()
        {
            return db.IN_StockCard;
        }

        // GET: api/IN_StockCard/5
        [ResponseType(typeof(IN_StockCard))]
        [BasicAuthentication]
        public IHttpActionResult GetIN_StockCard(int id)
        {
            IN_StockCard iN_StockCard = db.IN_StockCard.Find(id);
            if (iN_StockCard == null)
            {
                return NotFound();
            }

            return Ok(iN_StockCard);
        }

        // PUT: api/IN_StockCard/5
        [ResponseType(typeof(void))]
        [BasicAuthentication]
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

        // POST: api/IN_StockCard
        [ResponseType(typeof(IN_StockCard))]
        [BasicAuthentication]
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

        // DELETE: api/IN_StockCard/5
        [ResponseType(typeof(IN_StockCard))]
        [BasicAuthentication]
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
    }
}