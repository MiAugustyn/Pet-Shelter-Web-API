using AutoMapper;
using PetShelterWebAPI.Models;
using Pet_Shelter_Web_API.DTO;

namespace PetShelterWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pet, PetDTO>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Shelter, ShelterDTO>();
            CreateMap<Worker, WorkerDTO>();
            CreateMap<Note, NoteDTO>();
            CreateMap<Specie, SpecieDTO>();
            CreateMap<Breed, BreedDTO>();
        }
    }
}
