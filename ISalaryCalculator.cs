using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public interface ISalaryCalculator
    {
        Salary Calculate(Employee employee);
        object Calculate(object emp);
    }
}
