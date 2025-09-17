using System.ComponentModel.DataAnnotations;

namespace Food.Dal.Models.Payment
{
    public class RatingRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
