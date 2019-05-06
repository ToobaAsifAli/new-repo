using EmployeeLoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class LoanApplicationController : Controller
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-R6RA1PL\\TOOBAASIF;Initial Catalog=DB7;User ID=sa;Password=1212;MultipleActiveResultSets=True;Application Name=EntityFramework");

        // GET: LoanApplication
        public ActionResult Index()
        {
            return View();
        }

        // GET: LoanApplication/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        private int emp_id(string email1)
        {
            string query;
            query = "SELECT Id FROM Employee WHERE  Email ='" + email1 + "' ";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse(reader[0].ToString());
            }
            return value;
        }
        private int cat_id(string email1)
        {
            string query;
            query = "SELECT Id FROM LoanCategory WHERE  Category ='" + email1 + "' ";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse(reader[0].ToString());
            }
            return value;
        }
        // GET: LoanApplication/Create
        [HttpGet]
        public ActionResult Create()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
          //  if(provident)
           // int id1 = emp_id(email);
            //String qry = "insert into LoanApply  values('" + id1 + "','" + LastName + "','" + Contact + "','" + Email + "','" + dt + "','" + gen + "' ) ";
           // SqlCommand sc = new SqlCommand(qry, conn);
           // int i = sc.ExecuteNonQuery();
            return View();
        }
        //file download
        public FileResult Download()
        {
            string path = Server.MapPath("~/assets/files");
            string filename = Path.GetFileName("agreement.pdf");
            string fullpath = Path.Combine(path, filename);
            return File(fullpath, "pdf", "employee_agreement.pdf");

            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        // POST: LoanApplication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Create(FormCollection collection,byte[]image1)
        {
          //  try
            //{
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            int loanreq = int.Parse(collection["loanreq"]);
            int instalacq = int.Parse(collection["instalacq"]);
            DateTime enddate = DateTime.Parse(collection["enddate"]);
            DateTime startdate = DateTime.Parse(collection["startdate"]);
           int  instal = (instalacq);
                decimal loan1 = loanreq;
            string email = collection["email"].ToString();
            decimal instalmoney = decimal.Parse(collection["instalmoney"]);
                int catid=0;
                // TODO: Add insert logic here
                string email1 = email;
                int emailid = emp_id(email1);
              //  string loan_mon = ;
              //  int  = emp_id(email1);
                string loancategory = collection["Category"].ToString();
                if(loancategory == "AgainstProvidentFund")
                {
                    string l = "Against Provident Fund";
                    catid = cat_id(l);
                }
                else if(loancategory == "General")
                {
                    string l = "General Loan";
                    catid = cat_id(l);
                }
                DateTime req = DateTime.Now;
                string choose = "N";
                string choose1 = "Y";
                string loanpur = collection["Purpose"].ToString();
                if (loancategory == "Automobile")
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate+ "','" + req + "','" + image1 + "','" + choose + "','" + choose1 + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                }
                else if(loancategory == "Property")
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose1 + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                }
                else
                {
                    String qry = "insert into LoanApply  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + image1 + "','" + choose + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    if (i >= 1)
                    { ViewBag(); }
                    else
                    {
                         }
                }
                    return RedirectToAction("Index");
            //}
           /* catch
            {
                return View();
            }*/
        }

        // GET: LoanApplication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoanApplication/Edit/5
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

        // GET: LoanApplication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoanApplication/Delete/5
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
