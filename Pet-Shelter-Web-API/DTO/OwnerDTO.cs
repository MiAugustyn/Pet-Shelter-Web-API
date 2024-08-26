using PetShelterWebAPI.Models.JoinTables;

namespace Pet_Shelter_Web_API.DTO
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
