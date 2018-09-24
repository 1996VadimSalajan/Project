using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using WebApplication3.Models.Validation;

namespace CodeFirst
{
    public class User : IdentityUser
    {
        public string FirstName{get;set;}
        public string LastName { get; set;}
        public DateTime DateCreated { get; set; }
        public byte[] Photo { get; set; }
        [ValidBirthday]
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public enum Gender { }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}