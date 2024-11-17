// using System.Net;
// using AutoMapper;
// using Microsoft.AspNetCore.JsonPatch;
// using Microsoft.AspNetCore.Mvc;
// using Villa.Application.Common.Interfaces;
// using Villa.Domain.Entities;
// using Villa.Domain.Entities.Dtos.Villa;

// namespace Villa.Api.Controllers;

// //[Route("api/[conroller]")]
// [ApiController]
// [Route("api/villas")]
// public class VillaAPIController : ControllerBase
// {
//     private readonly ILogger<VillaAPIController> _logger;
//     private readonly IUnitOfWork _unitOfWork;
//     private readonly IMapper _mapper;
//     protected Response _response;

//     public VillaAPIController(ILogger<VillaAPIController> logger,
//          IMapper mapper,
//          IUnitOfWork unitOfWork)
//     {
//         _logger = logger;
//         _mapper = mapper;
//         _unitOfWork = unitOfWork;
//         _response = new();
//     }

//     [HttpGet]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     public async Task<IActionResult> GetVillas()
//     {
//         try
//         {
//             _logger.LogInformation("Getting all villas");
//             var villas = await _unitOfWork.Villa.GetAll();
//             _response.Result = _mapper.Map<List<VillaDto>>(villas);
//             _response.HttpStatusCode = HttpStatusCode.OK;
//             return Ok(_response);
//         }
//         catch (Exception e)
//         {
//             _response.IsSuccess = false;
//             _response.Erros = [e.ToString()];
//         }
//         return Ok(_response);
//     }

//     [HttpGet("{id:int}", Name = "GetVilla")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     //[ProducesResponseType(404)]
//     public async Task<IActionResult> GetVilla(int id)
//     {
//         try
//         {

//             if (id is 0)
//             {
//                 _response.HttpStatusCode = HttpStatusCode.BadRequest;
//                 return BadRequest(_response);
//             }
//             var villa = await _unitOfWork.Villa.Get(v => v.Id == id);
//             if (villa is null)
//             {
//                 _response.HttpStatusCode = HttpStatusCode.NotFound;
//                 return NotFound(_response);
//             }
//             _response.Result = _mapper.Map<VillaDto>(villa);
//             _response.HttpStatusCode = HttpStatusCode.OK;
//             return Ok(_response);
//         }
//         catch (Exception e)
//         {
//             _response.IsSuccess = false;
//             _response.Erros.Add(e.ToString());
//         }
//         return Ok(_response);
//     }

//     [HttpPost]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//     public async Task<IActionResult> CreateVilla(VillaCreateDto? createDto)
//     {
//         try
//         {
//             if (createDto is null)
//             {
//                 _response.HttpStatusCode = HttpStatusCode.BadRequest;
//                 return BadRequest(_response);
//             }
//             var villa = _mapper.Map<VillaModel>(createDto);
//             await _unitOfWork.Villa.Add(villa);
//             await _unitOfWork.SaveAsync();
//             _response.HttpStatusCode = HttpStatusCode.Created;
//             _response.Result = villa;
//             return CreatedAtRoute("GetVilla", new { id = villa.Id }, _response);
//         }
//         catch (Exception e)
//         {
//             _response.IsSuccess = false;
//             _response.Erros = [e.ToString()];
//         }
//         return Ok(_response);
//     }


//     [HttpDelete("{id:int}", Name = "DeleteVilla")]
//     [ProducesResponseType(StatusCodes.Status204NoContent)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> DeleteVilla(int? id)
//     {
//         try
//         {

//             if (id is null or 0)
//             {
//                 _response.HttpStatusCode = HttpStatusCode.BadRequest;
//                 return BadRequest(_response);
//             }
//             var villa = await _unitOfWork.Villa.Get(v => v.Id == id);
//             if (villa is null)
//                 return NotFound();
//             _unitOfWork.Villa.Remove(villa);
//             await _unitOfWork.SaveAsync();
//             _response.HttpStatusCode = HttpStatusCode.NoContent;
//             _response.IsSuccess = true;
//             return Ok(_response);
//         }
//         catch (Exception e)
//         {
//             _response.IsSuccess = false;
//             _response.Erros = [e.ToString()];
//         }
//         return Ok(_response);
//     }

//     [HttpPut("{id:int}", Name = "UpdateVilla")]
//     public async Task<IActionResult> UpdateVilla(int? id, VillaUpdateDto updateDto)
//     {
//         try
//         {
//             if (updateDto is null || id != updateDto.Id)
//             {
//                 _response.HttpStatusCode = HttpStatusCode.BadRequest;
//                 return BadRequest(_response);
//             }
//             var villa = _mapper.Map<VillaModel>(updateDto);
//             _unitOfWork.Villa.Update(villa);
//             await _unitOfWork.SaveAsync();
//             _response.HttpStatusCode = HttpStatusCode.NoContent;
//             _response.IsSuccess = true;
//             return Ok(_response);
//         }
//         catch (Exception e)
//         {
//             _response.IsSuccess = false;
//             _response.Erros = [e.ToString()];
//         }
//         return Ok(_response);
//     }

//     [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
//     public async Task<IActionResult> UpdatePartialVilla(int? id, JsonPatchDocument<VillaUpdateDto> patchDto)
//     {
//         try
//         {
//             if (patchDto is null || id == 0)
//             {
//                 _response.HttpStatusCode = HttpStatusCode.BadRequest;
//                 return BadRequest(_response);
//             }

//             var villa = await _unitOfWork.Villa.Get(v => v.Id == id, isTracked: false);
//             if (villa is null)
//                 return NotFound();

//             var villaDto = _mapper.Map<VillaUpdateDto>(villa);

//             patchDto.ApplyTo(villaDto, ModelState);

//             var model = _mapper.Map<VillaModel>(villaDto);

//             _unitOfWork.Villa.Update(model);
//             await _unitOfWork.SaveAsync();
//             _response.HttpStatusCode = HttpStatusCode.NoContent;
//             _response.IsSuccess = true;
//             return Ok(_response);
//         }
//         catch (Exception e)
//         {
//             _response.IsSuccess = false;
//             _response.Erros = [e.ToString()];
//         }
//         return Ok(_response);
//     }
// }
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Villa.Application.Common.Interfaces;
using Villa.Domain.Entities;
using Villa.Domain.Entities.Dtos.Villa;

namespace Villa.Api.Controllers
{
    [ApiController]
    [Route("api/villas")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VillaAPIController(ILogger<VillaAPIController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVillas()
        {
            try
            {
                _logger.LogInformation("Getting all villas");
                var villas = await _unitOfWork.Villa.GetAll();
                var villasDto = _mapper.Map<List<VillaDto>>(villas);
                return Ok(villasDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching villas.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while fetching the villas." });
            }
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVilla(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { error = "Invalid villa ID." });
                }

                var villa = await _unitOfWork.Villa.Get(v => v.Id == id);
                if (villa is null)
                {
                    return NotFound(new { error = "Villa not found." });
                }

                var villaDto = _mapper.Map<VillaDto>(villa);
                return Ok(villaDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching villa.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while fetching the villa." });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVilla(VillaCreateDto createDto)
        {
            try
            {
                if (createDto is null)
                {
                    return BadRequest(new { error = "Villa creation data is required." });
                }

                var villa = _mapper.Map<VillaModel>(createDto);
                await _unitOfWork.Villa.Add(villa);
                await _unitOfWork.SaveAsync();

                return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating villa.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while creating the villa." });
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVilla(int? id)
        {
            try
            {
                if (id is null or 0)
                {
                    return BadRequest(new { error = "Invalid villa ID." });
                }

                var villa = await _unitOfWork.Villa.Get(v => v.Id == id);
                if (villa is null)
                {
                    return NotFound(new { error = "Villa not found." });
                }

                _unitOfWork.Villa.Remove(villa);
                await _unitOfWork.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting villa.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while deleting the villa." });
            }
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVilla(int? id, VillaUpdateDto updateDto)
        {
            try
            {
                if (updateDto is null || id != updateDto.Id)
                {
                    return BadRequest(new { error = "Invalid villa update data." });
                }

                var villa = await _unitOfWork.Villa.Get(v => v.Id == id, isTracked: false);
                if (villa is null)
                {
                    return NotFound(new { error = "Villa not found." });
                }

                var model = _mapper.Map<VillaModel>(updateDto);
                _unitOfWork.Villa.Update(model);
                await _unitOfWork.SaveAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating villa.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while updating the villa." });
            }
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePartialVilla(int? id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            try
            {
                if (patchDto is null || id == 0)
                {
                    return BadRequest(new { error = "Invalid villa patch data." });
                }

                var villa = await _unitOfWork.Villa.Get(v => v.Id == id, isTracked: false);
                if (villa is null)
                {
                    return NotFound(new { error = "Villa not found." });
                }

                var villaDto = _mapper.Map<VillaUpdateDto>(villa);
                patchDto.ApplyTo(villaDto, ModelState);

                var model = _mapper.Map<VillaModel>(villaDto);
                _unitOfWork.Villa.Update(model);
                await _unitOfWork.SaveAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating partial villa.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while updating the villa." });
            }
        }
    }
}