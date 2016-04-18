using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace publicAutoTutorWebApi.Models
{
    public class Classes
    {
        public string _id { get; set; }
        public string TeacherEmail { get; set; }
        public string ClassName { get; set; }
       
        public Nullable<System.DateTime> StudyStartTime { get; set; }
        public Nullable<System.DateTime> StudyEndTime { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> LastChangeTime { get; set; }
      
        public string StudyURL { get; set; }
        public string ClassStatus { get; set; }
        public string LessonGroup { get; set; }

        public List<SeletedLessons> SeletedLeassons { get; set; }
        public List<SeletedStudent> StudentGroup { get; set; }  
    }
    public class SeletedLessons 
    {
        public string LessonID { get; set; }
        public string LessonName { get; set; }
        public string LessonURL { get; set; }
        public string ScriptURL { get; set; }
        public string Description { get; set; }
        public string VideoURL { get; set; }  
    }
    public class SeletedStudent 
    {
        public string ID { get; set; }
        public string StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set; }
    }

        
}