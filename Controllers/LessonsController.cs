﻿using System;
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
    public class LessonsController : ApiController
    {
        Models.OprationMongo opm = new OprationMongo();
         string strconn = ConfigurationManager.AppSettings["connectionString"];
        //const string strconn = "mongodb://localhost:27017/PublicAutoTutor";
        //private static Dictionary<string, Lessons> list = new Dictionary<string, Lessons>();

        [HttpGet]
        [ActionName("SelectAll")]
        
        public List<Lessons> Get()
        {
            opm.ConnDatabase(strconn);
            var list=opm.getAllLessonsInfo();
            return list;
        }

        [HttpGet]
        [ActionName("SelectByLessonID")]

        public Lessons Get(string id)
        {
            opm.ConnDatabase(strconn);
            var result = opm.getLessonsInfoById(id);
            return result;
            
          
        }

        [HttpGet]
        [ActionName("SelectByLessonID")]

        public List<Lessons> Get(string id, string status)
        {
            opm.ConnDatabase(strconn);
            var list = opm.getLessonsInfoByStatus(status);
            return list;
           

        }


        


        [HttpPost]
        [ActionName("Add")]
        public bool Post(Models.Lessons lessonInfo)
        {
            lessonInfo.Status = "active";
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

        [HttpDelete]
        [ActionName("Remove")]
        public string Delete(string id, Models.Lessons lessonInfo)
        {
            opm.ConnDatabase(strconn);
            if (id == "deleteLessonById")
            {
                var result = opm.deleteLessonById(lessonInfo.LessonID);
                return "this lesson deleted successfully!";
            }
            return "this lesson deleted successfully!";
           
        }
    }
}
