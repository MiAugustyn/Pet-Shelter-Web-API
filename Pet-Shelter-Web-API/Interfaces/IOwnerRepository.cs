using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        ICollection<Owner> GetOwners(string name);
        ICollection<Pet> GetPetsByOwner(int ownerId);
        Owner GetOwner(int id);
        bool OwnerExists(int id);
        bool OwnerExists(string name);
    }
}
