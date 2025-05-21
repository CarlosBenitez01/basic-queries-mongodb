using MongoDB.Bson;
using Unab.Practice.Employees.Domain.Entities;

namespace Unab.Practice.Employees.Interfaces.Persistence
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellation);
        Task<bool> AddAsync(Employee employee, CancellationToken cancellation);
        Task<bool> UpdateAsync(Employee employee, CancellationToken cancellation);
        Task<bool> DeleteAsync(ObjectId id, CancellationToken cancellation);
        Task<Employee> GetByIdAsync(ObjectId id, CancellationToken cancellation);
        Task<Employee> GetByCodeAsync(string code, CancellationToken cancellation);
        Task<Employee> GetByDuiAsync(string code, CancellationToken cancellation);
        Task<IEnumerable<Employee>> FindByFullNameAsync( string fullname, CancellationToken cancellation);
        Task<int> CountAsync(CancellationToken cancellation);
    }
}
