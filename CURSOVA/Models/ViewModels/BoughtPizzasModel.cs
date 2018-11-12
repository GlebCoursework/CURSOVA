using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CURSOVA.Models.ViewModels
{
    public class BoughtPizzasModel
    {
        public PizzaModel PizzaModel { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}