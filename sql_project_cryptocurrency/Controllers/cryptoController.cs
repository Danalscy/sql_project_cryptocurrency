using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sql_project_cryptocurrency.Models;

namespace sql_project_cryptocurrency.Views
{
    public class cryptoController : Controller
    {
        private CryptoEntities db = new CryptoEntities();

        // GET: crypto
        public ActionResult Index()
        {
            return View(db.cryptoes.ToList());
        }

        // GET: crypto/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crypto crypto = db.cryptoes.Find(id);
            if (crypto == null)
            {
                return HttpNotFound();
            }
            return View(crypto);
        }

        // GET: crypto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: crypto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,source,timestamp,price,volume_24h,change_7d")] crypto crypto)
        {
            if (ModelState.IsValid)
            {
                db.cryptoes.Add(crypto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crypto);
        }

        // GET: crypto/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crypto crypto = db.cryptoes.Find(id);
            if (crypto == null)
            {
                return HttpNotFound();
            }
            return View(crypto);
        }

        // POST: crypto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,source,timestamp,price,volume_24h,change_7d")] crypto crypto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crypto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crypto);
        }

        // GET: crypto/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crypto crypto = db.cryptoes.Find(id);
            if (crypto == null)
            {
                return HttpNotFound();
            }
            return View(crypto);
        }

        // POST: crypto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            crypto crypto = db.cryptoes.Find(id);
            db.cryptoes.Remove(crypto);
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
