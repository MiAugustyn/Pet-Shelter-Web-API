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
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IPetRepository petRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _petRepository = petRepository;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int PetId, [FromBody] OwnerDTO NewOwner)
        {
            if (NewOwner == null || !_petRepository.PetExists(PetId))
            {
                return BadRequest(ModelState);
            }

            var NewOwnerCheck = _ownerRepository.GetOwners()
                .Where(o => o.Email.Trim().ToLower() == NewOwner.Email.Trim().ToLower())
                .FirstOrDefault();

            if (NewOwnerCheck != null)
            {
                ModelState.AddModelError("", "Owner already exists.");
                return StatusCode(422);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewOwnerMap = _mapper.Map<Owner>(NewOwner);

            if (!_ownerRepository.CreateOwner(PetId, NewOwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving owner.");
                return StatusCode(500);
            }

            return Ok("Owner created successfully.");
        }

        [HttpPut("{OwnerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOwner(int OwnerId, [FromBody] OwnerDTO UpdatedOwner)
        {
            if (UpdatedOwner == null)
            {
                return BadRequest(ModelState);
            }

            if (!_ownerRepository.OwnerExists(OwnerId))
            {
                return NotFound();
            }

            if (UpdatedOwner.Id != OwnerId)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var OwnerMap = _mapper.Map<Owner>(UpdatedOwner);

            if (!_ownerRepository.UpdateOwner(OwnerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating owner.");
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
