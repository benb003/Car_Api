using System.ComponentModel.DataAnnotations;

namespace Car_Api.Models.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsElectric { get; set; }
        public int BrandId {  get; set; }
    }
}
