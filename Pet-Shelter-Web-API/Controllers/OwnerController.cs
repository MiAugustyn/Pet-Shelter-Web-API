using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pet_Shelter_Web_API.DTO;
using Pet_Shelter_Web_API.Interfaces;
using PetShelterWebAPI.Models;

namespace Pet_Shelter_Web_API.Controllers
{
    [Route("api / [controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var Owners = _mapper.Map<List<OwnerDTO>>(_ownerRepository.GetOwners());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Owners);
        }

        [HttpGet("ByName/{OwnerName}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwners(string OwnerName)
        {
            if (!_ownerRepository.OwnerExists(OwnerName))
            {
                return NotFound();
            }

            var Owners = _mapper.Map<List<OwnerDTO>>(_ownerRepository.GetOwners(OwnerName));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Owners);
        }

        [HttpGet("ById/{OwnerId}")]
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int OwnerId)
        {
            if (!_ownerRepository.OwnerExists(OwnerId))
            {
                return NotFound();
            }

            var Owner = _mapper.Map<OwnerDTO>(_ownerRepository.GetOwner(OwnerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Owner);
        }

        [HttpGet("PetsByOwner/{OwnerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pet>))]
        [ProducesResponseType(400)]
        public IActionResult GetPetsByOwner(int OwnerId)
        {
            if (!_ownerRepository.OwnerExists(OwnerId))
            {
                return NotFound();
            }

            var Pets = _mapper.Map<List<PetDTO>>(_ownerRepository.GetPetsByOwner(OwnerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(Pets);
        }
    }
}
