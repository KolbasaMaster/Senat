﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SenatApi;
using RestSharp;
using NLog;

//namespace SenatWebAp
//{
//    [Authorize]
//    public class LoginToStandController : ApiController
//    {
//        [HttpGet]
//        public IHttpActionResult GetLogin()
//        {
//            RestSenatApiClient client = new RestSenatApiClient("https://dev.senat.sbt-osop-224.sigma.sbrf.ru");
//            return Ok("Connection succes");
//        }
//    }
//}