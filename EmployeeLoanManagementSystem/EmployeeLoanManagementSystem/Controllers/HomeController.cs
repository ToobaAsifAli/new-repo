using EmployeeLoanManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLoanManagementSystem.Controllers
{
    public class HomeController : Controller
    {

        //SqlConnection con = new SqlConnection("Data Source=DESKTOP-R6RA1PL\\TOOBAASIF;Initial Catalog=ProjectA;Persist Security Info=True;User ID=sa;Password=1212");

        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-R6RA1PL\\TOOBAASIF;Initial Catalog=DB7;User ID=sa;Password=1212;MultipleActiveResultSets=True;Application Name=EntityFramework");
        private DB7Entities4 db = new DB7Entities4();

        public ActionResult Index()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string choice = "Y";
           // string choice1 = "N";
            DateTime dt = DateTime.Now;
            string query = "update Installment set IsPaid='"+choice+ "' where InstallmentDate='"+dt+"' ";
            SqlCommand sc = new SqlCommand(query, conn);
            int i = sc.ExecuteNonQuery();
            string query1 = "update LoanDocumentVerify set IsDefaulter='"+choice+"' select DATEDIFF(MONTH,RespondDate,SubmissionDate)  From  LoanDocumentVerify JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanDocumentVerify.LoanId  Group by DATEDIFF(MONTH,RespondDate,SubmissionDate) Having DATEDIFF(MONTH,RespondDate,SubmissionDate)>1";
            SqlCommand sc1 = new SqlCommand(query1, conn);
            int j = sc.ExecuteNonQuery();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}