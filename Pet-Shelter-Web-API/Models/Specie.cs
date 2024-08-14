namespace PetShelterWebAPI.Models
{
    public class Specie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
