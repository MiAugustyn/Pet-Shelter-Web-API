using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.DTO
{
    public class SpecieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
