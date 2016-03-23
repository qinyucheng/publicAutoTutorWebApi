using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
