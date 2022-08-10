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
    public class TypeSwordDAL : ITypeSword
    {
        private readonly SamuraiContext _context;
        public TypeSwordDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteTypeSword = await _context.TypeSwords.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteTypeSword == null)
                {
                    throw new Exception($"Type sword dengan id {id} tidak ditemukan");
                }
                _context.TypeSwords.Remove(deleteTypeSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<TypeSword>> GetAll()
        {
            var results = await _context.TypeSwords.OrderBy(t => t.TypeName).ToListAsync();
            return results;
        }

        public async Task<TypeSword> GetById(int id)
        {
            var result = await _context.TypeSwords.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Type sword dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<TypeSword> Insert(TypeSword obj)
        {
            try
            {
                _context.TypeSwords.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<TypeSword> Update(TypeSword obj)
        {
            try
            {
                var updateTypeSword = await _context.TypeSwords.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateTypeSword == null)
                {
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");
                }

                updateTypeSword.TypeName = obj.TypeName;
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
