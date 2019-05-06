using EmployeeLoanManagementSystem.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net;


namespace EmployeeLoanManagementSystem.Controllers
{
    public class loanController : Controller
    {
        // public int ide;
        public static int docid;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-R6RA1PL\\TOOBAASIF;Initial Catalog=DB7;User ID=sa;Password=1212;MultipleActiveResultSets=True;Application Name=EntityFramework");
        private DB7Entities4 db = new DB7Entities4();
        // GET: loan
        public ActionResult Index()
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
        private int RetrieveID()
        {
            int value = 0;
            try
            {
                string query = " Select LoanApplyId  from LoanApply where (LoanApplyId = SCOPE_IDENTITY());";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                var val = cmd.ExecuteScalar().ToString();
                value = int.Parse(val);

            }
            catch (Exception ex)
            {
                throw;
            }


            return value;
        }
        private int cat_id(string email1)
        {
            string query;

            query = "SELECT Id FROM LoanCategory WHERE  Type ='" + email1 + "' ";


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
        private string loanprop(int l)
        {
            string query;

            query = "SELECT LoanForProperty FROM LoanApply WHERE  LoanApplyId ='" + l + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private int getsal(string email)
        {
            string query;

            query = "SELECT Salary FROM Employee WHERE  Email ='" + email + "' ";


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
        private int getpro(string email)
        {
            string query;

            query = "SELECT ProvidentFundAmount FROM Employee WHERE  Email ='" + email + "' ";


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
        private string loanauto(int l)
        {
            string query;

            query = "SELECT LoanForAutomobile FROM LoanApply WHERE  LoanApplyId ='" + l + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string stat(int l)
        {
            string query;

            query = "select Status from LoanDocumentVerify  where LoanId = '" + l + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = ((reader[0].ToString()));
            }
            return value;
        }
        private int instalno(int l)
        {
            string query;

           query = "select NoOfInstallments from LoanApply join LoanRequestStatus on LoanRequestStatus.LoanId = LoanApply.LoanApplyId where RequestStatus = Accepted AND LoanId = '" + l + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int value = 0;
            while (reader.Read())
            {
                value = int.Parse((reader[0].ToString()));
            }
            return value;
        }
        // GET: loan/Details/5
        public ActionResult Details(int ide)
        {
            return View();
        }
        public ActionResult welcome()
        {
            return View();
        }
        public ActionResult loanpolicies()
        {
            return View();
        }
        [HttpGet]
        public ActionResult documentsverification(int ide)
        {
            //LoanRequestStatu l = db.LoanRequestStatus.Find(ide);
            docid = ide;
           
            return View();
        }
      
        public ActionResult installments(int ide)

        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string s = "select InstallmentId,LoanId,InstallmentsNo,InstallmentDate,[Installment Money],IsPaid from Installment join LoanApply on LoanApply.LoanApplyId = Installment.LoanId   where LoanId = '" + ide+"' ";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(s, conn);
            da.Fill(dt);

            return View(dt);
        }
        public ActionResult loanstat()
        {
            return View();
        }
      
        public ActionResult instructions()
        {
            return View();
        }
        public ActionResult provident()
        {
            string s = "Against Provident Fund";
            int cat = cat_id(s);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "SELECT LoanApplyId,LoanMoney,RequestDate,LoanForProperty,LoanForAutomobile from LoanApply JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanApply.LoanApplyId JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanCategory='" + cat + "' AND Employee.Email ='" + User.Identity.Name.ToString() + "'";
            //va
            string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason,LoanForProperty,LoanForAutomobile from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory  JOIN LoanRequestStatus  on LoanRequestStatus.LoanId = LoanApply.LoanApplyId   where LoanCategory='" + cat + "' ";

            SqlDataAdapter da = new SqlDataAdapter(query1,conn);
            da.Fill(dt);

           // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        public ActionResult detailsprov(int ide)
        {
           
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "SELECT LoanApplyId,LoanMoney,RequestDate,LoanForProperty,LoanForAutomobile from LoanApply JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanApply.LoanApplyId JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanCategory='" + cat + "' AND Employee.Email ='" + User.Identity.GetUserName().ToString() + "'";
            //va
            string query1 = "SELECT LoanApplyId,EmployeeId,Employee.FirstName + ' ' +Employee.LastName,Employee.Email,Employee.Salary,LoanMoney,RequestDate,RequestStatus,Reason,LoanForProperty,LoanForAutomobile,NoOfInstallments,[Installment Money],InstallmentStartDate,InstallmentEndDate,LoanAgreement from LoanApply JOIN Employee ON Employee.Id = LoanApply.EmployeeId   JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory  JOIN LoanRequestStatus  on LoanRequestStatus.LoanId = LoanApply.LoanApplyId   where LoanApplyId = '" + ide+"' ";

            SqlDataAdapter da = new SqlDataAdapter(query1, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        //installments
        public ActionResult instal(int ide)
        {
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string a = "Rejected";
            string q;
            q = "select InstallmentsNo,[Installment Money],InstallmentDate,IsPaid from Installment join LoanApply on LoanApply.LoanApplyId = Installment.LoanId where Installment.LoanId ='" + ide + "' ";

            SqlDataAdapter da = new SqlDataAdapter(q, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        //delete documents
        public ActionResult Deletedoc(int ide)
        {
            string status = stat(ide);
            if (status != "Accepted")
            {
                string query = "update LoanDocumentVerify set SubmissionDate='" + null + "',PropertyDocument='" + null + "',AutomobileDocument='" + null + "' where LoanId = '" + ide + "'";
                SqlCommand sc = new SqlCommand(query, conn);
                int i = sc.ExecuteNonQuery();
            }
            return RedirectToAction("viewimag",new {@ide =  ide });
        }
        public ActionResult viewimag(int ide)
        {
            //string s = "Against Provident Fund";
            //int cat = cat_id(s);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //string query = "SELECT LoanApplyId,LoanMoney,RequestDate,LoanForProperty,LoanForAutomobile from LoanApply JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanApply.LoanApplyId JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanCategory='" + cat + "' AND Employee.Email ='" + User.Identity.GetUserName().ToString() + "'";
            //va
            //string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,LoanForProperty,LoanForAutomobile from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory   where LoanCategory='" + cat + "' ";
            string query1 = "SELECT LoanId,PropertyDocument,AutomobileDocument,SubmissionDate from LoanDocumentVerify join LoanApply on LoanDocumentVerify.LoanId =LoanApply.LoanApplyId where LoanApplyId = '"+ide+"'";
            SqlDataAdapter da = new SqlDataAdapter(query1, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
       
       
            public ActionResult general()
        {
            string s = "General Loan";
            int cat = cat_id(s);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string e = User.Identity.Name;
            string query = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason from LoanApply JOIN LoanRequestStatus ON LoanRequestStatus.LoanId = LoanApply.LoanApplyId JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanCategory='" + cat + "' AND Employee.Email ='" +e + "'";
            //va
            string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory   where LoanCategory='" + cat + "' ";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);
        }
        private string nam1(string email)
        {
            string query;

            query = "SELECT FirstName FROM Employee WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string nam2(string email)
        {
            string query;

            query = "SELECT LastName FROM Employee WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string add(string email)
        {
            string query;

            query = "SELECT Address FROM Employee WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader["Address"].ToString()) ;
            }
            return value;
        }
        private string con(string email)
        {
            string query;

            query = "SELECT ContactNo FROM Employee WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string hire(string email)
        {
            string query;

            query = "SELECT HireDate FROM Employee WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string des(string email)
        {
            string query;

            query = "SELECT Designation FROM Employee WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string dep(string email)
        {
            string query;

            query = "SELECT Department.Name FROM Employee JOIN Department on Department.Id = Employee.DepartmentId WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
            }
            return value;
        }
        private string acc(string email)
        {
            string query;

            query = "SELECT AccountNo FROM Employee  WHERE  Email ='" + email + "' ";


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = (reader[0].ToString());
               
            }
            return value;
        }
        [Authorize]
            // GET: loan/Create
            public ActionResult Create(string email)
        {
            int salary = getsal(email);
            if(salary*10 < 6000000)
            {
                ViewBag.sal = salary * 10;
            }
            else
            {
                ViewBag.sal = 6000000;
            }
            int pro = getpro(email);
            ViewBag.prov = pro;
            
         //   string n = nam1[email];
            ViewBag.nam = nam1(email);
            ViewBag.nam1 = nam2(email);
            ViewBag.em = email;
            ViewBag.add = add(email);
            ViewBag.con = con(email);
            ViewBag.hire = hire(email);
            ViewBag.des = des(email);
            ViewBag.dep = dep(email);
            ViewBag.acc = acc(email);
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
        public void docupload(HttpPostedFileBase file, int ide)
        {
            if (file != null && file.ContentLength != 0)
            {
                string imgname = Path.GetFileName(file.FileName);
                string imgext = Path.GetExtension(imgname);
             
                string imgname1 = Path.GetFileNameWithoutExtension(file.FileName);
                string myUniqueFileName = $@"{Guid.NewGuid()}";
                string NewPath = imgname.Replace(imgname1, myUniqueFileName);
                if (imgext == ".jpg" || imgext == ".png")
                {
                    string imgpath = Path.Combine(Server.MapPath("~/assets/agreementdoc"), NewPath);
                    // string imgpath = Server.MapPath(" ") + "\\" + NewPath;
                    // string imgpath = "~/assets/agreementdoc/" + NewPath;
                    string img = "/assets/agreementdoc/" + NewPath;
                    file.SaveAs(imgpath);
                    String qry1 = "update LoanApply set LoanAgreement = '" + img + "' where LoanApplyId = '" + ide + "'";

                    SqlCommand sc = new SqlCommand(qry1, conn);
                    int i = sc.ExecuteNonQuery();
                }


            }

        }
        public void docuploadprop(HttpPostedFileBase file, int ide)
        {
            if (file != null && file.ContentLength != 0)
            {
                string imgname = Path.GetFileName(file.FileName);
                string imgext = Path.GetExtension(imgname);

                string imgname1 = Path.GetFileNameWithoutExtension(file.FileName);
                string myUniqueFileName = $@"{Guid.NewGuid()}";
                string NewPath = imgname.Replace(imgname1, myUniqueFileName);
                if (imgext == ".jpg" || imgext == ".png")
                {
                    string imgpath = Path.Combine(Server.MapPath("~/assets/propertydoc"), NewPath);
                    // string imgpath = Server.MapPath(" ") + "\\" + NewPath;
                    // string imgpath = "~/assets/agreementdoc/" + NewPath;
                    string img = "/assets/propertydoc/" + NewPath;
                    file.SaveAs(imgpath);
                    String qry1 = "update LoanDocumentVerify set PropertyDocument = '" + img + "' where LoanId = '" + ide + "'";

                    SqlCommand sc = new SqlCommand(qry1, conn);
                    int i = sc.ExecuteNonQuery();
                }


            }

        }
        public void docuploadauto(HttpPostedFileBase file, int ide)
        {
            if (file != null && file.ContentLength != 0)
            {
                string imgname = Path.GetFileName(file.FileName);
                string imgext = Path.GetExtension(imgname);

                string imgname1 = Path.GetFileNameWithoutExtension(file.FileName);
                string myUniqueFileName = $@"{Guid.NewGuid()}";
                string NewPath = imgname.Replace(imgname1, myUniqueFileName);
                if (imgext == ".jpg" || imgext == ".png")
                {
                    string imgpath = Path.Combine(Server.MapPath("~/assets/automobiledoc"), NewPath);
                    // string imgpath = Server.MapPath(" ") + "\\" + NewPath;
                    // string imgpath = "~/assets/agreementdoc/" + NewPath;
                    string img = "/assets/automobiledoc/" + NewPath;
                    file.SaveAs(imgpath);
                    String qry1 = "update LoanDocumentVerify set AutomobileDocument = '" + img + "' where LoanId = '" + ide + "'";

                    SqlCommand sc = new SqlCommand(qry1, conn);
                    int i = sc.ExecuteNonQuery();
                }


            }

        }
        // POST: loan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase image1)
        {
           
          //  try
          // {
           // TODO: Add insert logic here
            if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
           
                int catid = 0;
                string loancategory = collection["Category"].ToString();
           
            if (loancategory == "AgainstProvidentFund")
                {
                    string l = "Against Provident Fund";
                    catid = cat_id(l);
                }
                else if (loancategory == "General")
                {
                    string l = "General Loan";
                    catid = cat_id(l);
                }
               // string image1 = (collection["instalacq"].ToString());
                int loanreq = int.Parse(collection["loanreq"]);
                int instalacq = int.Parse(collection["in"]);
            int instalacq1 = int.Parse(collection["instalacq"]);
            decimal instalacq2 = instalacq1;
            DateTime enddate = DateTime.Now;
                DateTime startdate = DateTime.Now;
                int instal = (instalacq);
                decimal loan1 = loanreq;
                string email = collection["email"].ToString();
                decimal instalmoney = decimal.Parse(collection["instalmoney"]);
            string ack = collection["ack"].ToString();
                // TODO: Add insert logic here
                string email1 = email;
                int emailid = emp_id(email1);
            decimal maxi = 6000000;
            if(loan1 > maxi)
            {
                ViewBag.Error = TempData["Maximun loan can be 6000000"];
                return View();
            }
            if (instalmoney > instalacq2)
            {
                ViewBag.Error = TempData["Installment money is exceeding maximum imstallment"];
                return View();
            }
            if(ack=="No")
            {
                ViewBag.Error = TempData["You need to agree to our policies"];
                return View();
            }
            //  string loan_mon = ;
            //  int  = emp_id(email1);
            string loanpur = null;
                if ((loancategory == "General"))
                { loanpur = null; }
                else
                {
                    if (collection["Purpose"].ToString() == null)
                    {
                        loanpur = null;
                    }
                    else
                    {
                        loanpur = collection["Purpose"].ToString();

                    }
                }
                DateTime req = DateTime.Now;
                string choose = "N";
                string choose1 = "Y";

                if (loanpur == "Automobile")
                {
                    String qry = "insert into LoanApply(EmployeeId,LoanCategory,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate,LoanForProperty,LoanForAutomobile,[Installment Money])  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + choose + "','" + choose1 + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    int j = RetrieveID();
                    string req1 = "Pending";
                    string reason1 = null;
                    // DateTime res = ;
                    string qry1 = "insert into LoanRequestStatus(LoanId,RequestStatus,Reason) values('" + j + "','" + req1 + "', '" + reason1 + "')    ";
                    SqlCommand sc1 = new SqlCommand(qry1, conn);
                    int q = sc1.ExecuteNonQuery();
                    string defaulter = "N";
                    string qry2 = "insert into LoanDocumentVerify(LoanId,IsDefaulter,Status)VALUES('" + j + "','" + defaulter + "','" + req1 + "')";
                    SqlCommand sc2 = new SqlCommand(qry2, conn);
                    int w = sc2.ExecuteNonQuery();
                docupload(image1, j);
                }
                else if (loanpur == "Property")
                {
                    String qry = "insert into LoanApply(EmployeeId,LoanCategory,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate,LoanForProperty,LoanForAutomobile,[Installment Money])   values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + choose1 + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    int j = RetrieveID();
                    string req1 = "Pending";
                    string reason1 = null;
                    // DateTime res = ;
                    string qry1 = "insert into LoanRequestStatus(LoanId,RequestStatus,Reason) values('" + j + "','" + req1 + "', '" + reason1 + "' )   ";
                    SqlCommand sc1 = new SqlCommand(qry1, conn);
                    int q = sc1.ExecuteNonQuery();
                    string defaulter = "N";
                    string qry2 = "insert into LoanDocumentVerify(LoanId,IsDefaulter,Status)VALUES('" + j + "','" + defaulter + "','" + req1 + "')";
                    SqlCommand sc2 = new SqlCommand(qry2, conn);
                    int w = sc2.ExecuteNonQuery();
                docupload(image1, j);
            }
                else
                {
                    String qry = "insert into LoanApply (EmployeeId,LoanCategory,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,RequestDate,LoanForProperty,LoanForAutomobile,[Installment Money])  values('" + emailid + "','" + catid + "','" + loan1 + "','" + instal + "','" + startdate + "','" + enddate + "','" + req + "','" + choose + "','" + choose + "','" + instalmoney + "' ) ";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        int j = RetrieveID();
                        string req1 = "Pending";
                        string reason1 = null;
                        // DateTime res = ;
                        string qry1 = "insert into LoanRequestStatus(LoanId,RequestStatus,Reason) values('" + j + "','" + req1 + "', '" + reason1 + "')    ";
                        SqlCommand sc1 = new SqlCommand(qry1, conn);
                        int q = sc1.ExecuteNonQuery();
                    docupload(image1, j);
                    // ViewBag.Message = ("You have successfully applied for loan.");
                    TempData["Success"] = "Added Successfully!";
                        return RedirectToAction("welcome", "loan");
                    }
                    else
                    {
                        TempData["Success"] = "Not Added Successfully!";

                    }
                    }
                if (loancategory == "AgainstProvidentFund")
                {
                    return RedirectToAction("provident", "loan");
                }
                if (loancategory == "General")
                {
                    return RedirectToAction("general", "loan");
                }
               return View();

          /*  }

           catch
            {
                return View();
            }*/
        }

        // GET: loan/Edit/5
        public ActionResult Edit(int? ide)
        {
           // int ide = int.Parse(collection["loanid"]);
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string q = "select LoanAgreement from LoanApply where LoanApplyId = '" + ide + "'";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string value = "0";
            while (reader.Read())
            {
                value = ((reader[0].ToString()));
            }
            LoanApply la = new LoanApply()
            {
                LoanAgreement = value
            };
            ViewBag.message = la;
            string query = "SELECT LoanCategory,LoanForProperty,LoanForAutomobile,LoanMoney,NoOfInstallments,InstallmentStartDate,InstallmentEndDate,[Installment Money],LoanAgreement From LoanApply JOIN Employee on Employee.Id = LoanApply.EmployeeId  where LoanApplyId='" + ide + "' ";
            //va
           // string query1 = "SELECT LoanApplyId,LoanMoney,RequestDate,RequestStatus,Reason from LoanApply  JOIN LoanCategory on LoanCategory.Id=LoanApply.LoanCategory   where LoanCategory='" + cat + "' ";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);

            // r data = db.LoanApplies.SqlQuery(query).ToList();
            return View(dt);

           // return View();
            
        }

        // POST: loan/Edit/5
        [HttpPost]
        public ActionResult Edit(int ide, FormCollection collection,HttpPostedFileBase image1)
        {
          /*  try
            {
                // TODO: Add insert logic here*/
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
               // byte[] image1 = Encoding.ASCII.GetBytes(collection["image1"].ToString());
                int loanreq = int.Parse(collection["loanreq"]);
                int instalacq = int.Parse(collection["instalacq"]);
            string option = collection["newag"];
                DateTime enddate = DateTime.Now;
                DateTime startdate = DateTime.Now;
                int instal = (instalacq);
                decimal loan1 = loanreq;
               // string email = collection["email"].ToString();
                decimal instalmoney = instalacq;
                int catid = 0;
                // TODO: Add insert logic here
             //   string email1 = email;
              //  int emailid = emp_id(email1);
                //  string loan_mon = ;
                //  int  = emp_id(email1);
                string loancategory = collection["Category"].ToString();
                if (loancategory == "AgainstProvidentFund")
                {
                    string l = "Against Provident Fund";
                    catid = cat_id(l);
                }
                else if (loancategory == "General")
                {
                    string l = "General Loan";
                    catid = cat_id(l);
                }
                DateTime req = DateTime.Now;
                string choose = "N";
                string choose1 = "Y";
            if (loancategory == "AgainstProvidentFund")
            {
                string loanpur = collection["Purpose"].ToString();
                if (loanpur == "Automobile")
                {
                    string stat = "Pending";
                    //  String qry = "update LoanApply  set LoanCategory='" + catid + "',LoanMoney='" + loan1 + "',NoOfInstallments='" + instal + "',InstallmentStartDate='" + startdate + "',InstallmentEndDate=''" + enddate + "',RequestDate='" + req + "',LoanAgreement='" + image1 + "',LoanForProperty='" + choose + "',LoanForAutomobile='" + choose1 + "',[Installment_Money]='" + instalmoney + "'where LoanApplyId = '" + ide + "' ";
                    String qry = "update LoanApply  set LoanCategory='" + catid + "',LoanMoney='"+loan1+ "',NoOfInstallments='"+instal+ "',RequestDate='"+req+"',LoanForProperty='"+choose+ "',LoanForAutomobile='"+choose1+ "',[Installment Money]='"+ instalmoney + "',InstallmentStartDate='"+startdate+ "',InstallmentEndDate='" + enddate + "' where LoanApplyId = '" + ide + "'";
                    String qry1 = "update LoanRequestStatus set RequestStatus = '" + stat + "' where LoanId = '"+ide+"'";

                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    SqlCommand sc2 = new SqlCommand(qry1, conn);
                    int j = sc2.ExecuteNonQuery();
                    if(option =="Yes")
                    {
                        docupload(image1, ide);
                    }
                }
                else if (loanpur == "Property")
                {
                    string stat = "Pending";
                    String qry = "update LoanApply  set LoanCategory='" + catid + "',LoanMoney='" + loan1 + "',NoOfInstallments='" + instal + "',RequestDate='" + req + "',LoanAgreement='" + image1 + "',LoanForProperty='" + choose1 + "',LoanForAutomobile='" + choose + "',[Installment Money]='" + instalmoney + "',InstallmentStartDate='" + startdate + "',InstallmentEndDate='" + enddate + "' where LoanApplyId = '" + ide + "'";
                    String qry1 = "update LoanRequestStatus set RequestStatus = '" + stat + "'where LoanId = '" + ide + "'";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    SqlCommand sc2 = new SqlCommand(qry1, conn);
                    int j = sc2.ExecuteNonQuery();
                    if (option == "Yes")
                    {
                        docupload(image1, ide);
                    }
                }
            }
            else
            {
                string stat = "Pending";
                String qry = "update LoanApply  set LoanCategory='" + catid + "',LoanMoney='" + loan1 + "',NoOfInstallments='" + instal + "',RequestDate='" + req + "',LoanAgreement='" + image1 + "',LoanForProperty='" + choose1 + "',LoanForAutomobile='" + choose1 + "',[Installment Money]='" + instalmoney + "',InstallmentStartDate='" + startdate + "',InstallmentEndDate='" + enddate + "' where LoanApplyId = '" + ide + "'";
                String qry1 = "update LoanRequestStatus set RequestStatus = '" + stat + "'where LoanId = '" + ide + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                SqlCommand sc2 = new SqlCommand(qry1, conn);
                int j = sc2.ExecuteNonQuery();
                if (option == "Yes")
                {
                    docupload(image1, ide);
                }
                if (i >= 1)
                { ViewBag(); }
                else
                {
                }
            }
                return RedirectToAction("Index", "Home");
          /*  }
            catchs
            {
                return View();
            }*/
        }
        [HttpPost, ActionName("documentsverification")]
        [ValidateAntiForgeryToken]
        public ActionResult documentsverification( FormCollection collection, HttpPostedFileBase imageproperty, HttpPostedFileBase imageauto)
        {
            int ide = docid;
            string property = loanprop(ide);
            string auto = loanauto(ide);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            if (property == "Y")
            {
                DateTime sub = DateTime.Now;
                string qry = "update LoanDocumentVerify set SubmissionDate='" + sub + "' where LoanId = '" + ide + "'";
                //  string qry = "insert into LoanDocumentVerify(PropertyDocument)values('"+image1+ "') select * from LoanDocumentVerify where LoanId = '" + ide+"'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                docuploadprop(imageproperty, ide);

            }
              
              
               
            
            if(auto == "Y")
            {
                
                DateTime sub = DateTime.Now;
                string qry = "update LoanDocumentVerify set SubmissionDate='" + sub + "' where LoanId = '" + ide + "'";
                //  string qry = "insert into LoanDocumentVerify(PropertyDocument)values('"+image1+ "') select * from LoanDocumentVerify where LoanId = '" + ide+"'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                docuploadauto(imageauto, ide);
            }

          return RedirectToAction("viewimag",new { @ide = ide });
        }
        // GET: loan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: loan/Delete/5
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
