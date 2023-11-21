using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Api.Models
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string? Country { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

    }
}
