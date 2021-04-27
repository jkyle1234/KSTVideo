using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KSTVideo.Data;
using KSTVideo.Models;
using KSTVideo.Services;
using Microsoft.AspNet.Identity;

namespace KSTVideo.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genres
        public ActionResult Index()
        {
            var service = GetService();
            var genres = service.GetGenres();
            return View(genres);
        }

        // GET: Genres/Details/5
        public ActionResult Details(int id)
        {
            
            var service = GetService();

            GenreDetail genre = service.GetGenreById(id);

            if (genre == null)
            {
                return HttpNotFound();
            }
           
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GenreCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetService();

            service.CreateGenre(model);

            return RedirectToAction("Index");
        }


        

        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GenreUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = GetService();

            if (service.UpdateGenre(model))
            {
                TempData["SaveResult"] = "Genre was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Genre could not be updated.");
            return View(model);
        }



        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name")] Genre genre)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(genre).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(genre);
        //}

        // GET: Genres/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Genre genre = db.Genres.Find(id);
        //    if (genre == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(genre);
        //}


        public ActionResult Delete(int id)
        {
           

            var service = GetService();
            var genre = service.GetGenreById(id);

            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var service = GetService();
            service.DeleteGenre(id);
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

        private GenreService GetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GenreService(userId);
            return service;
        }
    }
}
