using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByCodeEmployeeQuery
{
    public sealed record GetByCodeEmployeeQuery : IRequest<Response<EmployeeDto>>
    {
        public string Code { get; set; } = string.Empty;
    }
}
