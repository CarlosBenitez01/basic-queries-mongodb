using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByFullnameEmployeeQuery
{
    public sealed record GetByFullnameEmployeeQuery : IRequest<Response<IEnumerable<EmployeeDto>>>
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
