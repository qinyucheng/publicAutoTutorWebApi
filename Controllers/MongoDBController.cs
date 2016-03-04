using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using publicAutoTutorWebApi.Models;
using System.Configuration;

namespace publicAutoTutorWebApi.Controllers
{
    public class MongoDBController : ApiController
    {
          
             Models.OprationMongo opm = new OprationMongo();
             string strconn = ConfigurationManager.AppSettings["connectionString"];

        [HttpGet]
        [ActionName("SelectByEmail")]
       public bool Get(string Email)
        {

            opm.ConnDatabase(strconn);
           var result=opm.validateUserEmail(Email);
            if(result)
            {
                return true;
            }
            else

            {
                return false;
            }
        }

        [HttpGet]
        [ActionName("SelectByEmailandPwd")]
        public Users Get(string Email, string Password)
        {
            opm.ConnDatabase(strconn);
            var result = opm.validateUserEmailandPwd(Email, Password);
           return result;
        }
      

        [HttpPost]
        [ActionName("Add")]
        public bool Post(Models.Users userinfo)
        {
            userinfo.RegistrationTime =  DateTime.Now;
            userinfo.ApprovalTime = DateTime.Now;
            opm.ConnDatabase(strconn);
            var result = opm.addUserInfo(userinfo);
            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }


        [HttpPut]
        [ActionName("Modify")]
        public bool Put(string id, Models.Users userinfo)
        {
            var result=false;
            opm.ConnDatabase(strconn);
            if (id == "ModifyUserInfo")
            {
                 result = opm.updateUserInfo(userinfo);
            }
            else if (id == "ModifyUserPassword")
            {
                 result = opm.updateUserPassword(userinfo);
            }
            else if (id == "ModifyUserStatus")
            {
                 result = opm.updateUserStatus(userinfo);
            }
            else if (id == "ModifyUserRole")
            {
                 result = opm.updateUserRole(userinfo);
            }
            else if (id == "ModifyUserRoleAndStatus")
            {
                 result = opm.ModifyUserRoleAndStatus(userinfo);
            }

            return result;
        }

        [HttpDelete]
        [ActionName("Remove")]
        public string Delete(string id)
        {
           
            return "Customer deleted successfully!";
        }
       
       
    }
  
}
