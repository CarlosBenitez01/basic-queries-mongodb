using FluentValidation;
using MongoDB.Bson;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByCodeEmployeeQuery
{
    public sealed class GetByCodeEmployeeValidator: AbstractValidator<GetByCodeEmployeeQuery>
    {
        public GetByCodeEmployeeValidator()
        {
            RuleFor(x => x.Code)
                .NotNull().NotEmpty().WithMessage("El campo de correlativo es requerido.")
                .MaximumLength(10).WithMessage("El campo de correlativo no puede exceder los 10 caracteres.");
        }
    }
}
