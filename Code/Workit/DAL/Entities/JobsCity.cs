using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("JobsCities")]
    public class JobsCity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Districts { get; set; }

        public virtual ICollection<Job> Jobs { get; set; } 
    }
}
