﻿using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.DTO
{
    public class ShelterDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public ICollection<Worker> Workers { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
