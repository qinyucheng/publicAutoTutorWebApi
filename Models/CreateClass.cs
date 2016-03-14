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
            string templateLoginHtml = HttpContext.Current.Server.MapPath("~/templateLogin.html");
            string newLoginHtml = HttpContext.Current.Server.MapPath("~/login" + ClassName.Replace(" ", "")+".html"); //new URL
            System.IO.File.Copy(templateLoginHtml, newLoginHtml, true);
            writeNameListToLoginPage(newLoginHtml);
            generateNameList();
            return true;
        }

        public bool generateNameList()
        {
            //string path = HttpContext.Current.Server.MapPath("~/js/xyz.js");
            string path = HttpContext.Current.Server.MapPath("~/js/login"+ClassName+".js");
            string studentNameListString="";
            string studentUIDAndName = "";
            int length= StudentsName.Count;
            StreamWriter sr = File.CreateText(path);
            for (int i = 0; i < length; i++)
            {
                var studentInfor = StudentsName[i].Split(':');
                if (i == length - 1)
                {
                    studentNameListString += "\"" + studentInfor[studentInfor.Length-1] + "\"";
                    studentUIDAndName += "\"" + StudentsName[i] + "\"";
                }
                else
                {
                    studentNameListString += "\"" + studentInfor[studentInfor.Length - 1] + "\", ";
                    studentUIDAndName += "\"" + StudentsName[i] + "\", ";
                }
            }
            string data = "var studentNames=[" + studentNameListString + "];" + "\r\n" + "var studentUID=[" + studentUIDAndName + "];" + "\r\n" + "var ClassName=\"" + group + "\";";
            sr.Write(data);
            sr.Flush();
            sr.Close();
            return true;
        }

        public void writeNameListToLoginPage(string newLoginHtml)
        {
            string line="<script type='text/javascript' src='js/login"+ClassName+".js'></script>";
            string text = File.ReadAllText(newLoginHtml);
            text = text.Replace("<script></script>", line);
            File.WriteAllText(newLoginHtml, text);
        
        }
    }

}