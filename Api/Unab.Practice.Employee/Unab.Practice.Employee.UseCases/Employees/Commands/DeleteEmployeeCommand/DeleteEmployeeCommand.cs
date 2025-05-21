using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Commands.DeleteEmployeeCommand
{
    public sealed record DeleteEmployeeCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; } = string.Empty;
    }
}
