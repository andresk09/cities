using OpLab.API.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpLab.API.Domain.Repositories
{
    public interface ICityRepository
    {
        Task<City> Get(int id);

        Task<IEnumerable<City>> Get();

        Task Add(City entity);

        Task Update(City entity);

        Task Remove(int id);
    }
}
