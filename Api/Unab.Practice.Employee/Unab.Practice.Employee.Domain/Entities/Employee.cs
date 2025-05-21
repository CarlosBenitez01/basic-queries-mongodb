using Unab.Practice.Employees.Domain.Common;
using Unab.Practice.Employees.Domain.Enums;

namespace Unab.Practice.Employees.Domain.Entities
{
    public sealed class Employee: BaseAuditableEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Dui { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; }
        public decimal TravelExpenses { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
