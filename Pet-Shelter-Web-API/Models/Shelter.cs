namespace PetShelterWebAPI.Models
{
    public class Shelter
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public ICollection<Worker> Workers { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
