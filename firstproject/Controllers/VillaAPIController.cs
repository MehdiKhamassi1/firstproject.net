using Microsoft.AspNetCore.Mvc;
using firstproject.Models.Dto;
using firstproject.Models;
using firstproject.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using firstproject.Repository.IRepository;
namespace firstproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly IVillaRepository _dbVilla;
        protected ApiResponse _response;
        public VillaAPIController(IVillaRepository dbVilla) {
            _dbVilla = dbVilla; 
            this._response = new ();
        }
        
        [HttpGet]
        public async Task<ActionResult<ApiResponse>>GetVillas()
        {
            try
            {
                _response.Result = await _dbVilla.GetAllAsync();
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{id:int}",Name ="getvilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ApiResponse>> GetVillaById(int id)
        {
            try { 
            if (id == 0)
            {
                
                return BadRequest();
            }
            var villa = await _dbVilla.GetAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _response.Result = await _dbVilla.GetAsync(u => u.Id == id);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateVilla([FromBody] VillaCreateDTO villaDTO)
        {
            try { 
            
            if (villaDTO == null)
            {
                return BadRequest();
            }
            //if (villaDTO.Id > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            Villa model=new ()
                {
                Amenity= villaDTO.Amenity,
                Details= villaDTO.Details,
                ImageUrl= villaDTO.ImageUrl,
                Name= villaDTO.Name,
                Occupancy= villaDTO.Occupancy,
                Rate= villaDTO.Rate,
                Sqft= villaDTO.Sqft,
            };
            await _dbVilla.CreateAsync(model);
            _response.Result = model;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "deletevilla")]
        public async Task<ActionResult<ApiResponse>> DeleteVilla(int id)
        {
            try { 
            if (id == 0)
            {
                return BadRequest();
            }
            var villa= await _dbVilla.GetAsync(u=>u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _dbVilla.RemoveAsync(villa);   
            _response.StatusCode= System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "updatevilla")]
        public async Task<ActionResult<ApiResponse>> UpdateVilla(int id,[FromBody] VillaUpdateDTO villaDTO)
        {
            try { 
            if(villaDTO == null ||id != villaDTO.Id){
                return BadRequest();
            }
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            await _dbVilla.UpdateAsync(model);
            _response.StatusCode = System.Net.HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPatch("{id:int}", Name = "updatepartialvilla")]
        public async Task<IActionResult> UpdatePartialVilla(int id,JsonPatchDocument<VillaUpdateDTO> patchDTO)
        {
            
            if (patchDTO==null ||id==0)
            { return BadRequest(); }
            var villa = await _dbVilla.GetAsync(u => u.Id == id,tracked:false);
            VillaUpdateDTO villaDTO = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO,ModelState);
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
            };
            await _dbVilla.UpdateAsync(model);
            if (!ModelState.IsValid)
            {

            return BadRequest(); }
            return NoContent();

        }
        

    }
}
