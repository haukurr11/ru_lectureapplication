﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyLectureApplication.Models;  

using System.Web.Security;
namespace MyLectureApplication.Controllers
{

    public class CurrentUserController : ApiController
    {
        [Authorize]
        public UserProfile Get()
        {
            AppDataContext db = new AppDataContext();
            var u = (from user in db.UserProfiles
                     where user.UserName == User.Identity.Name
                     select user).SingleOrDefault();
            return u;
        }
    }
    public class LecturesController : ApiController
    {
        AppDataContext db = new AppDataContext();

        // GET api/lectures
        [Authorize]
        public IEnumerable<Lecture> Get()
        {
            var result = from lecture in db.Lectures
                         select lecture;
            if (result == null)
                throw new FieldAccessException("Nothing found in db.Lectures");
                          
            return result;
        }

        // GET api/lectures/5
        [Authorize]
        public Lecture Get(int id)
        {
            var result = (from lecture in db.Lectures 
                           where lecture.ID == id 
                           select lecture).SingleOrDefault();
            if (result == null)
            {
                //throw new HttpResponseException("Not found");
            }
            return result;
        }

        // POST api/lectures
        //[Authorize(Roles = "Teachers")]
        public void Post(Lecture lec)
        {
            AppDataContext db = new AppDataContext();

            var result = (from users in db.UserProfiles
                          where users.UserName == User.Identity.Name
                          select users).SingleOrDefault();
            lec.Teacher = result;
            //var lecture = new Lecture { LectureURL = lec.LectureURL, DatePublished = lec.DatePublished, Teacher = lec.Teacher, Name = lec.Name };
            lec.DatePublished = DateTime.Now;
            db.Lectures.Add(lec);
            db.SaveChanges();
                         
        }

        // PUT api/lectures/5
        [Authorize(Roles = "Teachers")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/lectures/5
        [Authorize(Roles = "Teachers")]
        public void Delete(int id)
        {
        }
    }
}
