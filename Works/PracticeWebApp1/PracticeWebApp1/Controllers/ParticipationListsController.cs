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
    public class ParticipationListsController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();

        // GET: ParticipationLists
        [Authorize]
        public ActionResult Index()
        {
            var participationLists = db.ParticipationLists.Include(p => p.Member).Include(p => p.Stage);
            return View(participationLists.ToList());
        }

        // GET: ParticipationLists/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationList participationList = db.ParticipationLists.Find(id);
            if (participationList == null)
            {
                return HttpNotFound();
            }
            return View(participationList);
        }

        // GET: ParticipationLists/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "idMember", "MemberSurname");
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text");
            //ViewBag.StageId = new SelectList(db.Stages, "idStage", "StageName + ' ' + DateStart");
            return View();
        }

        // POST: ParticipationLists/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idPart,StageId,MemberId")] ParticipationList participationList)
        {
            if (ModelState.IsValid)
            {
                db.ParticipationLists.Add(participationList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "idMember", "MemberSurname", participationList.MemberId);
            //ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "idStage", "StageName", participationList.StageId);
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text");
            return View(participationList);
        }

        // GET: ParticipationLists/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationList participationList = db.ParticipationLists.Find(id);
            if (participationList == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "idMember", "MemberSurname", participationList.MemberId);
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text", participationList.StageId);
            return View(participationList);
        }

        // POST: ParticipationLists/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPart,StageId,MemberId")] ParticipationList participationList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participationList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "idMember", "MemberSurname", participationList.MemberId);
            ViewBag.StageId = new SelectList(db.Stages.AsEnumerable().Select(x => new { Value = x.idStage, Text = x.StageName + "  " + (x.DateStart).ToString("d-MM-yyyy HH:mm") }), "Value", "Text", participationList.StageId);
            return View(participationList);
        }

        // GET: ParticipationLists/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParticipationList participationList = db.ParticipationLists.Find(id);
            if (participationList == null)
            {
                return HttpNotFound();
            }
            return View(participationList);
        }

        // POST: ParticipationLists/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParticipationList participationList = db.ParticipationLists.Find(id);
            db.ParticipationLists.Remove(participationList);
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

            var events = from m in db.ParticipationLists
                         select m;

            WebGrid grid = new WebGrid(source: events, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
                columns: grid.Columns(
                        grid.Column("Member.MemberSurname", "Фамилия участника"),
                        grid.Column("Member.MemberName", "Имя участника"),
                        grid.Column("Stage.StageName", "Название этапа"),
                        grid.Column("Stage.DateStart", "Дата начала этапа")
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
