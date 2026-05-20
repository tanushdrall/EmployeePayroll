using System.Collections.Generic;

namespace EmployeePayroll
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public decimal BaseSalary { get; set; }

        // ✅ Navigation property to Department
        public Department? Department { get; set; }

        // ✅ Navigation property to Salaries
        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();

        // Optional constructors
        public Employee()
        { 
        }


        public Employee(int id, string name, int deptId, int baseSalary)
        {
            Id = id;
            Name = name;
            DepartmentId = deptId;
            BaseSalary = baseSalary;
        }
    }
}