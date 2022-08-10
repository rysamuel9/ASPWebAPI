using SampleWebAPI.Data.DAL.Pagination;
using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL.IRepository
{
    public interface ISamurai : ICrud<Samurai>
    {
        Task<IEnumerable<Samurai>> GetByName(string name);
        Task<IEnumerable<Samurai>> GetSamuraiWithQuotes();
        public Task<IEnumerable<Samurai>> GetSamuraiAllProp(PaginationParams @params);
    }
}
