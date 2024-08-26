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
        private readonly IMapper _mapper;

        public PetController(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
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
    }
}
