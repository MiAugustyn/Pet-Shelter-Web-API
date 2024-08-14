namespace PetShelterWebAPI.Models
{
    public class Breed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
