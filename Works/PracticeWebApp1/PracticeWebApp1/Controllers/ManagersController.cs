using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticeWebApp1.Models;

namespace PracticeWebApp1.Controllers
{
    public class ManagersController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();

        // GET: Managers
        [Authorize]
        public ActionResult Index()
        {
            var managers = db.Managers.Include(m => m.ManagerType);
            return View(managers.ToList());
        }

        // GET: Managers/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ManagerTypeid = new SelectList(db.ManagerTypes, "idManagerType", "ManagerType1");
            return View();
        }

        // POST: Managers/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idManager,ManagerSurname,ManagerName,ManagerOtch,ManagerTypeid,ManagerLink,ManagerDesc")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Managers.Add(manager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerTypeid = new SelectList(db.ManagerTypes, "idManagerType", "ManagerType1", manager.ManagerTypeid);
            return View(manager);
        }

        // GET: Managers/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerTypeid = new SelectList(db.ManagerTypes, "idManagerType", "ManagerType1", manager.ManagerTypeid);
            return View(manager);
        }

        // POST: Managers/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idManager,ManagerSurname,ManagerName,ManagerOtch,ManagerTypeid,ManagerLink,ManagerDesc")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerTypeid = new SelectList(db.ManagerTypes, "idManagerType", "ManagerType1", manager.ManagerTypeid);
            return View(manager);
        }

        // GET: Managers/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
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
