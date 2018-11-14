using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CURSOVA.Areas.Admin.Models
{
    public class PizzaComponentsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CurrentPizzaComponentsModel
    {
        public List<PizzaComponentsModel> CurrentList { get; set; }
        public List<PizzaComponentsModel> TotalList { get; set; }
    }
}