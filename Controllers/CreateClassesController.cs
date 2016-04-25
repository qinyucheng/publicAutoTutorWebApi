using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace publicAutoTutorWebApi.Controllers
{
    public class CreateClassesController : ApiController
    {
        public string Post(Models.CreateClass names)
        {
            var baseUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            var classURL = names.generateLoginPage(baseUrl);
            return classURL;
        }

        public string Put(LoginPageName pageName)
        {
            changeName(pageName.NewPageName, pageName.OldPageName);
            return "true";
        }

        public bool changeName(string newPageName, string oldPageName) {
             string loginPageFold = ConfigurationManager.AppSettings["loginPageFolder"];
             string LoginHtmlPath = HttpContext.Current.Server.MapPath("~/" + loginPageFold);
             newPageName = LoginHtmlPath + @"\" + newPageName+".html";
             oldPageName = LoginHtmlPath + @"\" + oldPageName + ".html";
             if (File.Exists(oldPageName))
             {
                 File.Move(oldPageName, newPageName);
             }
             else
             {
                 return false;
             }

            return true;
        }

        public class LoginPageName { 
            public string NewPageName {get; set;}
            public string OldPageName { get; set; }
        }
    }
}
