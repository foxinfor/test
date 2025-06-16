using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class CarsRepository : Repository<Car>, ICarRepository
    {
        public CarsRepository(ApplicationContext context) : base(context) { }
    }
}
