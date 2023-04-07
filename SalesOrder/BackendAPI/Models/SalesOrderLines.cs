using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesOrder.Models
{
    public class SalesOrderLines
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Article { get; set; }
        public decimal Amount { get; set; }

        public SalesOrders SalesOrder { get; set; }
    }
}
