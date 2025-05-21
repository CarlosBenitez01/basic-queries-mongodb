using MediatR;
using Unab.Practice.Employees.Dto.Enums;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Commands.CreateEmployeeCommand
{
    public sealed record CreateEmployeeCommand : IRequest<Response<bool>>
    {
        public string Code { get; set; } = string.Empty;
        public string Dui { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SexDto Sex { get; set; } = SexDto.MAN;
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
