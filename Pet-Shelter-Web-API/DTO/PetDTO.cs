using PetShelterWebAPI.Models.JoinTables;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.DTO
{
    public class PetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Specie Specie { get; set; }
        public Breed Breed { get; set; }
        public Shelter? Shelter { get; set; }
        public ICollection<Note>? Notes { get; set; }
    }
}