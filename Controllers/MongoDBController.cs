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
namespace publicAutoTutorWebApi.Controllers
{
    public class MongoDBController : ApiController
    {
          
             Models.OprationMongo opm = new OprationMongo();
             const string strconn = "mongodb://localhost:27017/PublicAutoTutor";

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
        public string Get(string Email, string Password)
        {
            opm.ConnDatabase(strconn);
            var result = opm.validateUserEmailandPwd(Email, Password);
            if (result)
            {
                return "validation successful";
            }
            else
            {
                return "validation fail";
            }
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
        public string Put(string id, Models.Users userinfo)
        {
            opm.ConnDatabase(strconn);
            if (id == "ModifyUserInfo")
            {
                var result = opm.updateUserInfo(userinfo);
            }
            else if (id == "ModifyUserPassword")
            {
                var result = opm.updateUserPassword(userinfo);
            }
            else if (id == "ModifyUserStatus")
            {
                var result = opm.updateUserStatus(userinfo);
            }
            else if (id == "ModifyUserRole")
            {
                var result = opm.updateUserRole(userinfo);
            }
            else if (id == "ModifyUserRoleAndStatus")
            {
                var result = opm.ModifyUserRoleAndStatus(userinfo);
            }
           
            return "Customer updated successfully!";
        }

        [HttpDelete]
        [ActionName("Remove")]
        public string Delete(string id)
        {
           
            return "Customer deleted successfully!";
        }
       
       
    }
  
}
