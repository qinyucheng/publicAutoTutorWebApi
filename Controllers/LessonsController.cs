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
    public class LessonsController : ApiController
    {
        Models.OprationMongo opm = new OprationMongo();
        const string strconn = "mongodb://localhost:27017/PublicAutoTutor";
        private static Dictionary<string, Lessons> list = new Dictionary<string, Lessons>();

        [HttpGet]
        [ActionName("SelectAll")]
        public List<string> Get()
        {
            opm.ConnDatabase(strconn);
            var list=opm.getAllLessonsInfo();
            return list;
        }

        [HttpPost]
        [ActionName("Add")]
        public bool Post(Models.Lessons lessonInfo)
        {
           
            opm.ConnDatabase(strconn);
            var result = opm.addLessonInfo(lessonInfo);
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
        public string Put(string id, Models.Lessons lessonInfo)
        {
            opm.ConnDatabase(strconn);
            if (id == "ModifyLessonInfo")
            {
                var result = opm.updateLessonInfo(lessonInfo);
            }


            return "lessonInfo updated successfully!";
        }
    }
}
