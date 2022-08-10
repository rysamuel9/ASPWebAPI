using SampleWebAPI.Data.DAL.Pagination;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL.IRepository
{
    public interface ISword : ICrud<Sword>
    {
        public Task<IEnumerable<Sword>> SearchByName(string name);
        public Task<IEnumerable<Sword>> OrderFromHeaviest();
        public Task<IEnumerable<Sword>> OrderFromLightest();
        public Task<Sword> InsertWithType(Sword obj);
        public Task InsertExistingElement(int swordId, int elementId);
        public Task DeleteElement(int id);
        public Task<IEnumerable<Sword>> PagingWithType(PaginationParams @params);
    }
}
