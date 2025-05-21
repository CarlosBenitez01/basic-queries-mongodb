using AutoMapper;
using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Commands.DeleteEmployeeCommand
{
    internal sealed class DeleteEmployeeHandler: IRequestHandler<DeleteEmployeeCommand, Response<bool>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IValidator<DeleteEmployeeCommand> _validator;
        public DeleteEmployeeHandler(IEmployeeRepository repository, IValidator<DeleteEmployeeCommand> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Response<bool>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            try
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    response.Errors = validationResult.Errors.Select(x => new BaseError { PropertyMessage = x.PropertyName, ErrorMessage = x.ErrorMessage }).ToList();
                    response.IsSuccess = false;
                    return response;
                }
                response.Data = await _repository.DeleteAsync(ObjectId.Parse(request.Id), cancellationToken);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "¡Empleado Eliminado Exitosamente!";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = "Error interno";
                response.Errors.Add(new BaseError { ErrorMessage = ex.Message});
                return response;
            }
        }
    }
}
