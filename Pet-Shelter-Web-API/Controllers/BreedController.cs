using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet_Shelter_Web_API.DTO;
using Pet_Shelter_Web_API.Interfaces;
using Pet_Shelter_Web_API.Repositories;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class BreedController : Controller
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedController(IBreedRepository breedRepository, IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Breed>))]
        public IActionResult GetSBreeds()
        {
            var Species = _mapper.Map<List<BreedDTO>>(_breedRepository.GetBreeds());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Species);
        }

        [HttpGet("ById/{BreedId}")]
        [ProducesResponseType(200, Type = typeof(Breed))]
        [ProducesResponseType(400)]
        public IActionResult GetBreed(int BreedId)
        {
            if (!_breedRepository.BreedExists(BreedId))
            {
                return NotFound();
            }

            var Breed = _mapper.Map<BreedDTO>(_breedRepository.GetBreed(BreedId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Breed);
        }

        [HttpGet("PetsByBreed/{BreedId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
        [ProducesResponseType(400)]
        public IActionResult GetPetsByBreed(int BreedId)
        {
            if (!_breedRepository.BreedExists(BreedId))
            {
                return NotFound();
            }

            var Pets = _mapper.Map<List<PetDTO>>(_breedRepository.GetPetsByBreed(BreedId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pets);
        }
    }
}
