using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSite.Models
{
    public class ProfileModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Message { get; set; }
        public string ImageFile { get; set; }
    }
}
