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
    public class PasageriController : Controller
    {
        private IntVirtualEntities db = new IntVirtualEntities();

        // GET: Pasageri
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DataSortParm = String.IsNullOrEmpty(sortOrder) ? "data_nasterii_desc" : "";
            ViewBag.CurrentFilter = searchString;

            var pasageri = from s in db.PASAGERIs
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pasageri = pasageri.Where(s => s.NATIONALITATE.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "data_nasterii_desc":
                    pasageri = pasageri.OrderByDescending(s => s.DATA_NASTERII);
                    break;
                default:
                    pasageri = pasageri.OrderBy(s => s.DATA_NASTERII);
                    break;
            }

            return View(pasageri.ToList());
        }

        // GET: Pasageri/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASAGERI pASAGERI = db.PASAGERIs.Find(id);
            if (pASAGERI == null)
            {
                return HttpNotFound();
            }
            return View(pASAGERI);
        }

        // GET: Pasageri/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pasageri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PASAGERI,NUME_PASAGER,PRENUME_PASAGER,DATA_NASTERII,NATIONALITATE,NR_PASAPORT,GEN,NR_TEL")] PASAGERI pASAGERI)
        {
            if (ModelState.IsValid)
            {
                db.PASAGERIs.Add(pASAGERI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pASAGERI);
        }

        // GET: Pasageri/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASAGERI pASAGERI = db.PASAGERIs.Find(id);
            if (pASAGERI == null)
            {
                return HttpNotFound();
            }
            return View(pASAGERI);
        }

        // POST: Pasageri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PASAGERI,NUME_PASAGER,PRENUME_PASAGER,DATA_NASTERII,NATIONALITATE,NR_PASAPORT,GEN,NR_TEL")] PASAGERI pASAGERI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pASAGERI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pASAGERI);
        }

        // GET: Pasageri/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PASAGERI pASAGERI = db.PASAGERIs.Find(id);
            if (pASAGERI == null)
            {
                return HttpNotFound();
            }
            return View(pASAGERI);
        }

        // POST: Pasageri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PASAGERI pASAGERI = db.PASAGERIs.Find(id);
            db.PASAGERIs.Remove(pASAGERI);
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
