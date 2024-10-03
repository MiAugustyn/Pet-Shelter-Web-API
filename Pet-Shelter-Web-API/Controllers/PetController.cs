using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet_Shelter_Web_API.DTO;
using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class PetController : Controller
    {
        private readonly IPetRepository _petRepository;
        private readonly IShelterRepository _shelterRepository;
        private readonly ISpecieRepository _specieRepository;
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public PetController(IPetRepository petRepository,
            IShelterRepository shelterRepository,
            ISpecieRepository specieRepository,
            IBreedRepository breedRepository,
            IMapper mapper)
        {
            _petRepository = petRepository;
            _shelterRepository = shelterRepository;
            _specieRepository = specieRepository;
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
        public IActionResult GetPets()
        {
            var Pets = _mapper.Map<List<PetDTO>>(_petRepository.GetPets());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pets);
        }

        [HttpGet("ByName/{PetName}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pet>))]
        [ProducesResponseType(400)]
        public IActionResult GetPets(string PetName)
        {
            if (!_petRepository.PetExists(PetName))
            {
                return NotFound();
            }

            var Pets = _mapper.Map<List<PetDTO>>(_petRepository.GetPets(PetName));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pets);
        }

        [HttpGet("ById/{PetId}")]
        [ProducesResponseType(200, Type = typeof(Pet))]
        [ProducesResponseType(400)]
        public IActionResult GetPet(int PetId)
        {
            if (!_petRepository.PetExists(PetId))
            {
                return NotFound();
            }

            var Pet = _mapper.Map<PetDTO>(_petRepository.GetPet(PetId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pet);
        }

        [HttpGet("SpecieByPet/{PetId}")]
        [ProducesResponseType(200, Type = typeof(Specie))]
        [ProducesResponseType(400)]
        public IActionResult GetSpecieByPet(int PetId)
        {
            if (!_petRepository.PetExists(PetId))
            {
                return NotFound();
            }

            var Specie = _mapper.Map<SpecieDTO>(_petRepository.GetSpecieByPet(PetId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Specie);
        }

        [HttpGet("BreedByPet/{PetId}")]
        [ProducesResponseType(200, Type = typeof(Breed))]
        [ProducesResponseType(400)]
        public IActionResult GetBreedByPet(int PetId)
        {
            if (!_petRepository.PetExists(PetId))
            {
                return NotFound();
            }

            var Breed = _mapper.Map<BreedDTO>(_petRepository.GetBreedByPet(PetId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Breed);
        }



        [HttpGet("NotesByPet/{PetId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<Note>))]
        [ProducesResponseType(400)]
        public IActionResult GetNotesByPet(int PetId)
        {
            if (!_petRepository.PetExists(PetId))
            {
                return NotFound(ModelState);
            }

            var Notes = _mapper.Map<List<NoteDTO>>(_petRepository.GetNotesByPet(PetId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Notes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePet([FromQuery] int ShelterId, [FromQuery] int SpecieId, [FromQuery] int BreedId, [FromBody] PetDTO NewPet)
        {
            if (NewPet == null || !_shelterRepository.ShelterExists(ShelterId))
            {
                return BadRequest(ModelState);
            }

            var NewPetCheck = _petRepository.GetPets()
                .Where(p => p.Name.Trim().ToLower() == NewPet.Name.Trim().ToLower() && p.Age == NewPet.Age)
                .FirstOrDefault();

            if (NewPetCheck != null)
            {
                ModelState.AddModelError("", "Pet already exists.");
                return StatusCode(422);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewPetMap = _mapper.Map<Pet>(NewPet);

            NewPetMap.Shelter = _shelterRepository.GetShelter(ShelterId);
            NewPetMap.Specie = _specieRepository.GetSpecie(SpecieId);
            NewPetMap.Breed = _breedRepository.GetBreed(BreedId);

            if (!_petRepository.CreatePet(NewPetMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving Pet.");
                return StatusCode(500);
            }

            return Ok("Pet created successfully.");
        }

        [HttpPut("{PetId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePet(int PetId, [FromQuery] int ShelterId, 
            [FromQuery] int SpecieId, [FromQuery] int BreedId, [FromBody] PetDTO UpdatedPet)
        {
            if (UpdatedPet  == null)
            {
                return BadRequest(ModelState);
            }

            if (!_petRepository.PetExists(PetId) || !_shelterRepository.ShelterExists(ShelterId)
                || !_breedRepository.BreedExists(BreedId) || !_specieRepository.SpecieExists(SpecieId))
            {
                return NotFound();
            }

            if (UpdatedPet.Id != PetId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PetMap = _mapper.Map<Pet>(UpdatedPet);
            PetMap.Shelter = _shelterRepository.GetShelter(ShelterId);
            PetMap.Specie = _specieRepository.GetSpecie(SpecieId);
            PetMap.Breed = _breedRepository.GetBreed(BreedId);

            if (!_petRepository.UpdatePet(PetMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating Pet.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
