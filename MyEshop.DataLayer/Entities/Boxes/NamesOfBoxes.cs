using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Boxes
{
    public class NamesOfBoxes
    {
        public NamesOfBoxes()
        {

        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "نام باکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string BoxTitle { get; set; }

        #region Relations

        public List<Boxes> Boxes { get; set; }

        #endregion
    }
}
