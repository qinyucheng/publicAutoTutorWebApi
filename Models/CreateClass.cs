using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace publicAutoTutorWebApi.Models
{
    public class CreateClass
    {
        public List<string> StudentsName { get; set; }
        public string ClassName { get; set;}
        public string group{get;set;}      // using in student login to distingush classGroup

        public bool generateLoginPage() {
            generateNameList();
            return true;
        }

        public bool generateNameList()
        {
            string path = HttpContext.Current.Server.MapPath("../js/xyz.js");
            string studentNameListString="";
            int length= StudentsName.Count;
            StreamWriter sr = File.CreateText(path);
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    studentNameListString += "\"" +StudentsName[i]+ "\"";
                }
                else
                {
                    studentNameListString += "\"" + StudentsName[i] + "\", ";
                }
            }
            string data = "var studentNames=[" + studentNameListString + "];" + "\r\n" + "var ClassName=" + group + ";";
            sr.Write(data);
            sr.Flush();
            sr.Close();
            return true;
        }
    }

}