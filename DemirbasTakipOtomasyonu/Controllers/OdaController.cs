using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemirbasTakipOtomasyonu.Models;

namespace DemirbasTakipOtomasyonu.Controllers
{
    public class OdaController : Controller
    {
        private OtomasyonEntities db = new OtomasyonEntities();

        // GET: Oda
        [Authorize(Roles = "admin,yetkili")]
        public ActionResult Index(int? id)
        {
            var oda = db.Oda.Include(o => o.Departman).Include(o => o.Personel).AsQueryable();

            if(id!=null)
            {
                oda = oda.Where(i => i.PersonelId == id);
            }
            return View(oda.ToList());
        }

        // GET: Oda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oda oda = db.Oda.Find(id);
            if (oda == null)
            {
                return HttpNotFound();
            }
            return View(oda);
        }

        // GET: Oda/Create
        //[Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad");
            ViewBag.PersonelId = new SelectList(db.Personel, "Id", "Ad");

            if (User.IsInRole("admin"))
            {
                return View();
            }
            else if (User.IsInRole("yetkili"))
            {
                return View("Hata");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Oda/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad,DepartmanId,PersonelId")] Oda oda)
        {
            if (ModelState.IsValid)
            {
                db.Oda.Add(oda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad", oda.DepartmanId);
            ViewBag.PersonelId = new SelectList(db.Personel, "Id", "Ad", oda.PersonelId);
            return View(oda);
        }

        // GET: Oda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oda oda = db.Oda.Find(id);
            if (oda == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad", oda.DepartmanId);
            ViewBag.PersonelId = new SelectList(db.Personel, "Id", "Ad", oda.PersonelId);
            return View(oda);
        }

        // POST: Oda/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,DepartmanId,PersonelId")] Oda oda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad", oda.DepartmanId);
            ViewBag.PersonelId = new SelectList(db.Personel, "Id", "Ad", oda.PersonelId);
            return View(oda);
        }

        // GET: Oda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Oda oda = db.Oda.Find(id);
                if (oda == null)
                {
                    return HttpNotFound();
                }
                return View(oda);
            }
            else if (User.IsInRole("yetkili"))
            {
                return View("Hata");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Oda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oda oda = db.Oda.Find(id);
            db.Oda.Remove(oda);
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
