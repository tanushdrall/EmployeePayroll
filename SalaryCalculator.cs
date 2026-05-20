
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public class SalaryCalculator : ISalaryCalculator
    {
        public Salary Calculate(Employee employee)
        {
            var gross = employee.BaseSalary;
            var tax = gross * 0.1m; 
            return new Salary
            {
                EmployeeId = employee.Id,
                GrossSalary = gross,
                Deductions = tax,
                NetSalary = gross - tax
            };
        }

        public object Calculate(object emp)
        {
            throw new NotImplementedException();
        }
    }
}
