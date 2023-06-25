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
    public class ManagersListsController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();

        // GET: ManagersLists
        [Authorize]
        public ActionResult Index()
        {
            var managersLists = db.ManagersLists.Include(m => m.Manager).Include(m => m.Stage);
            return View(managersLists.ToList());
        }

        // GET: ManagersLists/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagersList managersList = db.ManagersLists.Find(id);
            if (managersList == null)
            {
                return HttpNotFound();
            }
            return View(managersList);
        }

        // GET: ManagersLists/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.Managers, "idManager", "ManagerSurname");
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text");
            return View();
        }

        // POST: ManagersLists/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idForManager,StageId,ManagerId")] ManagersList managersList)
        {
            if (ModelState.IsValid)
            {
                db.ManagersLists.Add(managersList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerId = new SelectList(db.Managers, "idManager", "ManagerSurname", managersList.ManagerId);
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text");
            return View(managersList);
        }

        // GET: ManagersLists/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagersList managersList = db.ManagersLists.Find(id);
            if (managersList == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "idManager", "ManagerSurname", managersList.ManagerId);
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text", managersList.StageId);
            return View(managersList);
        }

        // POST: ManagersLists/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idForManager,StageId,ManagerId")] ManagersList managersList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managersList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.Managers, "idManager", "ManagerSurname", managersList.ManagerId);
            //ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "idStage", "StageName");
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text", managersList.StageId);
            return View(managersList);
        }

        // GET: ManagersLists/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagersList managersList = db.ManagersLists.Find(id);
            if (managersList == null)
            {
                return HttpNotFound();
            }
            return View(managersList);
        }

        // POST: ManagersLists/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManagersList managersList = db.ManagersLists.Find(id);
            db.ManagersLists.Remove(managersList);
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
