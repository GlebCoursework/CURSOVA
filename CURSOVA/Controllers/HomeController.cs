﻿using CURSOVA.Models;
using CURSOVA.Models.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CURSOVA.Controllers
{
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
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult CheckUser()
        {

            return PartialView("");
        }

        public PizzaModel ConvertPizzaToPizzaModel(Pizza pizza)
        {
            PizzaModel pizzaModel = new PizzaModel()
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Price = pizza.Price,
                Size = pizza.Size
            };
            pizzaModel.PizzasComponents = new List<string>();
            foreach (var item in pizza.Components)
            {
                pizzaModel.PizzasComponents.Add(item.Name);
            }
            return pizzaModel;
        }

        [Authorize]
        public ActionResult Pizzas()
        {
            List<PizzaModel> PizzaModels = applicationDbContext.Pizzas.Select(x => new PizzaModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Size = x.Size,
                PizzasComponents = x.Components.Select(c => c.Name).ToList()
            }).ToList();

            return View("Pizzas", PizzaModels);
        }

        public List<BoughtPizzasModel> SetBuyPizza(List<int> pizzaId)
        {
            List<BoughtPizzasModel> BoughtPizzasModel = new List<BoughtPizzasModel>();
            foreach (var item in pizzaId)
            {
                if (BoughtPizzasModel.FirstOrDefault(x => x.PizzaModel.Id == item) == null)
                {
                    Pizza pizza = applicationDbContext.Pizzas.FirstOrDefault(p => p.Id == item);
                    BoughtPizzasModel.Add(new BoughtPizzasModel
                    {
                        Count = 1,
                        PizzaModel = ConvertPizzaToPizzaModel(pizza),
                        TotalPrice = pizza.Price
                    });
                }
                else
                {
                    BoughtPizzasModel boughtPizza = BoughtPizzasModel.First(x => x.PizzaModel.Id == item);
                    boughtPizza.Count++;
                    boughtPizza.TotalPrice += boughtPizza.PizzaModel.Price;
                }
            }
            Session["Pizzas"] = pizzaId;
            return BoughtPizzasModel;
        }

        [Authorize]
        public ActionResult Buy(int? id)
        {
            List<BoughtPizzasModel> BoughtPizzasModel = new List<BoughtPizzasModel>();
            int Id = id ?? 0;
            List<int> pizzaId = new List<int>();
            if (Session["Pizzas"] != null)
            {
                pizzaId = (Session["Pizzas"] as List<int>);
            }
            if (Id > 0)
            {
                pizzaId.Add(Id);
            }
            BoughtPizzasModel = SetBuyPizza(pizzaId);
            return PartialView("_BoughtPizzas", BoughtPizzasModel);
        }

        [Authorize]
        public ActionResult Del(int? id)
        {
            List<BoughtPizzasModel> BoughtPizzasModel = new List<BoughtPizzasModel>();
            int Id = id ?? 0;
            List<int> pizzaId = (Session["Pizzas"] as List<int>);
            pizzaId.Remove(pizzaId.First(x => x == Id));

            BoughtPizzasModel = SetBuyPizza(pizzaId);
            return PartialView("_BoughtPizzas", BoughtPizzasModel);

        }

        public async Task<ActionResult> Accept(Models.ViewModels.UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("UserInfo", userInfo);
            }

            ApplicationUser applicationUser = await UserManager.FindByNameAsync(User.Identity.Name);
            // applicationUser.BoughtLists.Add
            BoughtList bought = new BoughtList { Address = userInfo.Address, Phone = userInfo.Phone };
            List<int> pizzaId = (Session["Pizzas"] as List<int>);
            List<BoughtPizzasModel> BoughtPizzasModel = SetBuyPizza(pizzaId);
            bought.BoughtPizzas = new List<BoughtPizza>();

            foreach (var item in BoughtPizzasModel)
            {
                bought.BoughtPizzas.Add(new BoughtPizza
                {
                    Count = item.Count,
                    TotalPrice = item.TotalPrice,
                    Pizza = applicationDbContext.Pizzas.
                            FirstOrDefault(x => x.Id == item.PizzaModel.Id)
                });

            }
            applicationUser.BoughtLists.Add(bought);
            await applicationDbContext.SaveChangesAsync();

            return null;
        }

        public ActionResult UserInfo()
        {
            return View("UserInfo");
        }
    }
}
