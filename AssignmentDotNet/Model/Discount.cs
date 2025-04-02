using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentDotNet.Model
{
    public class Discount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }

        public virtual Mobile Mobile { get; set; }
        [Required]
        public decimal DiscountedAmount { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
