using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using flight_management.Models;

namespace flight_management.Controllers
{
    public class BileteController : Controller
    {
        private IntVirtualEntities db = new IntVirtualEntities();

        // GET: Bilete
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RezervareSortParm = String.IsNullOrEmpty(sortOrder) ? "rezervare_desc" : "";
            ViewBag.CurrentFilter = searchString;

            var rezervari = from s in db.BILETEs
                         select s;

            switch (sortOrder)
            {
                case "rezervare_desc":
                    rezervari = rezervari.OrderByDescending(s => s.DATA_REZERVARE);
                    break;
                default:
                    rezervari = rezervari.OrderBy(s => s.DATA_REZERVARE);
                    break;
            }

            return View(rezervari.ToList());
        }

        // GET: Bilete/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILETE bILETE = db.BILETEs.Find(id);
            if (bILETE == null)
            {
                return HttpNotFound();
            }
            return View(bILETE);
        }

        // GET: Bilete/Create
        public ActionResult Create()
        {
            ViewBag.ID_PASAGERI = new SelectList(db.PASAGERIs, "ID_PASAGERI", "NUME_PASAGER");
            ViewBag.ID_ZBORURI = new SelectList(db.ZBORURIs, "ID_ZBORURI", "ID_ZBORURI");
            ViewBag.ZboruriList = db.ZBORURIs.ToList();
            return View();
        }

        // POST: Bilete/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BILETE,ID_PASAGERI,ID_ZBORURI,DATA_REZERVARE,CLASA,LOC,PRET")] BILETE bILETE)
        {
            if (ModelState.IsValid)
            {
                db.BILETEs.Add(bILETE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PASAGERI = new SelectList(db.PASAGERIs, "ID_PASAGERI", "NUME_PASAGER", bILETE.ID_PASAGERI);
            ViewBag.ID_ZBORURI = new SelectList(db.ZBORURIs, "ID_ZBORURI", "AEROPORT_PLECARE", bILETE.ID_ZBORURI);
            return View(bILETE);
        }

        // GET: Bilete/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILETE bILETE = db.BILETEs.Find(id);
            if (bILETE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PASAGERI = new SelectList(db.PASAGERIs, "ID_PASAGERI", "NUME_PASAGER", bILETE.ID_PASAGERI);
            ViewBag.ID_ZBORURI = new SelectList(db.ZBORURIs, "ID_ZBORURI", "AEROPORT_PLECARE", bILETE.ID_ZBORURI);
            return View(bILETE);
        }

        // POST: Bilete/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BILETE,ID_PASAGERI,ID_ZBORURI,DATA_REZERVARE,CLASA,LOC,PRET")] BILETE bILETE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bILETE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PASAGERI = new SelectList(db.PASAGERIs, "ID_PASAGERI", "NUME_PASAGER", bILETE.ID_PASAGERI);
            ViewBag.ID_ZBORURI = new SelectList(db.ZBORURIs, "ID_ZBORURI", "AEROPORT_PLECARE", bILETE.ID_ZBORURI);
            return View(bILETE);
        }

        // GET: Bilete/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BILETE bILETE = db.BILETEs.Find(id);
            if (bILETE == null)
            {
                return HttpNotFound();
            }
            return View(bILETE);
        }

        // POST: Bilete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            BILETE bILETE = db.BILETEs.Find(id);
            db.BILETEs.Remove(bILETE);
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
