using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Data.DAL.Pagination;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Sword;
using SampleWebAPI.Helpers;
using SampleWebAPI.Helpers.Wrapper;

namespace SampleWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwordsController : ControllerBase
    {
        private readonly ISword _swordDAL;
        private readonly IMapper _mapper;
        private readonly SamuraiContext _context;

        public SwordsController(ISword swordDAL, IMapper mapper, SamuraiContext context)
        {
            _swordDAL = swordDAL;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("SwordPaging")]
        public async Task<IEnumerable<SwordWithTypeDTO>> GetAllPaging([FromQuery] PaginationParams @params)
        {
            var results = await _swordDAL.PagingWithType(@params);
            var samuraiReadDTO = _mapper.Map<IEnumerable<SwordWithTypeDTO>>(results);
            return samuraiReadDTO;
        }

        [HttpGet]
        public async Task<IEnumerable<SwordReadDTO>> GetAll()
        {
            var results = await _swordDAL.GetAll();
            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

        [HttpGet("{id}")]
        public async Task<SwordReadDTO> Get(int id)
        {
            var result = await _swordDAL.GetById(id);
            if (result == null)
            {
                throw new Exception($"Data Sword dengan ID: {id} tidak ditemukan");
            }
            var swordReadDTO = _mapper.Map<SwordReadDTO>(result);
            return swordReadDTO;
        }

        [HttpGet("SearchSword")]
        public async Task<IActionResult> List(string name)
        {
            var products = await _swordDAL.SearchByName(name);
            if (products != null)
            {
                var productReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(products);
                return Ok(productReadDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("SortLightestSword")]
        public async Task<IEnumerable<SwordReadDTO>> GetLightest()
        {

            var results = await _swordDAL.OrderFromLightest();
            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

        [HttpGet("SortHeaviestSword")]
        public async Task<IEnumerable<SwordReadDTO>> GetHeaviest()
        {
            var results = await _swordDAL.OrderFromHeaviest();
            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

        [HttpPost("WithType")]
        public async Task<ActionResult> PostWithSword(SwordWithTypeDTO swordWithTypeDTO)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(swordWithTypeDTO);
                var result = await _swordDAL.InsertWithType(newSword);
                var swordDto = _mapper.Map<SwordWithTypeDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, swordDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post(SwordCreate2DTO swordCreateDTO)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(swordCreateDTO);
                var result = await _swordDAL.Insert(newSword);
                var swordDTO = _mapper.Map<SwordCreate2DTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, swordDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertExistingElement")]
        public async Task<ActionResult> PostExElement(int swordId, int elementId)
        {
            try
            {
                await _swordDAL.InsertExistingElement(swordId, elementId);
                return Ok("Element sukses ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(SwordUpdateDTO swordUpdateDTO, int id)
        {
            try
            {
                var updateSword = new Sword
                {
                    Id = id,
                    Name = swordUpdateDTO.Name,
                    Weight = swordUpdateDTO.Weight,
                };

                var result = await _swordDAL.Update(updateSword);
                return Ok(swordUpdateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _swordDAL.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteElement/{id}")]
        public async Task<ActionResult> DeleteElement(int id)
        {
            try
            {
                await _swordDAL.DeleteElement(id);
                return Ok($"Element dengan id {id} success didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
