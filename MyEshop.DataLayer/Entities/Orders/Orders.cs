using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.DataLayer.Entities.Orders
{
    public class Orders
    {
        public Orders()
        {

        }

        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int CartSum { get; set; }
        public bool IsFinaly { get; set; }
        public bool HasRead { get; set; }
        public bool HasOrdered { get; set; }

        #region Relations

        public List<OrderDetails> OrderDetails { get; set; }

        [ForeignKey("UserId")]
        public MyEshop.DataLayer.Entities.Users.Users User { get; set; }

        #endregion

    }
}
