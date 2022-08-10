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
    public class ElementDAL : IElement
    {
        private readonly SamuraiContext _context;
        public ElementDAL(SamuraiContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteElement = await _context.Elements.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteElement == null)
                {
                    throw new Exception($"Data Element dengan id {id} tidak ditemukan");
                }

                _context.Elements.Remove(deleteElement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Element>> GetAll()
        {
            var results = await _context.Elements.OrderBy(e => e.Name).ToListAsync();
            return results;
        }

        public async Task<Element> GetById(int id)
        {
            var result = await _context.Elements.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null)
            {
                throw new Exception($"Data Element dengan id {id} tidak ditemukan");
            }
            return result;
        }

        public async Task<Element> Insert(Element obj)
        {
            try
            {
                var newElements = new Element
                {
                    Name = obj.Name,
                };

                _context.Elements.Add(newElements);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task InsertExistingSword(int elementId, int swordId)
        {
            var swordData = await _context.Swords.FirstOrDefaultAsync(s => s.Id == swordId);
            var elementData = await _context.Elements.FirstOrDefaultAsync(e => e.Id == elementId);

            if (swordData == null && elementData == null)
            {
                throw new Exception("ID NOT FOUND!");
            }

            elementData.Swords.Add(swordData);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Element>> SearchByName(string name)
        {
            try
            {
                var swords = await _context.Elements.Where(p => p.Name.Contains(name)).ToListAsync();
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

        public async Task<Element> Update(Element obj)
        {
            try
            {
                var elementUpdate = await _context.Elements.FirstOrDefaultAsync(e => e.Id == obj.Id);
                if (elementUpdate == null)
                {
                    throw new Exception($"Data Element dengan id {obj.Id} tidak ditemukan");
                }
                elementUpdate.Name = obj.Name;
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
