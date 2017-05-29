using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MMSDA.Models;
using MMSDA.Global;
using Microsoft.AspNet.Identity;

namespace MMSDA.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Album
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Albums.OrderBy(a => a.Artist.Name).ToList());
        }

        // GET: Album/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();

            return View(album);
        }

        // GET: Album/Create
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Create()
        {
            ViewBag.Artists = db.Artists.ToList();
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Create([Bind(Include = "AlbumID,Name,Genre,Year,ArtistRef")] Album album)
        {
            if (ModelState.IsValid)
            {

                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Album/Edit/5
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.Artists = db.Artists.ToList();
        
            return View(album);
        }

        // POST: Album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Edit([Bind(Include = "AlbumID,Name,Genre,Year,ArtistRef")] Album album)
        {
            if (ModelState.IsValid)
            {

                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Album/Delete/5
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebConfig.adminRole)]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
