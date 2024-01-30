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
    public class AeroporturiController : Controller
    {
        private IntVirtualEntities db = new IntVirtualEntities();

        // GET: Aeroporturi
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NumeParm = String.IsNullOrEmpty(sortOrder) ? "nume_desc" : "";
            ViewBag.CurrentFilter = searchString;

            var aeroporturi = from s in db.AEROPORTURIs
                              select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                aeroporturi = aeroporturi.Where(s => s.TARA.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nume_desc":
                    aeroporturi = aeroporturi.OrderByDescending(s => s.NUME_AEROPORT);
                    break;
                default:
                    aeroporturi = aeroporturi.OrderBy(s => s.NUME_AEROPORT);
                    break;
            }

            return View(aeroporturi.ToList());
        }

        // GET: Aeroporturi/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AEROPORTURI aEROPORTURI = db.AEROPORTURIs.Find(id);
            if (aEROPORTURI == null)
            {
                return HttpNotFound();
            }
            return View(aEROPORTURI);
        }

        // GET: Aeroporturi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aeroporturi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_AEROPORTURI,NUME_AEROPORT,COD_AEROPORT,ORAS,TARA,NR_PISTE,NR_PORTI")] AEROPORTURI aEROPORTURI)
        {
            if (ModelState.IsValid)
            {
                db.AEROPORTURIs.Add(aEROPORTURI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aEROPORTURI);
        }

        // GET: Aeroporturi/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AEROPORTURI aEROPORTURI = db.AEROPORTURIs.Find(id);
            if (aEROPORTURI == null)
            {
                return HttpNotFound();
            }
            return View(aEROPORTURI);
        }

        // POST: Aeroporturi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_AEROPORTURI,NUME_AEROPORT,COD_AEROPORT,ORAS,TARA,NR_PISTE,NR_PORTI")] AEROPORTURI aEROPORTURI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aEROPORTURI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aEROPORTURI);
        }

        // GET: Aeroporturi/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AEROPORTURI aEROPORTURI = db.AEROPORTURIs.Find(id);
            if (aEROPORTURI == null)
            {
                return HttpNotFound();
            }
            return View(aEROPORTURI);
        }

        // POST: Aeroporturi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            AEROPORTURI aEROPORTURI = db.AEROPORTURIs.Find(id);
            db.AEROPORTURIs.Remove(aEROPORTURI);
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
