using TNO.Entities;

namespace TNO.DAL.Services;

public interface IReportService : IBaseService<Report, int>
{
    IEnumerable<Report> FindAll();
}