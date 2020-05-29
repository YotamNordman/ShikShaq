using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

        [StringLength(45)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(45)]
        public string Address { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }

        [StringLength(45)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [StringLength(1)]
        public string IsAdmin { get; set; }
    }
}
