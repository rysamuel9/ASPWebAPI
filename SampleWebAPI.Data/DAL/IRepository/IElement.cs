using SampleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Data.DAL.IRepository
{
    public interface IElement : ICrud<Element>
    {
        public Task<IEnumerable<Element>> SearchByName(string name);
        Task InsertExistingSword(int elementId, int swordId);
    }
}
