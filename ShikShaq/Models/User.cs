using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShikShaq.Models
{
    public class User
    {

        [Key]
        public long id { get; set; }

        public string name { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This field is required!")]
        public string email { get; set;}

        public DateTime birthday { get; set; }

        public String address { get; set; }

        public float height { get; set; }

        public float weigth { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required!")]
        public String password { get; set; }

        public bool isAdmin { get; set; }

    }
}