using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Models;
using System.Net;
using System.Data.Entity;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private PBContext db = new PBContext();

        public ActionResult Index(PBFilterModel model)
        {
            model.Subscribers = db.Subscribers
                .Where(x =>
                    (model.PhoneNumber == null || x.PhoneNumber.Contains(model.PhoneNumber)) &&
                    (model.Name == null || x.Name.Contains(model.Name))
                );
            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return null;
            }
            return File(subscriber.ImageData, subscriber.ImageMimeType);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Subscriber subscriber, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentType.StartsWith("image/"))
                {
                    subscriber.ImageMimeType = image.ContentType;
                    subscriber.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(subscriber.ImageData, 0, image.ContentLength);
                }
                db.Subscribers.Add(subscriber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscriber);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Subscriber subscriber, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    subscriber.ImageMimeType = image.ContentType;
                    subscriber.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(subscriber.ImageData, 0, image.ContentLength);
                }
                db.Entry(subscriber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscriber);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            db.Subscribers.Remove(subscriber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}