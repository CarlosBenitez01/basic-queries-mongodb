using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Unab.Practice.Employees.Domain.Common
{
    public class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
