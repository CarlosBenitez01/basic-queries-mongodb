using FluentValidation;
using MongoDB.Bson;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByIdEmployeeQuery
{
    public sealed class GetByIdEmployeeValidator: AbstractValidator<GetByIdEmployeeQuery>
    {
        public GetByIdEmployeeValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("El campo de Id es requerido.")
                .Must(x => ObjectId.TryParse(x, out ObjectId id)).WithMessage("El id no tiene un formato válido");
        }
    }
}
