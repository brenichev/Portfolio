using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticeWebApp1.Models;
using System.Web.Helpers;

namespace PracticeWebApp1.Controllers
{
    public class StagesController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();

        // GET: Stages
        [Authorize]
        public ActionResult Index()
        {
            var stages = db.Stages.Include(s => s.Adress).Include(s => s.Event);
            return View(stages.ToList());
        }

        // GET: Stages/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AdressId = new SelectList(db.Adresses, "idAdress", "Adress1");
            ViewBag.EventId = new SelectList(db.Events, "idEvents", "EventName");
            return View();
        }

        // POST: Stages/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idStage,StageNumber,EventId,StageName,AdressId,House,DateStart,DateFinish,StageCost,StageDesc")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Stages.Add(stage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdressId = new SelectList(db.Adresses, "idAdress", "Adress1", stage.AdressId);
            ViewBag.EventId = new SelectList(db.Events, "idEvents", "EventName", stage.EventId);
            return View(stage);
        }

        // GET: Stages/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdressId = new SelectList(db.Adresses, "idAdress", "Adress1", stage.AdressId);
            ViewBag.EventId = new SelectList(db.Events, "idEvents", "EventName", stage.EventId);
            return View(stage);
        }

        // POST: Stages/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idStage,StageNumber,EventId,StageName,AdressId,House,DateStart,DateFinish,StageCost,StageDesc")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdressId = new SelectList(db.Adresses, "idAdress", "Adress1", stage.AdressId);
            ViewBag.EventId = new SelectList(db.Events, "idEvents", "EventName", stage.EventId);
            return View(stage);
        }

        // GET: Stages/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stage stage = db.Stages.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        // POST: Stages/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stage stage = db.Stages.Find(id);
            db.Stages.Remove(stage);
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

        [Authorize]
        public void GetExcel()
        {
            List<Event> allCust = new List<Event>();
            EventsTestEntities1 dc = new EventsTestEntities1();

            var events = from m in db.Stages
                         select m;

            WebGrid grid = new WebGrid(source: events, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
                columns: grid.Columns(
                       grid.Column("StageNumber", "Номер этапа"),
        grid.Column("StageName", "Название этапа"),
        grid.Column("House", "Дом"),
        grid.Column("DateStart", "Дата начала этапа"),
        grid.Column("DateFinish", "Дата окончания этапа"),
        grid.Column("StageCost", "Стоимость"),
        grid.Column("StageDesc", "Описание"),
        grid.Column("Adress.Adress1", "Улица"),
        grid.Column("Event.EventName", "Название мероприятия")
                        )
                    ).ToString();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=CustomerInfo.xls");
            Response.ContentType = "application/excel";
            Response.Write(gridData);
            Response.End();
        }
    }
}
