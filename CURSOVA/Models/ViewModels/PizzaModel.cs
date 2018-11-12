using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CURSOVA.Models.ViewModels
{
    public class PizzaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public List<string> PizzasComponents { get; set; }
    }
}