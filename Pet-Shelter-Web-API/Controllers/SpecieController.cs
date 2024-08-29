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
    public class SpecieController : Controller
    {
        private readonly ISpecieRepository _specieRepository;
        private readonly IMapper _mapper;

        public SpecieController(ISpecieRepository specieRepository,IMapper mapper)
        {
            _specieRepository = specieRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Specie>))]
        public IActionResult GetSpecies()
        {
            var Species = _mapper.Map<List<SpecieDTO>>(_specieRepository.GetSpecies());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Species);
        }


        [HttpGet("ById/{SpecieId}")]
        [ProducesResponseType(200, Type = typeof(Specie))]
        [ProducesResponseType(400)]
        public IActionResult GetSpecie(int SpecieId)
        {
            if (!_specieRepository.SpecieExists(SpecieId))
            {
                return NotFound();
            }

            var Specie = _mapper.Map<SpecieDTO>(_specieRepository.GetSpecie(SpecieId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Specie);
        }

        [HttpGet("PetsBySpecie/{SpecieId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
        [ProducesResponseType(400)]
        public IActionResult GetPetsBySpecie(int SpecieId)
        {
            if (!_specieRepository.SpecieExists(SpecieId))
            {
                return NotFound();
            }

            var Pets = _mapper.Map<List<PetDTO>>(_specieRepository.GetPetsBySpecie(SpecieId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pets);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSpecie([FromBody] SpecieDTO NewSpecie)
        {
            if (NewSpecie == null)
            {
                return BadRequest(ModelState);
            }

            var SpecieCheck = _specieRepository.GetSpecies()
                .Where(s => s.Name.Trim().ToLower() == NewSpecie.Name.Trim().ToLower())
                .FirstOrDefault();

            if (SpecieCheck != null)
            {
                ModelState.AddModelError("", "Specie already exists.");
                return StatusCode(422);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewSpecieMap = _mapper.Map<Specie>(NewSpecie);

            if (!_specieRepository.CreateSpecie(NewSpecieMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving specie.");
                return StatusCode(500);
            }

            return Ok("Specie created successfully.");
        }
    }
}
