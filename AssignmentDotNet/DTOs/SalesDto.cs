using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentDotNet.DTOs
{
    public class SalesDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime SalesDate { get; set; }
        [ForeignKey("Discount")]
        public int? DiscountId { get; set; }
    }
}
