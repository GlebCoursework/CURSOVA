using CURSOVA.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CURSOVA.Areas.Admin.Models
{
    public class PizzaModelAdmin : PizzaModel
    {
        public bool Missing { get; set; }
    }
}