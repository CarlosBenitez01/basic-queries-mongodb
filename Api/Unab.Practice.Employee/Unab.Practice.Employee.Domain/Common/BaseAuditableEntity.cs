namespace Unab.Practice.Employees.Domain.Common
{
    public class BaseAuditableEntity: BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
