using CURSOVA.Areas.Admin.Models;
using CURSOVA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public ActionResult Users()
        {
            List<string> temp = new List<string>();
            List<UserModel> UsersModel= UserManager.Users.Select(x => new UserModel
                                                        {
                                                            Id=x.Id,
                                                            Bannes=x.Bannes,
                                                            Email=x.Email,
                                                            SurName=x.Surname,
                                                            Name=x.Name,
                                                            Login=x.UserName   
                                                         }).ToList();

            foreach (var item in UsersModel)
            {
                item.Roles = new List<string>();
                ApplicationUser au = applicationDbContext.Users.First(u => u.UserName == item.Login);

                foreach (IdentityUserRole role in au.Roles)
                {

                    string name = role.RoleId;
                    item.Roles.AddRange(applicationDbContext.Roles.Where(r => r.Id == role.RoleId).Select(r=>r.Name).ToList());
                }
            }
            return View(UsersModel);
        }


        public ActionResult EditPizzaComponents(int? id)
        {
            CurrentPizzaComponentsModel currentPizzaComponentsModel = new CurrentPizzaComponentsModel();
            currentPizzaComponentsModel.CurrentList =
            applicationDbContext.Pizzas.FirstOrDefault(x => x.Id == id).
            Components.Select(c => new PizzaComponentsModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            currentPizzaComponentsModel.TotalList = applicationDbContext.Components.Select(c => new PizzaComponentsModel {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return PartialView("_EditPizzaComponents", currentPizzaComponentsModel);
        }
    }
}