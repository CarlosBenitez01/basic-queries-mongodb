using FluentValidation;
using MongoDB.Bson;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByDuiEmployeeQuery
{
    public sealed class GetByDuiEmployeeValidator: AbstractValidator<GetByDuiEmployeeQuery>
    {
        public GetByDuiEmployeeValidator()
        {
            RuleFor(x => x.Dui)
                .NotNull().NotEmpty().WithMessage("El campo de DUI es requerido.")
                .Matches(@"^\d{8}-\d{1}").WithMessage("El campo de DUI no es válido.")
                .MaximumLength(10).WithMessage("El campo de DUI no puede exceder los 10 caracteres.");
        }
    }
}
