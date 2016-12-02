using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shared.Entity;
using Shared.Models;

namespace WebApplication1.Controllers
{
    public class StockPricesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StockPrices
        public ActionResult Index()
        {
            return View(db.StockPrices.ToList());
        }

        // GET: StockPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecondPrice stockPrice = db.StockPrices.Find(id);
            if (stockPrice == null)
            {
                return HttpNotFound();
            }
            return View(stockPrice);
        }

        // GET: StockPrices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Open,Last,Current,Max,Min,Buy,Sell,Turnover,Amount,Buy1,BuyPrice1,Buy2,BuyPrice2,Buy3,BuyPrice3,Buy4,BuyPrice4,Buy5,BuyPrice5,Sell1,SellPrice1,Sell2,SellPrice2,Sell3,SellPrice3,Sell4,SellPrice4,Sell5,SellPrice5,Date,Time,MillSecond,Remark,CreatedOn,UpdatedOn")] SecondPrice stockPrice)
        {
            if (ModelState.IsValid)
            {
                db.StockPrices.Add(stockPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockPrice);
        }

        // GET: StockPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecondPrice stockPrice = db.StockPrices.Find(id);
            if (stockPrice == null)
            {
                return HttpNotFound();
            }
            return View(stockPrice);
        }

        // POST: StockPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Open,Last,Current,Max,Min,Buy,Sell,Turnover,Amount,Buy1,BuyPrice1,Buy2,BuyPrice2,Buy3,BuyPrice3,Buy4,BuyPrice4,Buy5,BuyPrice5,Sell1,SellPrice1,Sell2,SellPrice2,Sell3,SellPrice3,Sell4,SellPrice4,Sell5,SellPrice5,Date,Time,MillSecond,Remark,CreatedOn,UpdatedOn")] SecondPrice stockPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockPrice);
        }

        // GET: StockPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecondPrice stockPrice = db.StockPrices.Find(id);
            if (stockPrice == null)
            {
                return HttpNotFound();
            }
            return View(stockPrice);
        }

        // POST: StockPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SecondPrice stockPrice = db.StockPrices.Find(id);
            db.StockPrices.Remove(stockPrice);
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
