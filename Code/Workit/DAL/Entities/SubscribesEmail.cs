using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("SubscribesEmails")]
    public class SubscribesEmail
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Email должно быть не длинее 200 символов"), MinLength(5, ErrorMessage = "Email не должен быть менее 5 символов")]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string Code { get; set; }

        public virtual ICollection<Subscribe> Subscribes { get; set; } 

        public SubscribesEmail() { }

        public SubscribesEmail(Guid id, string email, bool isActive)
        {
            this.Id = id;
            this.Email = email;
            this.Code = id.ToString().Replace("", string.Empty).Take(10).ToString().ToLower();
            this.IsActive = isActive;
            this.CreateDate = DateTime.Now;
        }
    }
}
