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
    }
}
