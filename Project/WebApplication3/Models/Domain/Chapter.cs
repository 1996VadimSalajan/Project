using CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Domain
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumbersTheme { get; set; }
        public int? CoursId { get; set; }
        public virtual ICollection<Theme> Themes { get; set; }
        public virtual Cours Cours { get; set; }
    }
}