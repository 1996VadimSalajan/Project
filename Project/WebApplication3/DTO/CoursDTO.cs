using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models.Domain;

namespace WebApplication3.DTO
{
    public class CoursDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreateCourse { get; set; }
        public string TeacherId { get; set; }
       
    }
}