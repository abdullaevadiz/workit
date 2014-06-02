using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dl.Classes
{


    [Table("Employees")]
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public double Salary { get; set; }

        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }

        #region Methods
        public double GetTax()
        {
            const double minimalSalaryLevel = 10000;
            const double middleSalaryLevel = 25000;
            const double minimalTaxPercent = 10 * 0.01;
            const double middleTaxPercent = 15 * 0.01;
            const double maximumTaxPercent = 25 * 0.01;

            if (Salary < minimalSalaryLevel)
                return Salary * minimalTaxPercent ;

            if (Salary < middleSalaryLevel)
                return Salary * middleTaxPercent;

            return Salary * maximumTaxPercent;
        }

        public double GetSalaryAfterTax()
        {
            return Salary - GetTax();
        }
        #endregion
    }
}
