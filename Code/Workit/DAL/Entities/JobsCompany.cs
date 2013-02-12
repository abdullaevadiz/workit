using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("JobsCompanies")]
    public class JobsCompany
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public virtual User User { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Url { get; set; }

        public string Logo { get; set; }

        public string MiniLogo { get; set; }

        [Required]
        public string AuthorName { get; set; }
    }
}
