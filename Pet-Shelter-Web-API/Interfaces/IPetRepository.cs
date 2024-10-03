using Microsoft.AspNetCore.Mvc;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface IPetRepository
    {
        ICollection<Pet> GetPets();
        ICollection<Pet> GetPets(string name);
        Specie GetSpecieByPet(int petId);
        Breed GetBreedByPet(int petId);
        Pet GetPet(int id);
        ICollection<Note> GetNotesByPet(int petId);
        bool PetExists(int id);
        bool PetExists(string name);
        bool CreatePet(Pet pet);
        bool UpdatePet(Pet pet);
        bool Save();
    }
}
