using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByDuiEmployeeQuery
{
    public sealed record GetByDuiEmployeeQuery : IRequest<Response<EmployeeDto>>
    {
        public string Dui { get; set; } = string.Empty;
    }
}
