using PetShelterWebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace Pet_Shelter_Web_API.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
