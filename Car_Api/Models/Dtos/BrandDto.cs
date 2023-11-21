namespace Car_Api.Models.Dtos
{
    public class BrandDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public int NumberOfCars
        {
            get
            {
                return Cars.Count;
            }
        }
    }
}
