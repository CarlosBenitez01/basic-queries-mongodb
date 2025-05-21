using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Text.RegularExpressions;
using Unab.Practice.Employees.Domain.Entities;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Persistence.Contexts;

namespace Unab.Practice.Employees.Persistence.Repositories
{
    internal sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoDatabase _database;
        public EmployeeRepository(MongoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _database = context.GetDatabase();
        }
        public async Task<bool> AddAsync(Employee employee, CancellationToken cancellation)
        {
            //conectado a base de datos
            var collection = _database.GetCollection<Employee>("Employees");
            employee.IsDeleted = false;
            employee.IsActive = true;
            employee.CreatedAt = DateTime.Now;
            employee.UpdatedAt = null;
            await collection.InsertOneAsync(employee, new InsertOneOptions(), cancellation);
            return true;
        }

        public async Task<int> CountAsync(CancellationToken cancellation)
        {
            var collection = _database.GetCollection<Employee>("Employees");
            var count = collection.AsQueryable().Count();
            return count;
        }

        public async Task<bool> DeleteAsync(ObjectId id, CancellationToken cancellation)
        {
            //conectado a base de datos
            var collection = _database.GetCollection<Employee>("Employees");
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            var update = Builders<Employee>.Update
                .Set(x => x.IsDeleted, true);
            var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions(), cancellation);
            return result.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Employee>> FindByFullNameAsync(string fullname, CancellationToken cancellation)
        {
            //conectado a base de datos
            var collection = _database.GetCollection<Employee>("Employees");
            var secureFullname = Regex.Escape(fullname);
            var filter = Builders<Employee>.Filter
                .Regex(x => x.FirstName, new BsonRegularExpression(secureFullname, "i"));
            filter |= Builders<Employee>.Filter
                .Regex(x => x.LastName, new BsonRegularExpression(secureFullname, "i"));
            filter &= Builders<Employee>.Filter
                .Eq(x => x.IsDeleted, false);

            var employee = await collection.Find(filter).ToListAsync(cancellation);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellation)
        {
            //conectado a base de datos
            var collection = _database.GetCollection<Employee>("Employees");
            var filter = Builders<Employee>.Filter.Eq(x => x.IsDeleted, false);

            var employees = await collection.FindAsync(filter, new FindOptions<Employee>(), cancellation);
            return await employees.ToListAsync(cancellation);
        }

        public async Task<Employee> GetByCodeAsync(string code, CancellationToken cancellation)
        {
            var collection = _database.GetCollection<Employee>("Employees");
            var filter = Builders<Employee>.Filter
                .Eq(x => x.Code, code);
            filter &= Builders<Employee>.Filter
                .Eq(x => x.IsDeleted, false);

            var employee = await collection.Find(filter).FirstOrDefaultAsync(cancellation);
            return employee;
        }

        public async Task<Employee> GetByDuiAsync(string dui, CancellationToken cancellation)
        {
            var collection = _database.GetCollection<Employee>("Employees");
            var filter = Builders<Employee>.Filter
                .Eq(x => x.Dui, dui);
            filter &= Builders<Employee>.Filter
                .Eq(x => x.IsDeleted, false);

            var employee = await collection.Find(filter).FirstOrDefaultAsync(cancellation);
            return employee;
        }

        public async Task<Employee> GetByIdAsync(ObjectId id, CancellationToken cancellation)
        {
            //conectado a base de datos
            var collection = _database.GetCollection<Employee>("Employees");
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, id);
            filter &= Builders<Employee>.Filter.Eq(x => x.IsDeleted, false);

            var employee = await collection.Find(filter).FirstOrDefaultAsync(cancellation);
            return employee;
        }

        public async Task<bool> UpdateAsync(Employee employee, CancellationToken cancellation)
        {
            var collection = _database.GetCollection<Employee>("Employees");
            var filter = Builders<Employee>.Filter.Eq(x => x.Id, employee.Id);
            var update = Builders<Employee>.Update
                .Set(x => x.Dui, employee.Dui)
                .Set(x => x.Nit, employee.Nit)
                .Set(x => x.FirstName, employee.FirstName)
                .Set(x => x.LastName, employee.LastName)
                .Set(x => x.Email, employee.Email)
                .Set(x => x.Sex, employee.Sex)
                .Set(x => x.DateOfBirth, employee.DateOfBirth)
                .Set(x => x.HireDate, employee.HireDate)
                .Set(x => x.DismissalDate, employee.DismissalDate)
                .Set(x => x.Salary, employee.Salary)
                .Set(x => x.TravelExpenses, employee.TravelExpenses)
                .Set(x => x.Address, employee.Address)
                .Set(x => x.UpdatedAt, DateTime.Now)
                .Set(x => x.IsActive, employee.IsActive);



            var result = await collection.UpdateOneAsync(filter, update, new UpdateOptions(), cancellation);
            return result.ModifiedCount > 0;
        }
    }
}
