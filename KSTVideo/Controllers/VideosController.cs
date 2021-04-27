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
    public class VideosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       

        // GET: Videos
        public ActionResult Index()
        {

            var service = GetService();
            var videos = service.GetVideos();
            return View(videos);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = GetService();

            VideoDetail video = service.GetVideoById(id);
           
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VideoCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = GetService();

            service.CreateVideo(model);

            return RedirectToAction("Index");
        }

        

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var service = GetService();

            VideoDetail video = service.GetVideoById(id);

            if (video == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "ID", "Name", video.GenreID);
            return View(video);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VideoUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = GetService();

            if (service.UpdateVideo(model))
            {
                TempData["SaveResult"] = "Your video was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your video could not be updated.");
            return View(model);
        }

       

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var service = GetService();
            var video = service.GetVideoById(id);

            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var service = GetService();
            service.DeleteVideo(id);
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


        private VideoService GetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new VideoService(userId);
            return service;
        }
    }
}
