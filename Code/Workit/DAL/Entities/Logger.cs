using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Logging")]
    public class Logger
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string MethodName { get; set; }

        [Required]
        public DateTime ExeptionTime { get; set; }
    }
}
