using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using System.Configuration;

namespace publicAutoTutorWebApi.Models
{
    public class CreateClass
    {
        public List<string> StudentsName { get; set; }
        public string ClassName { get; set;}
        public string group{get;set;}      // using in student login to distingush classGroup
        Models.OprationMongo opm = new OprationMongo();
        string strconn = ConfigurationManager.AppSettings["connectionString"];


        public string generateLoginPage(string url) {
            string templateLoginHtml = HttpContext.Current.Server.MapPath("~/templateLogin.html");
            string newLoginHtml = HttpContext.Current.Server.MapPath("~/" + ClassName.Replace(" ", "")+".html"); //new URL
            string newloginHtmlURL = url + "/"+ClassName.Replace(" ", "")+".html";
            try
            {
                System.IO.File.Copy(templateLoginHtml, newLoginHtml, true);
                writeNameListToLoginPage(newLoginHtml);
                generateNameList();
                opm.ConnDatabase(strconn);
                opm.UpdateClassStudyURLFromServer(newloginHtmlURL, ClassName);
                return newloginHtmlURL;
            }

            catch (Exception e)
            {
                return "false";
            }
        }

        public bool generateNameList()
        {
            //string path = HttpContext.Current.Server.MapPath("~/js/xyz.js");
            string path = HttpContext.Current.Server.MapPath("~/js/"+ClassName+".js");
            string studentNameListString="";
            string studentUIDAndName = "";
            int length= StudentsName.Count;
            try { 
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
            string data = "var studentNames=[" + studentNameListString + "];" + "\r\n" + "var studentUID=[" + studentUIDAndName + "];" + "\r\n" + "var ClassName=\"" + ClassName + "\";";
            sr.Write(data);
            sr.Flush();
            sr.Close();
               
            return true; 
            }
            catch (Exception e)
            {
                return false;       
            }
        }

        public void writeNameListToLoginPage(string newLoginHtml)
        {
            try
            {
                string line = "<script type='text/javascript' src='js/" + ClassName + ".js'></script>";
                string text = File.ReadAllText(newLoginHtml);
                text = text.Replace("<script></script>", line);
                File.WriteAllText(newLoginHtml, text);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.ToString()),
                    ReasonPhrase = "error"
                };
                throw new HttpResponseException(resp);

            }
        }
    }

}