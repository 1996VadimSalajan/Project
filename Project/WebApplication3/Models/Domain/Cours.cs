using System;
using System.Collections.Generic;
using WebApplication3.Models.Domain;

namespace CodeFirst
{
    public class Cours
    {
        public int Id { get; set; }
        /// <summary>
        /// de schimbat numele
        /// </summary>
        public string Name { get; set; }
        public int? GroupId { get; set; }
        public DateTime? DateCreateCourse { get; set; }
        public string TeacherId { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual Group Group { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}