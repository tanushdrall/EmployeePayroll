
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public class ConsoleLogger : IPayrollLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}
