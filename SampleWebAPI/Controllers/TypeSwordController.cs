using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.TypeSword;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeSwordController : ControllerBase
    {
        private readonly ITypeSword _typeSwordDAL;
        private readonly IMapper _mapper;
        private readonly SamuraiContext _context;
        public TypeSwordController(ITypeSword typeSwordDAL, IMapper mapper, SamuraiContext context)
        {
            _typeSwordDAL = typeSwordDAL;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<TypeReadDTO>> Get()
        {

            var results = await _typeSwordDAL.GetAll();
            var typeSwordDTO = _mapper.Map<IEnumerable<TypeReadDTO>>(results);

            return typeSwordDTO;
        }

        [HttpGet("{id}")]
        public async Task<TypeReadDTO> Get(int id)
        {
            var result = await _typeSwordDAL.GetById(id);
            if (result == null) throw new Exception($"type sword dengan {id} tidak ditemukan");
            var typeSwordDTO = _mapper.Map<TypeReadDTO>(result);

            return typeSwordDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TypeCreateDTO typeCreateDTO)
        {
            try
            {
                var newTypeSword = _mapper.Map<TypeSword>(typeCreateDTO);
                var result = await _typeSwordDAL.Insert(newTypeSword);
                var typeSwordDTO = _mapper.Map<TypeReadDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, typeSwordDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(TypeUpdateDTO typeUpdateDTO, int id)
        {
            try
            {
                var updateTypeSword = new TypeSword
                {
                    Id = id,
                    TypeName = typeUpdateDTO.TypeName
                };
                var result = await _typeSwordDAL.Update(updateTypeSword);
                return Ok(typeUpdateDTO);
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
                await _typeSwordDAL.Delete(id);
                return Ok($"Type sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
