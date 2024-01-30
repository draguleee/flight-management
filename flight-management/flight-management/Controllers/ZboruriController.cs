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
    public class ZboruriController : Controller
    {
        private IntVirtualEntities db = new IntVirtualEntities();

        // GET: Zboruri
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DurataSortParm = String.IsNullOrEmpty(sortOrder) ? "durata_desc" : "";
            ViewBag.CurrentFilter = searchString;

            var tipuri = from s in db.ZBORURIs
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tipuri = tipuri.Where(s => s.TIP_AVION.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "durata_desc":
                    tipuri = tipuri.OrderByDescending(s => s.DURATA_ZBOR);
                    break;
                default:
                    tipuri = tipuri.OrderBy(s => s.DURATA_ZBOR);
                    break;
            }

            return View(tipuri.ToList());
        }

        // GET: Zboruri/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZBORURI zBORURI = db.ZBORURIs.Find(id);
            if (zBORURI == null)
            {
                return HttpNotFound();
            }
            return View(zBORURI);
        }

        // GET: Zboruri/Create
        public ActionResult Create()
        {
            ViewBag.ID_AEROPORTURI = new SelectList(db.AEROPORTURIs, "ID_AEROPORTURI", "NUME_AEROPORT");
            ViewBag.ID_COMPANII = new SelectList(db.COMPANII_AERIENE, "ID_COMPANII", "NUME_COMPANIE");
            ViewBag.AeroporturiList = db.AEROPORTURIs.ToList();
            return View();
        }

        // POST: Zboruri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ZBORURI,ID_COMPANII,AEROPORT_PLECARE,AEROPORT_SOSIRE,ORA_PLECARE,ORA_SOSIRE,DURATA_ZBOR,TIP_AVION,ID_AEROPORTURI")] ZBORURI zBORURI)
        {
            if (ModelState.IsValid)
            {
                db.ZBORURIs.Add(zBORURI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_AEROPORTURI = new SelectList(db.AEROPORTURIs, "ID_AEROPORTURI", "NUME_AEROPORT", zBORURI.ID_AEROPORTURI);
            ViewBag.ID_COMPANII = new SelectList(db.COMPANII_AERIENE, "ID_COMPANII", "NUME_COMPANIE", zBORURI.ID_COMPANII);
            return View(zBORURI);
        }

        // GET: Zboruri/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZBORURI zBORURI = db.ZBORURIs.Find(id);
            if (zBORURI == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_AEROPORTURI = new SelectList(db.AEROPORTURIs, "ID_AEROPORTURI", "NUME_AEROPORT", zBORURI.ID_AEROPORTURI);
            ViewBag.ID_COMPANII = new SelectList(db.COMPANII_AERIENE, "ID_COMPANII", "NUME_COMPANIE", zBORURI.ID_COMPANII);
            return View(zBORURI);
        }

        // POST: Zboruri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ZBORURI,ID_COMPANII,AEROPORT_PLECARE,AEROPORT_SOSIRE,ORA_PLECARE,ORA_SOSIRE,DURATA_ZBOR,TIP_AVION,ID_AEROPORTURI")] ZBORURI zBORURI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zBORURI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_AEROPORTURI = new SelectList(db.AEROPORTURIs, "ID_AEROPORTURI", "NUME_AEROPORT", zBORURI.ID_AEROPORTURI);
            ViewBag.ID_COMPANII = new SelectList(db.COMPANII_AERIENE, "ID_COMPANII", "NUME_COMPANIE", zBORURI.ID_COMPANII);
            return View(zBORURI);
        }

        // GET: Zboruri/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZBORURI zBORURI = db.ZBORURIs.Find(id);
            if (zBORURI == null)
            {
                return HttpNotFound();
            }
            return View(zBORURI);
        }

        // POST: Zboruri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ZBORURI zBORURI = db.ZBORURIs.Find(id);
            db.ZBORURIs.Remove(zBORURI);
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
