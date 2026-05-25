using System.Collections.Generic;

namespace EmployeePayroll
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public decimal BaseSalary { get; set; }

        
        public Department? Department { get; set; }

        public ICollection<Salary> Salaries { get; set; } = new List<Salary>();

        public Employee()
        { 

        }


        public Employee(int id, string name, int deptId, decimal baseSalary)
        {
            Id = id;
            Name = name;
            DepartmentId = deptId;
            BaseSalary = baseSalary;
        }
    }
}