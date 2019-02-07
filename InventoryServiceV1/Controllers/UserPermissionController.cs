using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using InventoryDataAccess;

namespace InventoryService.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class UserPermissionController : ApiController
    {
        [BasicAuthentication]
        public HttpResponseMessage Get()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            using (InventoryDBEntities entities = new InventoryDBEntities())
            {



                string getUsername = username.ToLower();
                var entity = entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername).ToList();

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername).ToList());



                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ERROR!!!!");
                }

                // return Request.CreateResponse(HttpStatusCode.OK,
                //               entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername && e.Permission.ToLower() == "admin").ToList());


                /*    switch (username.ToLower())
                   {
                      case "all":
                           return Request.CreateResponse(HttpStatusCode.OK, 
                               entities.EmployeeInfoes.ToList());


                       case "z1":
                           return Request.CreateResponse(HttpStatusCode.OK, 
                               entities.EmployeeInfoes.Where(e => e.Position.ToLower() == "z1").ToList());
                       case "z2":
                           return Request.CreateResponse(HttpStatusCode.OK, 
                               entities.EmployeeInfoes.Where(e => e.Position.ToLower() == "z2").ToList());
                       default:
                           return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                           "Value for position must be All, z1 or z2." + position + "is invalid.");
                   } 
                   */
            }
        }

        /*  public UserPermission Get(string id)
          {
              using (InventoryDBEntities entities = new InventoryDBEntities())
              {
                  return entities.UserPermissions.FirstOrDefault(e => e.ESSUSR_Name == id);
              }
          }
          */

        [BasicAuthentication]
        public HttpResponseMessage Post([FromBody] UserPermission userPermission)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            try
            {
                using (InventoryDBEntities entities = new InventoryDBEntities())
                {

                    string getUsername = username.ToLower();
                    var checkPermiss = entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername && e.Permission.ToLower() == "admin").ToList();
                    if (checkPermiss.Count() != 0)
                    {



                        entities.UserPermissions.Add(userPermission);
                        entities.SaveChanges();

                        var message = Request.CreateResponse(HttpStatusCode.Created, userPermission);
                        message.Headers.Location = new Uri(Request.RequestUri + userPermission.ESSUSR_Name.ToString());
                        return message;



                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ERROR!!!!");
                    }

                }



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }





        [BasicAuthentication]
        public HttpResponseMessage Delete(string id)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            try
            {
                using (InventoryDBEntities entities = new InventoryDBEntities())
                {
                    string getUsername = username.ToLower();
                    var checkPermiss = entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername && e.Permission.ToLower() == "admin").ToList();

                    var entity = entities.UserPermissions.FirstOrDefault(e => e.ESSUSR_Name == id);
                    if (entity == null && checkPermiss.Count() != 0)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Error with Id = " + id.ToString() + "Not Found To Delete ! ");
                    }
                    else
                    {
                        entities.UserPermissions.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [BasicAuthentication]
        public HttpResponseMessage Put(string id, [FromBody]UserPermission userPermission)
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            string getUsername = username.ToLower();


            try
            {
                using (InventoryDBEntities entities = new InventoryDBEntities())
                {
                    var checkPermiss = entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername && e.Permission.ToLower() == "user" || e.Permission.ToLower() == "manager").ToList();
                    var checkPermissAdmin = entities.UserPermissions.Where(e => e.ESSUSR_Name.ToLower() == getUsername && e.Permission.ToLower() == "admin").ToList();
                    var entity = entities.UserPermissions.FirstOrDefault(e => e.ESSUSR_Name == id);
                    if (entity != null && id == getUsername && checkPermiss.Count() != 0)
                    {
                        entity.EMP_EngName = userPermission.EMP_EngName;
                        entity.EMP_Email = userPermission.EMP_Email;
                        entity.Position = userPermission.Position;
                        entity.Location = userPermission.Location;
                        entity.SITES = userPermission.SITES;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);

                    }
                    else if (entity != null && checkPermissAdmin.Count() != 0)
                    {

                        entity.EMP_EngName = userPermission.EMP_EngName;
                        entity.EMP_Email = userPermission.EMP_Email;
                        entity.Position = userPermission.Position;
                        entity.Location = userPermission.Location;
                        entity.SITES = userPermission.SITES;
                        entity.Status = userPermission.Status;
                        entity.Permission = userPermission.Permission;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Error with Id = " + id.ToString() + "not found to update !!");
                    }








                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }



        }



    }

}
