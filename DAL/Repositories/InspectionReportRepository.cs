using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class InspectionReportRepository : Repository<InspectionReport>, IInspectionReportRepository
    {
        public InspectionReportRepository(ApplicationContext context) : base(context) { }
    }
}