﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace publicAutoTutorWebApi.Controllers
{
    public class CreateClassesController : ApiController
    {
        public bool Post(Models.CreateClass names)
        {

            Console.Write(names.StudentsName);
            Console.Write(names.group);

            names.generateLoginPage();


            return true;
        }
    }
}