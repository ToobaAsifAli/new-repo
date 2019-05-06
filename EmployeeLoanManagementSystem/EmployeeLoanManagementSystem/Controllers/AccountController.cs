using EmployeeLoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        DB7Entities4 db = new DB7Entities4();
        //public ActionResult Login()
        //{
        //    return View();
        //}





        //[HttpPost]
        //public ActionResult Login(Models.Person emp)
        //{
        //    using (var context = new DB7Entities4())
        //    {
        //        bool isValid = context.Employees.Any(x => x.Email == emp.Email && x.Password == emp.Password);
        //        if (isValid)
        //        {
        //            FormsAuthentication.SetAuthCookie(emp.Email, false);
        //            return RedirectToAction("welcome", "loan\\welcome");
        //        }
        //        ModelState.AddModelError("", "Invalid Email or Password");
        //        return View();
        //    }

        //}


        public ActionResult Login2()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login2(Models.Person emp)
        {
            using (var context = new DB7Entities4())
            {
                bool isValid = context.Employees.Any(x => x.Email == emp.Email && x.Password == emp.Password);
                bool isValid1 = context.Admins.Any(x => x.Email == emp.Email && x.Password == emp.Password);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(emp.Email, false);
                    return RedirectToAction("welcome", "loan\\welcome");
                }
                else if (isValid1)
                {
                    FormsAuthentication.SetAuthCookie(emp.Email, false);
                    return RedirectToAction("welcome", "HR\\welcome");
                }

                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }

        }



        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Models.Person emp)
        {
            using (var context = new DB7Entities4())
            {
                bool isValid = context.Employees.Any(x => x.Email == emp.Email && x.Password == emp.Password);
                bool isValid1 = context.Admins.Any(x => x.Email == emp.Email && x.Password == emp.Password);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(emp.Email, false);
                    return RedirectToAction("welcome", "loan\\welcome");
                }
                if (isValid1)
                {
                    FormsAuthentication.SetAuthCookie(emp.Email, false);
                    return RedirectToAction("welcome", "HR\\welcome");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password");
                }
                return View();

            }
        }


        //public ActionResult SignUp()
        //{
        //    return View();
        //}


        //[HttpPost]
        //public ActionResult SignUp(Employee emp)
        //{
        //    db.Employees.Add(emp);
        //    db.SaveChanges();
        //    return RedirectToAction("Login");
        //}




        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Employee emp)
        {
            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public ActionResult Index()
        {

            var emp = db.Employees;
            return View(emp.ToList());
        }



        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee emp = db.Employees.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // GET: Admin/Create
        // GET: Employees/Create
        public ActionResult Create()
        {

            return View();
        }

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


            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Designation,Salary,HireDate,Address,City,ContactNo,AccountNo,Country,BirthDate,Gender,ProvidentFundOpted,ProvidentFundAmount,ProvidentFundPercentage,DepartmentId,ProvidentFundOptedDate,CNIC,Password,IsEmailSent")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Delete/5
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
    }
}
