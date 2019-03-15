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
    public class V_RequisitionPACKController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();

        // GET: api/V_RequisitionPACK
        public IQueryable<V_RequisitionPACK> GetV_RequisitionPACK()
        {
            return db.V_RequisitionPACK;
        }

        // GET: api/V_RequisitionPACK/5
        [ResponseType(typeof(V_RequisitionPACK))]
        public HttpResponseMessage GetV_RequisitionPACK(string RequisNumber)
        {
            return Request.CreateResponse(HttpStatusCode.OK,
                         db.V_RequisitionPACK.Where(e => e.RequisNumber == RequisNumber).ToList());


        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool V_RequisitionPACKExists(int id)
        {
            return db.V_RequisitionPACK.Count(e => e.RequisID == id) > 0;
        }
    }
}