using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CURSOVA.Areas.Admin.Models
{
    public class UserModel
    {

        public string Id { get; set; }

        public string Login { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool Bannes { get; set; }
        public List<string> Roles { get; set; }

    }
}