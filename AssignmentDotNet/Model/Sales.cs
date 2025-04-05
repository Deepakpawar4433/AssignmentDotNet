using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentDotNet.Model
{
    public class Sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }
        public Mobile Mobile { get; set; }
        public int Quantity { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public DateTime SalesDate { get; set; }
        [ForeignKey("Discount")]
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
