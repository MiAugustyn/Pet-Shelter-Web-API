using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Interfaces
{
    public interface ISpecieRepository
    {
        ICollection<Specie> GetSpecies();
        Specie GetSpecie(int id);
        ICollection<Pet> GetPetsBySpecie(int specieId);
        bool SpecieExists(int id);
    }
}
