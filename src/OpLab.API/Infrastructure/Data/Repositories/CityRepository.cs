using AutoMapper;
using OpLab.API.Domain.Repositories;
using OpLab.API.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpLab.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OpLab.API.Infrastructure.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly CitiesContext _context;
        private readonly IMapper _mapper;

        public CityRepository(CitiesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<City> Get(int id)
        {
            var model = await _context.Cities.FindAsync(id);
            var entity = _mapper.Map<City>(model);

            return entity;
        }

        public async Task<IEnumerable<City>> Get()
        {            
            List<Models.City> models = _context.Cities.ToList();
            List<City> entities = _mapper.Map<List<City>>(models);

            return entities;
        }

        public async Task Update(City entity)
        {
            var model = _mapper.Map<Models.City>(entity);
            _context.Entry(model).Property(e => e.Name).IsModified = true;
            _context.Entry(model).Property(e => e.Description).IsModified = true;
            await _context.SaveChangesAsync();
        }
        
        public async Task Add(City entity)
        {
            var model = _mapper.Map<Models.City>(entity);
            _context.Cities.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var model = await _context.Cities.FindAsync(id);
            _context.Entry(model).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
