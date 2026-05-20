using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public interface IPayrollLogger
    {
        void Log(string message);
    }
}
