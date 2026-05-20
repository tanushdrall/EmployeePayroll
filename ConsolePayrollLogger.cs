using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public class ConsolePayrollILogger : IPayrollLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[PayrollLog] {message}");
        }
    }
}
