using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data.DAL.IRepository;
using SampleWebAPI.Data.DAL.Pagination;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL
{
    public class SwordDAL : ISword
    {
        private readonly SamuraiContext _context;
        public SwordDAL(SamuraiContext context)
        {
            _context = context;
        }



        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                {
                    throw new Exception($"Data sword dengan id {id} tidak ditemukan");
                }
                _context.Swords.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task DeleteElement(int id)
        {
            var elemet = await _context.Swords.Include(b => b.Elements).FirstOrDefaultAsync(s => s.Id == id);
            var sword = elemet.Elements[0];
            elemet.Elements.Remove(sword);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sword>> GetAll()
        {
            try
            {
                var results = await _context.Swords.OrderBy(s => s.Weight).ToListAsync();
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> GetById(int id)
        {
            try
            {
                var result = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
                if (result == null)
                {
                    throw new Exception($"Data sword dengan id {id} tidak ditemukan");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> Insert(Sword obj)
        {
            try
            {
                await _context.Swords.AddAsync(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task InsertExistingElement(int swordId, int elementId)
        {

            var swordData = await _context.Swords.FirstOrDefaultAsync(s => s.Id == swordId);
            var elementData = await _context.Elements.FirstOrDefaultAsync(e => e.Id == elementId);

            if (swordData == null && elementData == null)
            {
                throw new Exception("ID NOT FOUND!");
            }

            swordData.Elements.Add(elementData);
            await _context.SaveChangesAsync(); ;
        }

        public async Task<Sword> InsertWithType(Sword obj)
        {
            try
            {
                await _context.Swords.AddAsync(obj);
                await _context.SaveChangesAsync();
                return obj;

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Sword>> OrderFromHeaviest()
        {
            try
            {
                var products = await _context.Swords.OrderByDescending(s => s.Weight).ToArrayAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Sword>> OrderFromLightest()
        {
            try
            {
                var products = await _context.Swords.OrderBy(s => s.Weight).ToArrayAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Sword>> PagingWithType(PaginationParams @params)
        {
            var results = await _context.Swords
                .Include(s => s.TypeSword)
                .OrderBy(s => s.Name)
                .Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage)
                .ToArrayAsync();
            return results;
        }

        public async Task<IEnumerable<Sword>> SearchByName(string name)
        {
            try
            {
                var swords = await _context.Swords.Where(p => p.Name.Contains(name)).ToListAsync();
                if (swords == null)
                {
                    throw new Exception($"Data {name} tidak ditemukan");
                }
                return swords;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> Update(Sword obj)
        {
            try
            {
                var updateSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSword == null)
                    throw new Exception($"Data sword dengan id {obj.Id} tidak ditemukan");
                updateSword.Name = obj.Name;
                updateSword.Weight = obj.Weight;
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
