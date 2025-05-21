using MediatR;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetNextCorrelativeEmployeeQuery
{
    public sealed record GetNextCorrelativeEmployeeQuery : IRequest<Response<string>>
    {
    }
}
