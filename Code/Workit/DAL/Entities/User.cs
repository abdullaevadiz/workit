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
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Email должно быть не длинее 200 символов"), MinLength(5, ErrorMessage = "Email не должен быть менее 5 символов")]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(200, ErrorMessage = "Пароль должен быть не длинее 200 символов"), MinLength(5, ErrorMessage = "Пароль не должен быть менее 5 символов")]
        public string Password { get; set; }

        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public string Token { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<JobsCompany> JobsCompanies { get; set; }
        
        //public int Role { get; set; }
    }
}
