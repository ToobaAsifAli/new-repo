//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeLoanManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Installment
    {
        public int InstallmentId { get; set; }
        public int LoanId { get; set; }
        public Nullable<int> InstallmentsNo { get; set; }
        public Nullable<System.DateTime> InstallmentDate { get; set; }
        public string IsPaid { get; set; }
    
        public virtual LoanApply LoanApply { get; set; }
    }
}
