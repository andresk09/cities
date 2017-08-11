using OpLab.API.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpLab.API.Domain.Entities;
using OpLab.API.Infrastructure.Data.Context;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpLab.API.Infrastructure.Data.Repositories
{
    public class PointOfInterestRepository : IPointOfInterestRepository
    {
        private readonly PointsOfInterestContext _context;
        private readonly IMapper _mapper;
        public PointOfInterestRepository(PointsOfInterestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(PointOfInterest entity)
        {
            var model = _mapper.Map<Models.PointOfInterest>(entity);
            _context.PointsOfInterest.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<PointOfInterest> Get(int cityId, int id)
        {
            var model = _context.PointsOfInterest.Where(p => p.CityId == cityId && p.Id == id).FirstOrDefault();
            var entity = _mapper.Map<PointOfInterest>(model);

            return entity;
        }

        public async Task<IEnumerable<PointOfInterest>> GetForCity(int cityId)
        {
            var models = _context.PointsOfInterest.Where(p => p.CityId == cityId).ToList();
            var entities = _mapper.Map<List<PointOfInterest>>(models);

            return entities;
        }

        public async Task Remove(int id)
        {
            var model = await _context.PointsOfInterest.FindAsync(id);
            _context.Entry(model).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task Update(PointOfInterest entity)
        {
            var model = _mapper.Map<Models.PointOfInterest>(entity);
            _context.Entry(model).Property(e => e.Name).IsModified = true;
            _context.Entry(model).Property(e => e.Description).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
