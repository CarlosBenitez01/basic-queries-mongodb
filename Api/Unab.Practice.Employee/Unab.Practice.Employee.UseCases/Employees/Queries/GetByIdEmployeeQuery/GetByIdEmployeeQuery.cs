using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByIdEmployeeQuery
{
    public sealed record GetByIdEmployeeQuery : IRequest<Response<EmployeeDto>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
