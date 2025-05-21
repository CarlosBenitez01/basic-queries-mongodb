using MediatR;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetAllEmployeeQuery
{
    public sealed record GetAllEmployeeQuery : IRequest<Response<IEnumerable<EmployeeDto>>>
    {
    }
}
