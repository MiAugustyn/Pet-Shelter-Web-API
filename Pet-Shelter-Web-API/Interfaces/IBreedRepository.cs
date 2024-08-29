using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface IBreedRepository
    {
        ICollection<Breed> GetBreeds();
        Breed GetBreed(int id);
        ICollection<Pet> GetPetsByBreed(int petId);
        bool BreedExists(int id);
        bool CreateBreed(Breed breed);
        bool Save();
    }
}
