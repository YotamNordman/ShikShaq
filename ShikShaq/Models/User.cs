using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShikShaq.Models
{
    public class User
    {

        [Key]
        private long id { get; set; }
        private string name { get; set; }
        private string email { get; set;}
        private DateTime birthday { get; set; }
        private String address { get; set; }
        private float height { get; set; }
        private float weigth { get; set; }
        private String password { get; set; }
        private bool isAdmin { get; set; }

    }
}