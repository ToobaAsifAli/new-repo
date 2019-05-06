using EmployeeLoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLoanManagementSystem.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private DB7Entities4 db = new DB7Entities4();
        // GET: Admin
        public ActionResult Index()
        {
            var stds = db.Employees;
            return View(stds.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult list()
        {
            //List<Employee> list = new List<Employee>();
            //var emp = db.Employees;

            //return View(list);
            var stds = db.Employees;
            return View(stds.ToList());
        }

        // GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,Designation,Salary,HireDate,Address,City,ContactNo,AccountNo,Country,BirthDate,Gender,ProvidentFundOpted,ProvidentFundAmount,ProvidentFundPercentage,DepartmentId,ProvidentFundOptedDate,CNIC,Password,IsEmailSent")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", employee.DepartmentId);
            return View(employee);









        }

        //public ActionResult RemoveEmployee(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee emp = db.Employees.Find(id);
        //    if (emp == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(emp);

        //}

        //[HttpPost, ActionName("RemoveEmployee")]
        //[ValidateAntiForgeryToken]
        //public ActionResult EmployeeRemoved(int id)
        //{
        //    Employee emp = db.Employees.Find(id);
        //    db.Employees.Remove(emp);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
