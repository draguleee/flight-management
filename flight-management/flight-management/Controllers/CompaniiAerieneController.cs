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
    public class CompaniiAerieneController : Controller
    {
        private IntVirtualEntities db = new IntVirtualEntities();

        // GET: CompaniiAeriene
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.AnSortParm = String.IsNullOrEmpty(sortOrder) ? "an_desc" : "";
            ViewBag.CurrentFilter = searchString;

            var companii = from s in db.COMPANII_AERIENE
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                companii = companii.Where(s => s.TARA.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "an_desc":
                    companii = companii.OrderByDescending(s => s.AN_INFIINTARE);
                    break;
                default:
                    companii = companii.OrderBy(s => s.AN_INFIINTARE);
                    break;
            }

            return View(companii.ToList());
        }

        // GET: CompaniiAeriene/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANII_AERIENE cOMPANII_AERIENE = db.COMPANII_AERIENE.Find(id);
            if (cOMPANII_AERIENE == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANII_AERIENE);
        }

        // GET: CompaniiAeriene/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompaniiAeriene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_COMPANII,NUME_COMPANIE,COD_COMPANIE,TARA,AN_INFIINTARE")] COMPANII_AERIENE cOMPANII_AERIENE)
        {
            if (ModelState.IsValid)
            {
                db.COMPANII_AERIENE.Add(cOMPANII_AERIENE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cOMPANII_AERIENE);
        }

        // GET: CompaniiAeriene/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANII_AERIENE cOMPANII_AERIENE = db.COMPANII_AERIENE.Find(id);
            if (cOMPANII_AERIENE == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANII_AERIENE);
        }

        // POST: CompaniiAeriene/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_COMPANII,NUME_COMPANIE,COD_COMPANIE,TARA,AN_INFIINTARE")] COMPANII_AERIENE cOMPANII_AERIENE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMPANII_AERIENE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOMPANII_AERIENE);
        }

        // GET: CompaniiAeriene/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMPANII_AERIENE cOMPANII_AERIENE = db.COMPANII_AERIENE.Find(id);
            if (cOMPANII_AERIENE == null)
            {
                return HttpNotFound();
            }
            return View(cOMPANII_AERIENE);
        }

        // POST: CompaniiAeriene/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            COMPANII_AERIENE cOMPANII_AERIENE = db.COMPANII_AERIENE.Find(id);
            db.COMPANII_AERIENE.Remove(cOMPANII_AERIENE);
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
