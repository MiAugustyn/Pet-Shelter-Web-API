using Pet_Shelter_Web_API.DTO;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface IShelterRepository
    {
        ICollection<Shelter> GetShelters();
        Shelter GetShelter(int id);
        ICollection<Worker> GetWorkersByShelter(int shelterId);
        ICollection<Pet> GetPetsByShelter(int shelterId);
        bool ShelterExists(int id);
        bool CreateShelter(Shelter shelter);
        bool UpdateShelter(Shelter shelter);
        bool Save();

    }
}
