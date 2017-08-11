namespace OpLab.API.Domain.Entities
{
    public class PointOfInterest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }
    }
}
