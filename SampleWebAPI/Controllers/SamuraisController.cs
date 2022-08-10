using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Data.DAL.Pagination;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Samurai;
using SampleWebAPI.Helpers;
using SampleWebAPI.Helpers.Wrapper;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samuraiDAL;
        private readonly IMapper _mapper;
        public SamuraisController(ISamurai samuraiDAL, IMapper mapper)
        {
            _samuraiDAL = samuraiDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SamuraiReadDTO>> Get()
        {
            //List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();
            /*foreach (var result in results)
           {
               samuraiDTO.Add(new SamuraiReadDTO
               {
                   Id = result.Id,
                   Name = result.Name
               });
           }*/
            var results = await _samuraiDAL.GetAll();
            var samuraiDTO = _mapper.Map<IEnumerable<SamuraiReadDTO>>(results);

            return samuraiDTO;
        }

        [HttpGet("{id}")]
        public async Task<SamuraiReadDTO> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<SamuraiReadDTO>(result);

            return samuraiDTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<SamuraiReadDTO>> GetByName(string name)
        {
            List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();
            var results = await _samuraiDAL.GetByName(name);
            foreach (var result in results)
            {
                samuraiDTO.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDTO;
        }

        [HttpGet("WithQuotes")]
        public async Task<IEnumerable<SamuraiWithQuotesDTO>> GetSamuraiWithQuote()
        {
            var results = await _samuraiDAL.GetSamuraiWithQuotes();
            var samuraiWithQuoteDtos = _mapper.Map<IEnumerable<SamuraiWithQuotesDTO>>(results);
            return samuraiWithQuoteDtos;
        }

        [HttpGet("SamuraiAllPropPaging")]
        public async Task<IEnumerable<SamuraiCreateAllPropDTO>> GetAllPaging([FromQuery] PaginationParams @params)
        {
            var results = await _samuraiDAL.GetSamuraiAllProp(@params);
            var samuraiReadDTO = _mapper.Map<IEnumerable<SamuraiCreateAllPropDTO>>(results);
            return samuraiReadDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SamuraiCreateDTO samuraiCreateDto)
        {
            try
            {
                var newSamurai = _mapper.Map<Samurai>(samuraiCreateDto);
                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDTO = _mapper.Map<SamuraiReadDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("SamuraiAllProp")]
        //public async Task<ActionResult> PostSamurai(SamuraiCreateAllPropDTO samuraiCreateAllProp)
        //{
        //    try
        //    {
        //        var newSamurai = _mapper.Map<Samurai>(samuraiCreateAllProp);
        //        var result = await _samuraiDAL.Insert(newSamurai);
        //        var samuraiReadDTO = _mapper.Map<SamuraiCreateAllPropDTO>(result);

        //        return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("WithSwords")]
        public async Task<ActionResult> Post(SamuraiWithSwordDTO samuraiWithSwordDTO)
        {
            try
            {
                var newSamurai = _mapper.Map<Samurai>(samuraiWithSwordDTO);
                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDto = _mapper.Map<SamuraiWithSwordDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(SamuraiReadDTO samuraiDto)
        {
            try
            {
                var updateSamurai = new Samurai
                {
                    Id = samuraiDto.Id,
                    Name = samuraiDto.Name
                };
                var result = await _samuraiDAL.Update(updateSamurai);
                return Ok(samuraiDto);
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
                await _samuraiDAL.Delete(id);
                return Ok($"Data samurai dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
