using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Subscribes")]
    public class Subscribe
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public virtual JobsCategory JobsCategory { get; set; }

        [Required]
        public virtual SubscribesEmail SubscribesEmail { get; set; }
    }
}
