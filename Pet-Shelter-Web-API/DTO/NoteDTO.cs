using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.DTO
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Pet Pet { get; set; }
        public Worker Worker { get; set; }
    }
}
