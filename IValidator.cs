using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
   public interface IValidator<T>
    {
        bool Validate(T entity);
    }
}
