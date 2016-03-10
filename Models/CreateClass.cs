using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace publicAutoTutorWebApi.Models
{
    public class CreateClass
    {
        public List<string> StudentsName { get; set; }
        public string ClassName { get; set;}
        public string group{get;set;}
    }

}