using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DAL.Entities
{
    [Table("Jobs")]
    public class Job
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Название вакансии должно быть не длинее 200 символов"), MinLength(5, ErrorMessage = "Название вакансии не должно быть менее 5 символов")]
        public string Name { get; set; }

        [Required]
        public string Requirements { get; set; }
        public string Bonuses { get; set; }
        public string Contact { get; set; }
        public string Instructions { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime ModificationDate { get; set; }

        [Required]
        public virtual JobsCity JobsCity { get; set; }

        [Required]
        public virtual JobsType JobsType { get; set; }
        
        [Required]
        public virtual JobsCategory JobsCategory { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
