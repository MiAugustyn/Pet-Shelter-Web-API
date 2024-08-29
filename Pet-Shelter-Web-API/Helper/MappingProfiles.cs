using AutoMapper;
using PetShelterWebAPI.Models;
using Pet_Shelter_Web_API.DTO;

namespace PetShelterWebAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Shelter, ShelterDTO>().ReverseMap();
            CreateMap<Worker, WorkerDTO>().ReverseMap();
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<Specie, SpecieDTO>().ReverseMap();
            CreateMap<Breed, BreedDTO>().ReverseMap();
        }
    }
}
