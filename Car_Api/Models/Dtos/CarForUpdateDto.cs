using System.ComponentModel.DataAnnotations;

namespace Car_Api.Models.Dtos
{
    public class CarForUpdateDto
    {
        [Required]
        public string Model { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }
        [Required]
        public bool IsElectric { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}
