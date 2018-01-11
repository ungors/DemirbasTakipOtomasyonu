using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DemirbasTakipOtomasyonu.Models;
using Rotativa;

namespace DemirbasTakipOtomasyonu.Controllers
{
    public class DemirbasController : Controller
    {
        private OtomasyonEntities db = new OtomasyonEntities();

        // GET: Demirbas
        //[Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var demirbas = db.Demirbas.Include(d => d.DemirbasTuru).Include(d => d.Departman).Include(d => d.Fakulte);
            if (User.IsInRole("admin"))
            {
                return View(demirbas.ToList());
            }  
            else if(User.IsInRole("yetkili"))
            {
                return View("Hata");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        public ActionResult PrintStock()
        {
            var model = db.Demirbas;
            ViewAsPdf pdf = new ViewAsPdf("Index", model);
            return pdf;
        }

        // GET: Demirbas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demirbas demirbas = db.Demirbas.Find(id);
            if (demirbas == null)
            {
                return HttpNotFound();
            }
            return View(demirbas);
        }

        // GET: Demirbas/Create
        //[Authorize(Roles ="admin")]
        public ActionResult Create()
        {
            ViewBag.DemirbasTurId = new SelectList(db.DemirbasTuru, "Id", "Ad");
            //ViewBag.FakulteId = new SelectList(db.Fakulte, "Id", "Ad");
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad");

            /*var roles =*//* User.IsInRole("admin");*/
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else if(User.IsInRole("yetkili"))
            {
                return View("Hata");
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }

        // POST: Demirbas/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad,Fiyat,AlımTarihi,Adet,DemirbasTurId,FakulteId,DepartmanId")] Demirbas demirbas)
        {
            if (ModelState.IsValid)
            {
                Departman departman = new Departman();
                departman = db.Departman.Find(demirbas.DepartmanId);
                demirbas.FakulteId = departman.FakulteId;
                demirbas.AlımTarihi = DateTime.Now;
                db.Demirbas.Add(demirbas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DemirbasTurId = new SelectList(db.DemirbasTuru, "Id", "Ad", demirbas.DemirbasTurId);
            //ViewBag.FakulteId = new SelectList(db.Fakulte, "Id", "Ad", demirbas.FakulteId);
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad", demirbas.DepartmanId);
            return View(demirbas);
        }

        // GET: Demirbas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demirbas demirbas = db.Demirbas.Find(id);
            if (demirbas == null)
            {
                return HttpNotFound();
            }
            ViewBag.DemirbasTurId = new SelectList(db.DemirbasTuru, "Id", "Ad", demirbas.DemirbasTurId);
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad", demirbas.DepartmanId);
            ViewBag.FakulteId = new SelectList(db.Fakulte, "Id", "Ad", demirbas.FakulteId);
            return View(demirbas);
        }

        // POST: Demirbas/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,Fiyat,AlımTarihi,Adet,DemirbasTurId,FakulteId,DepartmanId")] Demirbas demirbas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demirbas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DemirbasTurId = new SelectList(db.DemirbasTuru, "Id", "Ad", demirbas.DemirbasTurId);
            ViewBag.DepartmanId = new SelectList(db.Departman, "Id", "Ad", demirbas.DepartmanId);
            ViewBag.FakulteId = new SelectList(db.Fakulte, "Id", "Ad", demirbas.FakulteId);
            return View(demirbas);
        }

        // GET: Demirbas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demirbas demirbas = db.Demirbas.Find(id);
            if (demirbas == null)
            {
                return HttpNotFound();
            }
            return View(demirbas);
        }

        // POST: Demirbas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demirbas demirbas = db.Demirbas.Find(id);
            db.Demirbas.Remove(demirbas);
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
