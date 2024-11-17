using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Villa.Application.Common.Interfaces;
using Villa.Domain.Entities;
using Villa.Domain.Entities.Dtos.VIllaNumber;

namespace VillaAPI.Controllers
{
    [ApiController]
    [Route("api/villanumbers")]
    public class VillaNumberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VillaNumberController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var villasNumbers = await _unitOfWork.VillaNumber.GetAll();
                var villasNumbersDto = _mapper.Map<List<VillaNumberDto>>(villasNumbers);
                return Ok(villasNumbersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while fetc hing the villa numbers." });
            }
        }

        [HttpGet("{villaNo:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int? villaNo)
        {
            try
            {
                if (villaNo is null)
                {
                    return BadRequest(new { error = "Villa number is required." });
                }

                var villa = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNo == villaNo);
                if (villa is null)
                {
                    return NotFound(new { error = "Villa number not found." });
                }

                var villaNumberDto = _mapper.Map<VillaNumberDto>(villa);
                return Ok(villaNumberDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while fetching the villa number." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVillaNumber([FromBody] VillaNumberCreateDto villaNumberCreateDto)
        {
            try
            {
                if (villaNumberCreateDto is null)
                {
                    return BadRequest(new { error = "Villa number creation data is required." });
                }

                var villa = await _unitOfWork.Villa.Get(v => v.Id == villaNumberCreateDto.VillaId);
                if (villa is null)
                {
                    return BadRequest(new { error = "Invalid villa ID." });
                }

                var villaNo = await _unitOfWork.VillaNumber.Get(vn => vn.VillaId == villaNumberCreateDto.VillaNo);
                if (villaNo is not null)
                {
                    return BadRequest(new { error = "Villa number already exists." });
                }

                var villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDto);
                await _unitOfWork.VillaNumber.Add(villaNumber);
                await _unitOfWork.SaveAsync();

                return CreatedAtRoute("GetVillaNumber", new { villaNo = villaNumber.VillaNo }, villaNumber);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while creating the villa number." });
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int? id, [FromBody] VillaNumberUpdateDto villaNumberUpdateDto)
        {
            try
            {
                if (id is null or 0 || id != villaNumberUpdateDto.VillaNo)
                {
                    return BadRequest(new { error = "Invalid villa number ID." });
                }

                var villaNumberFromDb = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNo == id, isTracked: false);
                if (villaNumberFromDb is null)
                {
                    return NotFound(new { error = "Villa number not found." });
                }

                var villaNumber = _mapper.Map<VillaNumber>(villaNumberUpdateDto);
                _unitOfWork.VillaNumber.Update(villaNumber);
                await _unitOfWork.SaveAsync();

                return Ok(villaNumber);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while updating the villa number." });
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null or 0)
                {
                    return BadRequest(new { error = "Invalid villa number ID." });
                }

                var villaNumber = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNo == id);
                if (villaNumber is null)
                {
                    return NotFound(new { error = "Villa number not found." });
                }

                _unitOfWork.VillaNumber.Remove(villaNumber);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while deleting the villa number." });
            }
        }
    }
}