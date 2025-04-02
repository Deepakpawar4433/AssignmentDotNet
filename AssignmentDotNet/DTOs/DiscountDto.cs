using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentDotNet.DTOs
{
    public class DiscountDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Mobile")]
        public int MobileId { get; set; }
        [Required]
        public decimal DiscountAmont { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ValidUntil { get; set; }
    }
}
