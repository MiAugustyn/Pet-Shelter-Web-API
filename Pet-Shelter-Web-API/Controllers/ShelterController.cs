using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet_Shelter_Web_API.DTO;
using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Controllers
{
    [Route(" api / [controller]")]
    [ApiController]
    public class ShelterController : Controller
    {
        private readonly IShelterRepository _shelterRepository;
        private readonly IMapper _mapper;

        public ShelterController(IShelterRepository shelterRepository, IMapper mapper)
        {
            _shelterRepository = shelterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Shelter>))]
        public IActionResult GetShelters()
        {
            var Shelters = _mapper.Map<List<ShelterDTO>>(_shelterRepository.GetShelters());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Shelters);
        }

        [HttpGet("ById/{ShelterId}")]
        [ProducesResponseType(200, Type = typeof(Shelter))]
        [ProducesResponseType(400)]
        public IActionResult GetShelter(int ShelterId)
        {
            if (!_shelterRepository.ShelterExists(ShelterId))
            {
                return NotFound();
            }

            var Shelter = _mapper.Map<ShelterDTO>(_shelterRepository.GetShelter(ShelterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Shelter);
        }

        [HttpGet("WorkersByShelter/{ShelterId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Worker>))]
        [ProducesResponseType(400)]
        public IActionResult GetWorkersByShelter(int ShelterId)
        {
            if (!_shelterRepository.ShelterExists(ShelterId))
            {
                return NotFound();
            }

            var Workers = _mapper.Map<List<WorkerDTO>>(_shelterRepository.GetWorkersByShelter(ShelterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Workers);
        }

        [HttpGet("PetsByShelter/{ShelterId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
        [ProducesResponseType(400)]
        public IActionResult GetPetsByShelter(int ShelterId)
        {
            if (!_shelterRepository.ShelterExists(ShelterId))
            {
                return NotFound();
            }

            var Pets = _mapper.Map<List<PetDTO>>(_shelterRepository.GetPetsByShelter(ShelterId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pets);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateShelter([FromBody] ShelterDTO NewShelter)
        {
            if (NewShelter == null)
            {
                return BadRequest(ModelState);
            }

            var NewShelterCheck = _shelterRepository.GetShelters()
                .Where(s => s.Address.Trim().ToLower() == NewShelter.Address.Trim().ToLower())
                .FirstOrDefault();

            if (NewShelterCheck != null)
            {
                ModelState.AddModelError("", "Shelter already exists.");
                return StatusCode(422);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewShelterMap = _mapper.Map<Shelter>(NewShelter);

            if (!_shelterRepository.CreateShelter(NewShelterMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving shelter.");
                return StatusCode(500);
            }

            return Ok("Shelter created successfully.");
        }
    }
}
