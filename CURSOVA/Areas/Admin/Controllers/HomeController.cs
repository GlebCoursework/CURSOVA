using CURSOVA.Areas.Admin.Models;
using CURSOVA.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CURSOVA.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult PizzasAdmin()
        {
            return View();
        }

        public ActionResult EditPizzas()
        {
            List<PizzaModelAdmin> pizzas = applicationDbContext.Pizzas.Select
                              (x => new PizzaModelAdmin
                              {
                                  Id = x.Id,
                                  Name = x.Name,
                                  Price = x.Price,
                                  Missing = x.Missing,
                                  Size = x.Size,
                                  PizzasComponents = x.Components.Select(c => c.Name).ToList()
                              }).ToList();
            return PartialView("_EditPizzas", pizzas);
        }
        [HttpGet]
        public ActionResult EditPizza(int? id)
        {
            Pizza pizza = applicationDbContext.Pizzas.FirstOrDefault(x => x.Id == id);
            PizzaModelAdmin pizzamodel = new PizzaModelAdmin();
            if (pizza != null)
            {
                pizzamodel = new PizzaModelAdmin
                {
                    Id = pizza.Id,
                    Name = pizza.Name,
                    PizzasComponents = pizza.Components.Select(c => c.Name).ToList(),
                    Missing = pizza.Missing,
                    Price = pizza.Price,
                    Size = pizza.Size
                };
            }
            else
            {
                return null;
            }
            return PartialView("_EditPizza", pizzamodel);
        }

    }
}