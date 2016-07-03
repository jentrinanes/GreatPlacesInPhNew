using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreatPlacesInPh.Models;
using GreatPlacesInPh.ViewModels;
using Microsoft.AspNet.Identity;

namespace GreatPlacesInPh.Controllers
{ 
    public class PlacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Places
        public async Task<ActionResult> Index()
        {
            return View(await db.Places.Include(r => r.Comments).ToListAsync());
        }        

        // GET: Places/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var place = db.Places.Include(c => c.Comments).Where(p => p.Id == id).SingleOrDefault();            

            List<CommentViewModel> commentViewModel = new List<CommentViewModel>();
            if (place.Comments != null || place.Comments.Count != 0)
            {
                foreach (var c in place.Comments)
                {
                    commentViewModel.Add(new CommentViewModel()
                    {
                        Id = c.Id,
                        User = new UserViewModel()
                        {
                            UserId = db.Users.FirstOrDefault(x => x.Id == c.UserId).Id,
                            UserName = db.Users.FirstOrDefault(x => x.Id == c.UserId).FullName
                        },
                        Comment = c.Message
                    });
                }
            }
            
            var viewModel = new PlaceViewModel() {
                PlaceId = place.Id,
                Name = place.Name,
                User = new UserViewModel() {
                    UserId = db.Users.FirstOrDefault(x => x.Id == place.UserId).Id,
                    UserName = db.Users.FirstOrDefault(x => x.Id == place.UserId).FullName
                }, 
                ImageUrl = place.ImageUrl,
                Review = place.Review,
                Comments = commentViewModel
            };

            if (place == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }

        // GET: Places/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
       
        //[ValidateInput(false)]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,ImageUrl,Review")] PlaceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Place place = new Place()
                {
                    Id = Guid.NewGuid(),
                    Name = viewModel.Name,
                    ImageUrl = viewModel.ImageUrl,
                    Review = viewModel.Review,
                    UserId = User.Identity.GetUserId()
                };
                
                db.Places.Add(place);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel);
        }

        // GET: Places/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = await db.Places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ImageUrl")] Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Places/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = await db.Places.FindAsync(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Place place = await db.Places.FindAsync(id);
            db.Places.Remove(place);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
