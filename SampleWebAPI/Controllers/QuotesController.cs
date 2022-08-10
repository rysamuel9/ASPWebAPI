
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO.Quote;
using SampleWebAPI.Helpers;

namespace SampleWebAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuote _quoteDAL;
        private readonly IMapper _mapper;
        public QuotesController(IQuote quoteDAL, IMapper mapper)
        {
            _quoteDAL = quoteDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<QuoteReadDTO>> Get()
        {
            var results = await _quoteDAL.GetAll();
            var quotes = _mapper.Map<IEnumerable<QuoteReadDTO>>(results);

            return quotes;
        }

        [HttpGet("{id:int}")]
        public async Task<QuoteReadDTO> Get(int id)
        {
            var result = await _quoteDAL.GetById(id);
            if (result == null)
            {
                throw new Exception($"Quote dengan ID: {id} tidak ditemukan");
            }
            var quoteRaeadDTO = _mapper.Map<QuoteReadDTO>(result);
            return quoteRaeadDTO;
        }

        [HttpGet("SearchQuotes")]
        public async Task<IActionResult> List(string text)
        {
            var products = await _quoteDAL.SearchQuote(text);
            if (products != null)
            {
                var productReadDTO = _mapper.Map<IEnumerable<QuoteReadDTO>>(products);
                return Ok(productReadDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(QuoteCreateDTO quoteCreateDTO)
        {
            try
            {
                var newQuote = _mapper.Map<Quote>(quoteCreateDTO);
                var result = await _quoteDAL.Insert(newQuote);
                var quoteReadDTO = _mapper.Map<QuoteCreateDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, quoteReadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(QuoteUpdateDTO quoteUpdateDTO, int id)
        {
            try
            {
                var updateQuote = new Quote
                {
                    Id = id,
                    Text = quoteUpdateDTO.Text,
                };

                var result = await _quoteDAL.Update(updateQuote);
                return Ok(quoteUpdateDTO);
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
                await _quoteDAL.Delete(id);
                return Ok($"Quote dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}