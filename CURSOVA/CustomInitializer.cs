using System.Data.Entity;
using CURSOVA.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace CURSOVA
{
    internal class CustomInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var roleadmin = new IdentityRole { Name = "admin" };
            var roleuser = new IdentityRole { Name = "user" };

            roleManager.Create(roleadmin);
            roleManager.Create(roleuser);

            var admin = new ApplicationUser { Email = "glebich195@gmail.com", UserName = "glebich195", BoughtLists = new List<BoughtList>(), Name = "Gleb", Surname = "test" };
            string password = "Qwerty111";
            var result = userManager.Create(admin, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, roleadmin.Name);
                userManager.AddToRole(admin.Id, roleuser.Name);
            }

            var simpleuser = new ApplicationUser { Email = "qwerty@mail.ru", UserName = "simpleuser", BoughtLists = new List<BoughtList>(), Name = "Test", Surname = "Test2" };
            password = "Qwerty222";
            result = userManager.Create(simpleuser, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(simpleuser.Id, roleuser.Name);
            }

            Component meat = new Component() { Name = "Meat" };
            Component cheese = new Component() { Name = "Cheese" };
            Component tomatos = new Component() { Name = "Tomato" };
            Component sauce = new Component() { Name = "Sauce" };
            Component mushrooms = new Component() { Name = "Mushroom" };
            Component olives = new Component() { Name = "Olive" };

            Pizza pizza = new Pizza()
            {
                Name = "Proshuto",
                Price = 100,
                Size = "Large",
               Components = new List<Component>() { meat, sauce, cheese }
            };
            context.Pizzas.Add(pizza);

            pizza = new Pizza()
            {
                Name = "Polo",
                Price = 100,
                Size = "Large",
                Components = new List<Component>() { meat, tomatos, mushrooms }
            };
            context.Pizzas.Add(pizza);

            pizza = new Pizza()
            {
                Name = "QuatroFormadgi",
                Price = 100,
                Size = "Large",
                Components = new List<Component>() { meat, olives, sauce }
            };
            context.Pizzas.Add(pizza);

            context.SaveChanges();
        }

    }
}