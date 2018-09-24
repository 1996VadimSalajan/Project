using AutoMapper;
using CodeFirst;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.DTO;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        private readonly IRepository<Cours> repcours;
        public CourseController(IRepository<Cours> cours)
        {
            repcours = cours;
        }

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult Pdf()
        {
            return View();
        }

        public JsonResult List()
        {
            var list = from c in repcours.Read()
                       select new { c.Id, c.Name, c.DateCreateCourse, c.TeacherId };

            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(Cours cours)
        {
            var userid = User.Identity.GetUserId();

            cours.TeacherId = userid;
            cours.DateCreateCourse = DateTime.Now;
            repcours.Insert(cours);
            repcours.Save();

            return Json(cours, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetbyID(int ID)
        {
            var cours = repcours.Read().Where(x => x.Id.Equals(ID)).FirstOrDefault();
            var coursDto = Mapper.Map<Cours, CoursDTO>(cours);
            return Json(coursDto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Cours cours)
        {
            var userid = User.Identity.GetUserId();

            var date = repcours.Read().Where(x => x.TeacherId==(userid)).FirstOrDefault();
            cours.DateCreateCourse = date.DateCreateCourse;
            repcours.Update(cours);
            repcours.Save();

            return Json(cours, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int ID)
        {
            Cours cours = new Cours()
            {
                Id = ID
            };

            repcours.Delete(cours);
            repcours.Save();

            return Json(cours, JsonRequestBehavior.AllowGet);
        }
    }
}