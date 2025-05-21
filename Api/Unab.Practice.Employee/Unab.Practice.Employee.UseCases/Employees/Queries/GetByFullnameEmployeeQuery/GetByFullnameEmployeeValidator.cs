using FluentValidation;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByFullnameEmployeeQuery
{
    public sealed class GetByFullnameEmployeeValidator: AbstractValidator<GetByFullnameEmployeeQuery>
    {
        public GetByFullnameEmployeeValidator()
        {
            RuleFor(x => x.Fullname)
                .NotNull().NotEmpty().WithMessage("El campo de Nombre es requerido.")
                .MaximumLength(102).WithMessage("El campo de Nombre no puede exceder los 102 caracteres.");
        }
    }
}
