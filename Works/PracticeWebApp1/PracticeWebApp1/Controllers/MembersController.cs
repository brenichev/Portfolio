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
    public class MembersController : Controller
    {
        private EventsTestEntities1 db = new EventsTestEntities1();

        // GET: Members
        [Authorize]
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.MemberType);
            return View(members.ToList());
        }

        // GET: Members/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "idMemberType", "MemberType1");
            return View();
        }

        // POST: Members/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idMember,MemberSurname,MemberName,MemberOtch,MemberTypeId,MemberDesc,MemberLink")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "idMemberType", "MemberType1", member.MemberTypeId);
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "idMemberType", "MemberType1", member.MemberTypeId);
            return View(member);
        }

        // POST: Members/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idMember,MemberSurname,MemberName,MemberOtch,MemberTypeId,MemberDesc,MemberLink")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberTypeId = new SelectList(db.MemberTypes, "idMemberType", "MemberType1", member.MemberTypeId);
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
