using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace publicAutoTutorWebApi.Models
{
    public class Lessons
    {
        public string _id { get; set; }
        public string LessonID { get; set; }
        public string LessonGroup { get; set; }
        public string LessonName { get; set; }
        public string Author { get; set; }
        public string LessonURL { get; set; }
        public string ScriptURL { get; set; }
        public string LessonForder { get; set; }
        public string ServerUrl { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
      
    }
}