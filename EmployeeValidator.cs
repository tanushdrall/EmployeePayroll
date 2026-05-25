using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
   public class EmployeeValidator : IValidator<Employee>
    {
        public bool Validate(Employee employee)
        {
            
            if (string.IsNullOrWhiteSpace(employee.Name)) return false;
            if (employee.BaseSalary <= 0) return false;
            if (employee.DepartmentId <= 0) return false;  
            return true;
        }
    }
}
