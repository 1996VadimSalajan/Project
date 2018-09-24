using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models.Domain
{
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] InformationTheme { get; set; }
        public byte[] Exercises { get; set; }
        public int? ChapterId { get; set; }
        public virtual Chapter Chapter { get; set; }
    }
}