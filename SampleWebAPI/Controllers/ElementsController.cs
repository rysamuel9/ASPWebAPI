using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Element;
using SampleWebAPI.Helpers;

namespace SampleWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ElementsController : ControllerBase
    {
        private readonly IElement _elementDAL;
        private readonly IMapper _mapper;
        public ElementsController(IElement swordDAL, IMapper mapper)
        {
            _elementDAL = swordDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ElementReadDTO>> GetAll()
        {
            var results = await _elementDAL.GetAll();
            var elementReadDTO = _mapper.Map<IEnumerable<ElementReadDTO>>(results);

            return elementReadDTO;
        }

        [HttpGet("{id}")]
        public async Task<ElementReadDTO> Get(int id)
        {
            var element = await _elementDAL.GetById(id);
            if (element == null)
            {
                throw new Exception($"Data Element dengan ID: {id} tidak ditemukan");
            }
            var elementReadDTO = _mapper.Map<ElementReadDTO>(element);
            return elementReadDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ElementDTO elementCreateDTO)
        {
            try
            {
                var newElement = _mapper.Map<Element>(elementCreateDTO);
                var result = await _elementDAL.Insert(newElement);
                var elementReadDTO = _mapper.Map<ElementDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, elementCreateDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ElementDTO elementUpdateDTO, int id)
        {
            try
            {
                var updateElement = new Element
                {
                    Id = id,
                    Name = elementUpdateDTO.Name,
                };

                var result = await _elementDAL.Update(updateElement);
                return Ok(elementUpdateDTO);
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
                await _elementDAL.Delete(id);
                return Ok($"Data Element dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByName")]
        public async Task<IEnumerable<ElementReadDTO>> Get(string name)
        {
            List<ElementReadDTO> elementReadDTO = new List<ElementReadDTO>();
            var elements = await _elementDAL.SearchByName(name);
            foreach (var element in elements)
            {
                elementReadDTO.Add(new ElementReadDTO
                {
                    Id = element.Id,
                    Name = element.Name,
                });
            }

            return elementReadDTO;
        }


        [HttpPost("InsertExistingSword")]
        public async Task<ActionResult> PostExSword(int elementId, int swordId)
        {
            try
            {
                await _elementDAL.InsertExistingSword(elementId, swordId);
                return Ok("Element sukses ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
