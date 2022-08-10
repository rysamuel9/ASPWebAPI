using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Data.DAL.Pagination;
using SampleWebAPI.Domain;
using SampleWebAPI.Helpers;
using SampleWebAPI.Helpers.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SamuraiDAL : ISamurai
    {
        private readonly SamuraiContext _context;
        public SamuraiDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSamurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSamurai == null)
                    throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
                _context.Samurais.Remove(deleteSamurai);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            var results = await _context.Samurais.OrderBy(s => s.Name).ToListAsync();
            return results;
        }

        public async Task<Samurai> GetById(int id)
        {
            var result = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Samurai>> GetByName(string name)
        {
            var samurais = await _context.Samurais.Where(s => s.Name.Contains(name))
                .OrderBy(s => s.Name).ToListAsync();
            return samurais;
        }

        public async Task<IEnumerable<Samurai>> GetSamuraiAllProp(PaginationParams @params)
        {
            var results = await _context.Samurais
                .Include(s => s.Swords)
                .ThenInclude(s => s.TypeSword)
                .Include(s => s.Swords)
                .ThenInclude(s => s.Elements)
                .OrderBy(c => c.Name)
                .Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage)
                .ToArrayAsync();
            return results;
        }

        public async Task<IEnumerable<Samurai>> GetSamuraiWithQuotes()
        {
            var samurais = await _context.Samurais.Include(s => s.Quotes)
                .OrderBy(s => s.Name).AsNoTracking().ToListAsync();
            return samurais;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            try
            {
                _context.Samurais.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        //public async Task<Samurai> InsertAll(Samurai obj)
        //{
        //    try
        //    {
        //        _context.Samurais.Add(obj);
        //        await _context.SaveChangesAsync();
        //        return obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"{ex.Message}");
        //    }
        //}


        public async Task<Samurai> Update(Samurai obj)
        {
            try
            {
                var updateSamurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSamurai == null)
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");

                updateSamurai.Name = obj.Name;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        /*
        public async Task<Samurai> GetAllPaging(PaginationFilter filter)
        {
            PagedResponse<List<Samurai>> pagedResponse;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.Samurais
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var totalRecords = await _context.Samurais.CountAsync();
            //return Ok(new PagedResponse<List<Product>>(pagedData, validFilter.PageNumber, validFilter.PageSize, totalRecords));

            return pagedResponse.TotalRecords;
        }
         */
    }
}
