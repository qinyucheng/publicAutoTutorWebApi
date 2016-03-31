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
        string apiFolderName = ConfigurationManager.AppSettings["webApiFolder"];
        string loginPageFold = ConfigurationManager.AppSettings["loginPageFolder"];

        public string generateLoginPage(string url) {
            string templateLoginHtml = HttpContext.Current.Server.MapPath("~/"+loginPageFold+"/templateLogin.html");
            string folderStoreLoginPage = HttpContext.Current.Server.MapPath("~/" + loginPageFold);
            string newLoginHtml = folderStoreLoginPage+"/"+ClassName.Replace(" ", "")+".html"; //new URL
            string newloginHtmlURL = url + "/" + apiFolderName + "/" + loginPageFold + "/" + ClassName.Replace(" ", "") + ".html";
            try
            {
                if (!Directory.Exists(folderStoreLoginPage))
                {
                    Directory.CreateDirectory(folderStoreLoginPage);
                }

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
            string jsFolder = HttpContext.Current.Server.MapPath("~/" + loginPageFold + "/" + "js");
            string path = jsFolder+"/"+ClassName+".js";
            string studentNameListString="";
            string studentUIDAndName = "";

            if (!Directory.Exists(jsFolder))
            {
                Directory.CreateDirectory(jsFolder);
            }

            StreamWriter sr = File.CreateText(path);

            // there are no students in studentList
            if (StudentsName == null) {
                string data = "var studentNames=[" + "" + "];" + "\r\n" + "var studentUID=[" + "" + "];" + "\r\n" + "var ClassName=\"" + ClassName + "\";";
                sr.Write(data);
                sr.Flush();
                sr.Close();
                return false;
            }
            int length= StudentsName.Count;
            try { 
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