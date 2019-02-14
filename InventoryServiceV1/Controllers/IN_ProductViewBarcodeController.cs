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
    public class IN_ProductViewBarcodeController : ApiController
    {
        private InventoryDBEntities db = new InventoryDBEntities();
        [BasicAuthentication]
        public HttpResponseMessage GetIN_ProductBarcode(string RequisNumber = "All")
        {
            string getProductRequis = RequisNumber.ToLower();
            var checkSites = db.IN_ProductRequis.Where(e => e.RequisNumber.ToLower() == getProductRequis).ToList();
            if (!CheckPermission())
            {
                if (RequisNumber.ToLower() == "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                           db.IN_ProductRequis.ToList());
                }
                else if (checkSites.Count() != 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                          db.IN_ProductRequis.Where(e => e.RequisNumber.ToLower() == getProductRequis).ToList());
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
                      db.IN_ProductRequis.Where(e => e.RequisNumber.ToLower() == getProductRequis).ToList());
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                      "Value for your sites is invalid.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                   "Value for your sites is invalid.");
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
