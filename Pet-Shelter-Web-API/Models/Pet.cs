using PetShelterWebAPI.Models.JoinTables;

namespace PetShelterWebAPI.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Specie Specie { get; set; }
        public Breed Breed { get; set; }
        public Shelter? Shelter { get; set; }
        public ICollection<PetOwner>? PetOwners { get; set; }
        public ICollection<Note>? Notes { get; set; }
    }
}
