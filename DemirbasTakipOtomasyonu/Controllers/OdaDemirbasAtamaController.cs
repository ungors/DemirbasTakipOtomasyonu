using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemirbasTakipOtomasyonu.Models;
using Rotativa;

namespace DemirbasTakipOtomasyonu.Controllers
{
    public class OdaDemirbasAtamaController : Controller
    {
        private OtomasyonEntities db = new OtomasyonEntities();
        

        // GET: OdaDemirbasAtama
        [Authorize(Roles = "admin,yetkili")]
        public ActionResult Index(int? id)
        {
            var odaDemirbasAtama = db.OdaDemirbasAtama.Include(o => o.Demirbas).Include(o => o.Oda).AsQueryable();
            if (id != null)
            {
                odaDemirbasAtama = odaDemirbasAtama.Where(i => i.OdaId == id);
            }
            return View(odaDemirbasAtama.ToList());
        }

        public ActionResult PrintAllReport(int? id)
        {
           
            var  model = db.OdaDemirbasAtama.Where(i => i.OdaId == id);      
            ViewAsPdf pdf = new ViewAsPdf("Index", model);
            return pdf;

        }

        public ActionResult Print()
        {
            var model = db.OdaDemirbasAtama;
            ViewAsPdf pdf = new ViewAsPdf("Index", model);
            return pdf;
        }

        // GET: OdaDemirbasAtama/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OdaDemirbasAtama odaDemirbasAtama = db.OdaDemirbasAtama.Find(id);
            if (odaDemirbasAtama == null)
            {
                return HttpNotFound();
            }
            return View(odaDemirbasAtama);
        }

        // GET: OdaDemirbasAtama/Create
        public ActionResult Create()
        {
            ViewBag.DemirbasId = new SelectList(db.Demirbas, "Id", "Ad");
            ViewBag.OdaId = new SelectList(db.Oda, "Id", "Ad");
            
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else if (User.IsInRole("yetkili"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: OdaDemirbasAtama/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OdaId,DemirbasId,AtananAdet")] OdaDemirbasAtama odaDemirbasAtama)
        {
            Demirbas demirbas = new Demirbas();
            demirbas = db.Demirbas.Find(odaDemirbasAtama.DemirbasId);
            if (odaDemirbasAtama.AtananAdet > demirbas.Adet)
            {
                TempData["Stok"] = "Girdiğiniz değer stoktaki değerden fazladır.";
                return RedirectToAction("Create");
            }

            else if (odaDemirbasAtama.AtananAdet <= demirbas.Adet)
            {
                //demirbas.Adet = demirbas.Adet - odaDemirbasAtama.AtananAdet;
                //return RedirectToAction("Create");           

                //if (ModelState.IsValid)
                //{
                    demirbas.Adet = demirbas.Adet - odaDemirbasAtama.AtananAdet;
                    db.OdaDemirbasAtama.Add(odaDemirbasAtama);
                    db.SaveChanges();
                    return RedirectToAction("Index");
             }

                ViewBag.DemirbasId = new SelectList(db.Demirbas, "Id", "Ad", odaDemirbasAtama.DemirbasId);
                ViewBag.OdaId = new SelectList(db.Oda, "Id", "Ad", odaDemirbasAtama.OdaId);
                return View(odaDemirbasAtama);
            }
        

        // GET: OdaDemirbasAtama/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OdaDemirbasAtama odaDemirbasAtama = db.OdaDemirbasAtama.Find(id);
            //var adet1 = odaDemirbasAtama.AtananAdet;
            //TempData["adet"] = adet1;
            //Demirbas demirbas = new Demirbas();
            //demirbas = db.Demirbas.Find(odaDemirbasAtama.DemirbasId);
            //TempData["fark"] = 0;
            //if(odaDemirbasAtama.AtananAdet < demirbas.Adet)
            //{
            //    TempData["fark"] = demirbas.Adet - odaDemirbasAtama.AtananAdet;
            //}
            //else if(odaDemirbasAtama.AtananAdet > demirbas.Adet)
            //{
            //    TempData["fark"] = odaDemirbasAtama.AtananAdet - demirbas.Adet;
            //}

            if (odaDemirbasAtama == null)
            {
                return HttpNotFound();
            }
            ViewBag.DemirbasId = new SelectList(db.Demirbas, "Id", "Ad", odaDemirbasAtama.DemirbasId);
            ViewBag.OdaId = new SelectList(db.Oda, "Id", "Ad", odaDemirbasAtama.OdaId);
            return View(odaDemirbasAtama);
        }

        // POST: OdaDemirbasAtama/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OdaId,DemirbasId,AtananAdet")] OdaDemirbasAtama odaDemirbasAtama)
        {
            Demirbas demirbas = new Demirbas();
            demirbas = db.Demirbas.Find(odaDemirbasAtama.DemirbasId); //Demirbaşın stoktaki adeti
            //var adet2 = odaDemirbasAtama.AtananAdet;
            //var adet = TempData["adet"]
            if (ModelState.IsValid)
            {
                db.Entry(odaDemirbasAtama).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DemirbasId = new SelectList(db.Demirbas, "Id", "Ad", odaDemirbasAtama.DemirbasId);
            ViewBag.OdaId = new SelectList(db.Oda, "Id", "Ad", odaDemirbasAtama.OdaId);
            return View(odaDemirbasAtama);
        }

        // GET: OdaDemirbasAtama/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OdaDemirbasAtama odaDemirbasAtama = db.OdaDemirbasAtama.Find(id);
            if (odaDemirbasAtama == null)
            {
                return HttpNotFound();
            }

            //if (User.IsInRole("admin"))
            //{
            //    return View();
            //}
            //else if (User.IsInRole("yetkili"))
            //{
            //    return View("Hata");
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            return View(odaDemirbasAtama);
        }

        // POST: OdaDemirbasAtama/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OdaDemirbasAtama odaDemirbasAtama = db.OdaDemirbasAtama.Find(id);
            Demirbas demirbas = new Demirbas();
            demirbas = db.Demirbas.Find(odaDemirbasAtama.DemirbasId);
            db.OdaDemirbasAtama.Remove(odaDemirbasAtama);
            demirbas.Adet += odaDemirbasAtama.AtananAdet;
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
