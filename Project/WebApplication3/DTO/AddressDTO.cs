using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string Village { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UserId { get; set; }
    }
}