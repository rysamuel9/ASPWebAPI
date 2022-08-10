using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class QuotesDAL : IQuote
    {
        private readonly SamuraiContext _context;
        public QuotesDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteQuote = await _context.Quotes.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteQuote == null)
                {
                    throw new Exception($"Quote dengan id {id} tidak ditemukan");
                }
                _context.Quotes.Remove(deleteQuote);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Quote>> GetAll()
        {
            try
            {
                var quotes = await _context.Quotes.OrderBy(q => q.Text).ToListAsync();
                return quotes;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Quote> GetById(int id)
        {
            try
            {
                var result = await _context.Quotes.FirstOrDefaultAsync(s => s.Id == id);
                if (result == null)
                {
                    throw new Exception($"Quote dengan id {id} tidak ditemukan");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

        }

        public async Task<Quote> Insert(Quote obj)
        {
            try
            {
                var newQuote = new Quote
                {
                    Text = obj.Text,
                    SamuraiId = obj.SamuraiId
                };

                _context.Quotes.Add(newQuote);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Quote>> SearchQuote(string text)
        {
            try
            {
                var quotes = await _context.Quotes.Where(q => q.Text.Contains(text)).ToListAsync();
                if (quotes == null)
                {
                    throw new Exception($"Quote {text} tidak ditemukan");
                }
                return quotes;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Quote> Update(Quote obj)
        {
            try
            {
                var updateQuote = await _context.Quotes.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateQuote == null)
                    throw new Exception($"Quote dengan id {obj.Id} tidak ditemukan");
                updateQuote.Text = obj.Text;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
