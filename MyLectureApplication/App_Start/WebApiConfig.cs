﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using MyLectureApplication.Controllers;

namespace MyLectureApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Change to JSON
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            

            config.Routes.MapHttpRoute(
               name: "Lecture",
               routeTemplate: "api/v1/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
               name: "Lectures",
               routeTemplate: "api/v1/{controller}",
               defaults: new { controller = "Lectures" }
           );
           
            config.Routes.MapHttpRoute(
               name: "Comments",
               routeTemplate: "api/v1/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional}
           );/*
            config.Routes.MapHttpRoute(
               name: "Comment",
               routeTemplate: "api/v1/{controller}",
               defaults: new { controller = "CommentsController" }
           );*/
        }
    }
}
