using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnTwoWheels.Models;

namespace OnTwoWheels.Controllers
{
    [Authorize]
    public class AspNetUserPostsController : Controller
    {
        private Entities1 db = new Entities1();

        [AllowAnonymous]
        // GET: AspNetUserPosts
        public ActionResult Index()
        {
            var aspNetUserPosts = db.AspNetUserPosts.Include(a => a.AspNetUser);
            return View(aspNetUserPosts.ToList());
        }

        [AllowAnonymous]
        // GET: AspNetUserPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserPost aspNetUserPost = db.AspNetUserPosts.Find(id);
            if (aspNetUserPost == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserPost);
        }

        // GET: AspNetUserPosts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: AspNetUserPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Tags,Thumbnail,DateCreated,UserId")] AspNetUserPost aspNetUserPost, HttpPostedFileBase thumbnail)
        {
            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    aspNetUserPost.Thumbnail = new byte[thumbnail.ContentLength];
                    thumbnail.InputStream.Read(aspNetUserPost.Thumbnail, 0, thumbnail.ContentLength);
                }
                
                
                aspNetUserPost.DateCreated = DateTime.ParseExact(DateTime.Now.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                db.AspNetUserPosts.Add(aspNetUserPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserPost.UserId);
            return View(aspNetUserPost);
        }

        // GET: AspNetUserPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserPost aspNetUserPost = db.AspNetUserPosts.Find(id);
            if (aspNetUserPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserPost.UserId);
            return View(aspNetUserPost);
        }

        // POST: AspNetUserPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Tags,Thumbnail,DateCreated,UserId")] AspNetUserPost aspNetUserPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserPost.UserId);
            return View(aspNetUserPost);
        }

        // GET: AspNetUserPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserPost aspNetUserPost = db.AspNetUserPosts.Find(id);
            if (aspNetUserPost == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserPost);
        }

        // POST: AspNetUserPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUserPost aspNetUserPost = db.AspNetUserPosts.Find(id);
            db.AspNetUserPosts.Remove(aspNetUserPost);
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
