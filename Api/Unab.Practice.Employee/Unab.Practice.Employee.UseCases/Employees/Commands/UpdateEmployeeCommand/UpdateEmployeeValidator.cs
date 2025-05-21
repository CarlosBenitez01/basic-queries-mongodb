using FluentValidation;
using MongoDB.Bson;

namespace Unab.Practice.Employees.UseCases.Employees.Commands.UpdateEmployeeCommand
{
    public sealed class UpdateEmployeeValidator: AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("El campo de Id es requerido.")
                .Must(x => ObjectId.TryParse(x, out ObjectId id)).WithMessage("El id no tiene un formato válido");
            RuleFor(x => x.Dui)
                .NotNull().NotEmpty().WithMessage("El campo de DUI es requerido.")
                .Matches(@"^\d{8}-\d{1}").WithMessage("El campo de DUI no es válido.")
                .MaximumLength(10).WithMessage("El campo de DUI no puede exceder los 10 caracteres.");
            RuleFor(x => x.FirstName)
                .NotNull().NotEmpty().WithMessage("El campo de Nombre es requerido.")
                .MaximumLength(50).WithMessage("El campo de Nombre no puede exceder los 50 caracteres.");
            RuleFor(x => x.LastName)
                .NotNull().NotEmpty().WithMessage("El campo de Apellido es requerido.")
                .MaximumLength(50).WithMessage("El campo de Apellido no puede exceder los 50 caracteres.");

            RuleFor(x => x.Tel)
                .NotNull().NotEmpty().WithMessage("El campo de Teléfono es requerido.")
                .Matches(@"^\d{4}-\d{4}$").WithMessage("El campo de Teléfono no es válido.")
                .MaximumLength(9).WithMessage("El campo de Teléfono no puede exceder los 9 caracteres.");
            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("El campo de Correo Electrónico no es válido.")
                .MaximumLength(50).WithMessage("El campo de Correo Electrónico no puede exceder los 50 caracteres.");

            RuleFor(x => x.Sex)
                .NotNull().NotEmpty().WithMessage("El campo de Sexo es requerido.")
                .IsInEnum().WithMessage("El campo de Sexo no es válido.");
            RuleFor(x => x.DateOfBirth)
                .NotNull().NotEmpty().WithMessage("El campo de Fecha de Nacimiento es requerido.")
                .LessThan(DateTime.Now).WithMessage("La Fecha de Nacimiento no puede ser mayor a la fecha actual.")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("La Fecha de Nacimiento no puede ser menor a 100 años atrás.");

            RuleFor(x => x.HireDate)
                .NotNull().NotEmpty().WithMessage("El campo de Fecha de Contratación es requerido.")
                .LessThan(DateTime.Now).WithMessage("La Fecha de Contratación no puede ser mayor a la fecha actual.")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("La Fecha de Contratación no puede ser menor a 100 años atrás.")
                .GreaterThan(x => x.DateOfBirth).WithMessage("La Fecha de Contratación no puede ser menor a la Fecha de Nacimiento.");

            RuleFor(x => x.DismissalDate)
                .GreaterThan(x => x.HireDate).When(x => x.DismissalDate.HasValue).WithMessage("La Fecha de Despido no puede ser menor a la Fecha de Contratación.");

            RuleFor(x => x.Salary)
                .NotNull().NotEmpty().WithMessage("El campo de Salario es requerido.")
                .GreaterThan(0).WithMessage("El campo de Salario no puede ser menor o igual a 0.");

            RuleFor(x => x.Address)
                .NotNull().NotEmpty().WithMessage("El campo de Dirección es requerido.")
                .MaximumLength(255).WithMessage("El campo de Dirección no puede exceder los 255 caracteres.");
        }
    }
}
