using System;
using System.Linq;
using System.Web.Mvc;
using CodeFirst;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using WebApplication3.Controllers;
using WebApplication3.Models;

namespace UniTest
{
    [TestClass]
    public class CourseControllerTest
    {
        private static IRepository<Cours> cours;

        public static CourseController Controller { get; set; } = new CourseController(cours);

        [SetUp]
        public void IndexFixtureSetUp()
        {
          
         
        }
        [TestMethod]
        public void CourseViewModelNotNull()
        {
            Cours course = new Cours() { NameCourse = "Matematica", DateCreateCourse = DateTime.Now};
            Controller.Add(course);
            var coursea = cours.Read().ToList();
            NUnit.Framework.CollectionAssert.Contains(coursea, course);
        }
    }
}
