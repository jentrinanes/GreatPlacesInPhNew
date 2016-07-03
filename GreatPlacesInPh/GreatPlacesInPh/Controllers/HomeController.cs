using GreatPlacesInPh.Models;
using GreatPlacesInPh.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreatPlacesInPh.Attributes;

namespace GreatPlacesInPh.Controllers
{
    [ContentSecurityPolicyFilter]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var viewModel = (from p in db.Places                      
                             select new PlaceViewModel {
                                 PlaceId = p.Id,
                                 Name = p.Name,
                                 User = new UserViewModel() {
                                     UserId = db.Users.FirstOrDefault(x => x.Id == p.UserId).Id,
                                     UserName = db.Users.FirstOrDefault(x => x.Id == p.UserId).FullName
                                 },
                                 ImageUrl = p.ImageUrl,
                                 Review = p.Review
                          }).ToList();           
           
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}