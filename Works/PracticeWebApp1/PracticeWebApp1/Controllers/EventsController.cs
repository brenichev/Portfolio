using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PracticeWebApp1.Models;
using System.Web.Helpers;
using System.Data.Entity.Validation;
using System.Windows.Documents;

namespace PracticeWebApp1.Controllers
{
    public class EventsController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();

        [Authorize]
        public ActionResult Index(string Genre, string searchString, string DateStart, string DateFinish)
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] is UsersData)
            {
                var GenreLst = new List<string>();

                var GenreQry = from d in db.EventTypes
                               select d.EventType1;

                GenreLst.AddRange(GenreQry.Distinct());
                ViewBag.Genre = new SelectList(GenreLst);

                var events = from m in db.Events
                             select m;

                if ((DateStart != null && DateStart != "") || (DateFinish != null && DateFinish != ""))
                {
                    
                    if((DateStart != null && DateStart != "") && (DateFinish != null && DateFinish != ""))
                    {
                        DateTime DateStart2 = Convert.ToDateTime(DateStart);
                        DateTime DateFinish2 = Convert.ToDateTime(DateFinish);
                        events = (
        from r in db.Events
        from v in db.Stages
        .Where(a => a.EventId == r.idEvents
                &&
                (
                    DateStart2 <= a.DateStart
                    && DateFinish2 >= a.DateFinish
                )
        )
        .GroupBy(m => m.EventId)
        select r
);
                    }
                    else
                        if(DateStart != null && DateStart != "")
                    {
                        DateTime DateStart2 = Convert.ToDateTime(DateStart);
                        events = (
                            from r in db.Events
                            from v in db.Stages
                            .Where(a => a.EventId == r.idEvents
                            &&
                                (
                                    DateStart2 <= a.DateStart
                                )
                            )
                        .GroupBy(m => m.EventId)
                        select r
                        );
                    }
                    else
                    if (DateFinish != null && DateFinish != "")
                    {
                        DateTime DateFinish2 = Convert.ToDateTime(DateFinish);
                        events = (
from r in db.Events
from v in db.Stages
.Where(a => a.EventId == r.idEvents
&&
(
DateFinish2 >= a.DateFinish
)
)
.GroupBy(m => m.EventId)
select r
);
                    }

                }

                if (!String.IsNullOrEmpty(searchString))
                {
                    events = events.Where(s => s.EventName.Contains(searchString));
                    Session["lastName"] = searchString;
                }

                if (!string.IsNullOrEmpty(Genre) && Genre != "All")
                {
                    events = events.Where(x => x.EventType.EventType1 == Genre);
                    Session["lastGenre"] = Genre;
                }

                /*if (DateStart != null)
                {
                    stages = stages.Where(x => x.DateStart >= DateStart);
                    events = events.Where(x => x.idEvents == stages.EventId)
                    Session["DateStart"] = Genre;
                }*/

                return View(events);
            }
            else
                return Redirect("/Login");
        }

        // GET: Events/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Ageid = new SelectList(db.Ages, "idAge", "Age1");
            ViewBag.Formid = new SelectList(db.EventForms, "idForm", "EventForm1");
            ViewBag.Typeid = new SelectList(db.EventTypes, "idType", "EventType1");
            return View();
        }

        // POST: Events/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEvents,EventName,Typeid,Ageid,Formid,EventLink,EventDesc")] Event @event)
        {
            if (ModelState.IsValid)
            {
                if (System.Web.HttpContext.Current.Session["CurrentUser"] is PracticeWebApp1.Models.UsersData)
                {
                    if ((System.Web.HttpContext.Current.Session["CurrentUser"] as PracticeWebApp1.Models.UsersData).Mod == true)
                    {
                        db.Events.Add(@event);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                        return HttpNotFound();
                }
                else
                    return HttpNotFound();
            }
            ViewBag.Ageid = new SelectList(db.Ages, "idAge", "Age1", @event.Ageid);
            ViewBag.Formid = new SelectList(db.EventForms, "idForm", "EventForm1", @event.Formid);
            ViewBag.Typeid = new SelectList(db.EventTypes, "idType", "EventType1", @event.Typeid);
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ageid = new SelectList(db.Ages, "idAge", "Age1", @event.Ageid);
            ViewBag.Formid = new SelectList(db.EventForms, "idForm", "EventForm1", @event.Formid);
            ViewBag.Typeid = new SelectList(db.EventTypes, "idType", "EventType1", @event.Typeid);
            return View(@event);
        }

        // POST: Events/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEvents,EventName,Typeid,Ageid,Formid,EventLink,EventDesc")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ageid = new SelectList(db.Ages, "idAge", "Age1", @event.Ageid);
            ViewBag.Formid = new SelectList(db.EventForms, "idForm", "EventForm1", @event.Formid);
            ViewBag.Typeid = new SelectList(db.EventTypes, "idType", "EventType1", @event.Typeid);
            return View(@event);
        }

        [Authorize]
        public ActionResult Members(int? id)
        {
            var participationLists = db.ParticipationLists.Include(p => p.Member).Include(p => p.Stage);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            participationLists = participationLists.Where(x => x.StageId == id);
            if (participationLists == null)
            {
                return HttpNotFound();
            }
            return View(participationLists);
        }

        [Authorize]
        public ActionResult Managers(int? id)
        {
            var managersLists = db.ManagersLists.Include(m => m.Manager).Include(m => m.Stage);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            managersLists = managersLists.Where(x => x.StageId == id);
            if (managersLists == null)
            {
                return HttpNotFound();
            }
            return View(managersLists);
        }

        [Authorize]
        public ActionResult Stages(int? id)
        {
            var stages = db.Stages.Include(s => s.Adress).Include(s => s.Event);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stages = stages.Where(x => x.EventId == id);
            if (stages == null)
            {
                return HttpNotFound();
            }
            return View(stages);
        }

        // GET: Events/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] is PracticeWebApp1.Models.UsersData)
            {
                if ((System.Web.HttpContext.Current.Session["CurrentUser"] as PracticeWebApp1.Models.UsersData).Mod == true)
                {
                    Event @event = db.Events.Find(id);
                    db.Events.Remove(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return HttpNotFound();
            }
            else return HttpNotFound();
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
            EventsTestEntities1 dc = new EventsTestEntities1();

            var events = from m in db.Events
                         select m;

            if (Session["lastName"] != null)
            {
                string lastName = Session["lastName"].ToString();
                if (!String.IsNullOrEmpty(lastName))
                {
                    events = events.Where(s => s.EventName.Contains(lastName));
                }
            }

            if (Session["lastGenre"] != null)
            {
                string lastGenre = Session["lastGenre"].ToString();
                if (!string.IsNullOrEmpty(lastGenre) && lastGenre != "All")
                {
                    events = events.Where(x => x.EventType.EventType1 == lastGenre);
                }
            }

            WebGrid grid = new WebGrid(source: events, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
                columns: grid.Columns(
                    grid.Column("EventName", "Название мероприятия"),
                    grid.Column("Age.idAge", "Возрастное ограничение", format: (item) => item.Age.Age1, canSort: true),
                    grid.Column("EventForm.idForm", "Форма проведения", format: (item) => item.EventForm.EventForm1),
                    grid.Column("EventType.idType", "Тип мероприятия", format: (item) => item.EventType.EventType1),
                    grid.Column("EventLink", "Ссылка на мероприятие"),
                    grid.Column("EventDesc", "Описание мероприятия")
                        )
                    ).ToString();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=ExportExcel.xls");
            Response.ContentType = "application/excel";
            Response.Write(gridData);
            Response.End();
        }
    }
}
