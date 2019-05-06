using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLoanManagementSystem.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public System.DateTime HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string AccountNo { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Gender { get; set; }
        public string ProvidentFundOpted { get; set; }
        public string ProvidentFundAmount { get; set; }
        public Nullable<float> ProvidentFundPercentage { get; set; }
        public int DepartmentId { get; set; }
        public Nullable<System.DateTime> ProvidentFundOptedDate { get; set; }
        public string CNIC { get; set; }
        public string Password { get; set; }
        public string IsEmailSent { get; set; }
    }
}