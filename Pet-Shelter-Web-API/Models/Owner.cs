using PetShelterWebAPI.Models.JoinTables;

namespace PetShelterWebAPI.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<PetOwner>? PetOwners { get; set; }
    }
}
