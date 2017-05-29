using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MMSDA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MMSDA.Global;

namespace MMSDA.Controllers
{
    public class SongController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager { get; set; }

        public SongController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.db));
        }

        // GET: Song
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.InFavorites = false;

            return View(db.Songs.ToList());
        }

        // GET: Song/Favorites
        public ActionResult Favorites()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.InFavorites = true;

            var songs = db.Songs
                .Where(s => s.Users.Any(u => u.Id == user.Id))
                .ToList();
            return View("Index", songs);
        }

        // GET: Song/AddFavorite/5
        public ActionResult AddFavorite(int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Song song = db.Songs.Find(id);
            user.Songs.Add(song);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
        }

        // GET: Song/DeleteFavorite/5
        public ActionResult DeleteFavorite(int id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Song song = db.Songs.Find(id);
            user.Songs.Remove(song);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Redirect(HttpContext.Request.UrlReferrer.AbsolutePath);
        }

        // GET: Song/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();

            return View(song);
        }

        // GET: Song/Create
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Create()
        {
            ViewBag.Albums = db.Albums.ToList();

            return View();
        }

        // POST: Song/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Create([Bind(Include = "SongID,Title,Lyrics,Duration,AlbumRef")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: Song/Edit/5
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            ViewBag.Albums = db.Albums.ToList();

            return View(song);
        }

        // POST: Song/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Edit([Bind(Include = "SongID,Title,Lyrics,Duration,AlbumRef")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        // GET: Song/Delete/5
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Song/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
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
