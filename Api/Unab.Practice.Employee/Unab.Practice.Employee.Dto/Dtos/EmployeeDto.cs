using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Enums;

namespace Unab.Practice.Employees.Dto.Dtos
{
    public sealed record EmployeeDto
    {
        public string Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Dui { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public SexDto Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
