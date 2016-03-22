using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using publicAutoTutorWebApi.Models;
using System.Configuration;
namespace publicAutoTutorWebApi.Controllers
{
    public class ClassesController : ApiController
    {
        Models.OprationMongo opm = new OprationMongo();
        string strconn = ConfigurationManager.AppSettings["connectionString"];
        //const string strconn = "mongodb://localhost:27017/PublicAutoTutor";
      

        [HttpGet]
        [ActionName("SelectAll")]

        public List<Classes> Get()
        {
            opm.ConnDatabase(strconn);
            var list = opm.getAllClassInfo();
            return list;
        }

        [HttpGet]
        [ActionName("SelectClassesByTeacherEmail")]

        public List<Classes> Get(string TeacherEmail)
        {
            opm.ConnDatabase(strconn);
            var result = opm.getAllClassInfoByTeacherEmail(TeacherEmail);
            return result;
        }

        [HttpGet]
        [ActionName("SelectClassesByKey")]

        public List<Classes> Get(string key, string searchKey)
        {
            List<Classes> result;
            opm.ConnDatabase(strconn);
            if (key == "getClassInfoByClassName")
            {
                var ClassName = searchKey;
                result = opm.getClassInfoByClassName(ClassName);

            }

            else if (key == "advanceSearch") {
                result = opm.advanceSearchClassInfo(searchKey);
            }
            else
            {

                List<Classes> ls = new List<Classes>();

                result = ls;
            }
            return result;
            
        }

        [HttpPost]
        [ActionName("Add")]
        public bool Post(Classes ClassesObj)
        {
            //ClassesObj._id = ClassesObj.ClassName;
            ClassesObj.LastChangeTime = DateTime.Now;
            ClassesObj.CreatedTime = DateTime.Now;
            ClassesObj.ClassStatus = "active";
            opm.ConnDatabase(strconn);
            var result = opm.addClass(ClassesObj);
            return result;
           
        }

        [HttpPut]
        [ActionName("Modify")]
        public bool Put(string id, Classes ClassesObj)
        {
            opm.ConnDatabase(strconn);
            if (id == "ModifyClassStudents")
            {
                var result = opm.updateClassStudentsInfo(ClassesObj);
                return result;
            }
            else if (id == "ModifySelectedLessonsInfo")
            {
                var result = opm.updateClassSelectedLessonInfo(ClassesObj);
                return result;
            }
            else if (id == "ModifyClassBasicInfo")
            {
                ClassesObj.LastChangeTime = DateTime.Now;
                var result = opm.UpdateClassBasicInfo(ClassesObj);
                return result;
            }
            else if (id == "ModifyClassStadyURL")
            {
                ClassesObj.LastChangeTime = DateTime.Now;
                var result = opm.UpdateClassStadyURL(ClassesObj);
                return result;
            }
            else if (id == "ModifyUpdateClassInfo")
            {
                ClassesObj.LastChangeTime = DateTime.Now;
                var result = opm.updateClassInfo(ClassesObj);
                return result;
            }
            else if (id == "ModifyClassStatus")
            {
                ClassesObj.LastChangeTime = DateTime.Now;
                var result = opm.updateClassStatus(ClassesObj);
                return result;
            }
            else
            {
                return false;
            }

        }
    }
}
