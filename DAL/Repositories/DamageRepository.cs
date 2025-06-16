using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class DamageRepository : Repository<Damage>, IDamageRepository
    {
        public DamageRepository(ApplicationContext context) : base(context)  { }
    }
}