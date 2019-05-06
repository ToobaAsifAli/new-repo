using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmployeeLoanManagementSystem.Models;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class LoanAppliesController : Controller
    {
        private DB7Entities4 db = new DB7Entities4();

        // GET: LoanApplies
        public ActionResult Index()
        {
            var loanApplies = db.LoanApplies.Include(l => l.Employee).Include(l => l.LoanCategory1).Include(l => l.LoanDocumentVerify).Include(l => l.LoanRequestStatu);
            return View(loanApplies.ToList());
        }

        // GET: LoanApplies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApply loanApply = db.LoanApplies.Find(id);
            if (loanApply == null)
            {
                return HttpNotFound();
            }
            return View(loanApply);
        }
        //file download
        public FileResult Download()
        {
            string path = Server.MapPath("~/assets/files");
            string filename =   Path.GetFileName("agreement.pdf");
            string fullpath = Path.Combine(path, filename);
            return File(fullpath, "pdf","employee_agreement.pdf");
            
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

            // GET: LoanApplies/Create
            public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            ViewBag.LoanCategory = new SelectList(db.LoanCategories, "Id", "Type");
            ViewBag.LoanApplyId = new SelectList(db.LoanDocumentVerifies, "LoanId", "IsDefaulter");
            ViewBag.LoanApplyId = new SelectList(db.LoanRequestStatus, "LoanId", "RequestStatus");
            return View();
        }
        // GET: LoanApplies/Create
        public ActionResult Create(LoanApply b1,HttpPostedFileBase image1)
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName");
            ViewBag.LoanCategory = new SelectList(db.LoanCategories, "Id", "Type");
            ViewBag.LoanApplyId = new SelectList(db.LoanDocumentVerifies, "LoanId", "IsDefaulter");
            ViewBag.LoanApplyId = new SelectList(db.LoanRequestStatus, "LoanId", "RequestStatus");
            return View();
        }
      
        // POST: LoanApplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanApplyId,EmployeeId,LoanCategory,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate,LoanAgreement,LoanForProperty,LoanForAutomobile")] LoanApply loanApply)
        {
            if (ModelState.IsValid)
            {
                db.LoanApplies.Add(loanApply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", loanApply.EmployeeId);
            ViewBag.LoanCategory = new SelectList(db.LoanCategories, "Id", "Type", loanApply.LoanCategory);
            ViewBag.LoanApplyId = new SelectList(db.LoanDocumentVerifies, "LoanId", "IsDefaulter", loanApply.LoanApplyId);
            ViewBag.LoanApplyId = new SelectList(db.LoanRequestStatus, "LoanId", "RequestStatus", loanApply.LoanApplyId);
            return View(loanApply);
        }

        // GET: LoanApplies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApply loanApply = db.LoanApplies.Find(id);
            if (loanApply == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", loanApply.EmployeeId);
            ViewBag.LoanCategory = new SelectList(db.LoanCategories, "Id", "Type", loanApply.LoanCategory);
            ViewBag.LoanApplyId = new SelectList(db.LoanDocumentVerifies, "LoanId", "IsDefaulter", loanApply.LoanApplyId);
            ViewBag.LoanApplyId = new SelectList(db.LoanRequestStatus, "LoanId", "RequestStatus", loanApply.LoanApplyId);
            return View(loanApply);
        }

        // POST: LoanApplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanApplyId,EmployeeId,LoanCategory,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate,LoanAgreement,LoanForProperty,LoanForAutomobile")] LoanApply loanApply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanApply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FirstName", loanApply.EmployeeId);
            ViewBag.LoanCategory = new SelectList(db.LoanCategories, "Id", "Type", loanApply.LoanCategory);
            ViewBag.LoanApplyId = new SelectList(db.LoanDocumentVerifies, "LoanId", "IsDefaulter", loanApply.LoanApplyId);
            ViewBag.LoanApplyId = new SelectList(db.LoanRequestStatus, "LoanId", "RequestStatus", loanApply.LoanApplyId);
            return View(loanApply);
        }

        // GET: LoanApplies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApply loanApply = db.LoanApplies.Find(id);
            if (loanApply == null)
            {
                return HttpNotFound();
            }
            return View(loanApply);
        }

        // POST: LoanApplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanApply loanApply = db.LoanApplies.Find(id);
            db.LoanApplies.Remove(loanApply);
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
