using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Other
{
    public class Logoes
    {
        public Logoes()
        {

        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }
    }
}