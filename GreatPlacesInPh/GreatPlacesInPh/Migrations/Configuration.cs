namespace GreatPlacesInPh.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GreatPlacesInPh.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    internal sealed class Configuration : DbMigrationsConfiguration<GreatPlacesInPh.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GreatPlacesInPh.Models.ApplicationDbContext context)
        {                 
            if (!context.Roles.Any(r => r.Name == ApplicationConstants.GREAT_PLACES_PH_ADMIN_ROLE))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = ApplicationConstants.GREAT_PLACES_PH_ADMIN_ROLE };
                manager.Create(role);
            }

            string defaultusername = "jentrinanes@hotmail.com";

            if (!context.Users.Any(u => u.UserName == defaultusername))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                manager.UserValidator = new UserValidator<ApplicationUser>(manager) { AllowOnlyAlphanumericUserNames = false };

                var user = new ApplicationUser { UserName = defaultusername, Email = "jentrinanes@hotmail.com", EmailConfirmed = true, FullName = "Jen Triñanes" };
                var identityresult = manager.Create(user, "P@ssword1");

                var newuser = context.Users.Where(u => u.UserName == defaultusername).SingleOrDefault();
                manager.AddToRole(newuser.Id, ApplicationConstants.GREAT_PLACES_PH_ADMIN_ROLE);
            }

            var admin = context.Users.Where(u => u.UserName == defaultusername).SingleOrDefault();
            context.Places.AddOrUpdate(x => x.Id,
                new Place { Name = "Vigan, Ilocos", ImageUrl = "https://greentravelphilippines.files.wordpress.com/2011/04/vigan02.jpg", Review = "A historical place! Feels like living in the Spanish times!", UserId = admin.Id },
                new Place { Name = "Coron, Palawan", ImageUrl = "http://www.clubparadisepalawan.com/files/2015/04/27.jpg", Review = "Paradise on earth! Can't leave this place!", UserId = admin.Id },
                new Place { Name = "Samal Island, Davao", ImageUrl = "http://outoftownblog.com/wp-content/uploads/2012/05/Buenavista-Island-Resort-in-Samal-500x308.jpg", Review = "White sand, friendly people! several minutes away from Davao city!", UserId = admin.Id });
        }
    }
}
