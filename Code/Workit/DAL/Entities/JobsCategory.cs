using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("JobsCategory")]
    public class JobsCategory
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<Subscribe> Subscribes { get; set; } 
    }
}
