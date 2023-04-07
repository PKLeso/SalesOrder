using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesOrder.Models
{
    public class SalesOrders : BaseEntity
    {
        [MaxLength(150)]
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }
    }
}
