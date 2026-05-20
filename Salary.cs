using System;

namespace EmployeePayroll
{
    public class Salary
    {
        
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayDate { get; set; }
        public string ProcessedBy { get; set; } = string.Empty;

        public DateTime Date { get; set; }
        public Employee? Employee { get; set; }  //make nullable 
        
    }
}