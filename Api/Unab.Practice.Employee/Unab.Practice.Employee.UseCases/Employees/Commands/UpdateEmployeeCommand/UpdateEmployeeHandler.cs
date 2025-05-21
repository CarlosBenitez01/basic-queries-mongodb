using AutoMapper;
using FluentValidation;
using MediatR;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Commands.UpdateEmployeeCommand
{
    internal sealed class UpdateEmployeeHandler: IRequestHandler<UpdateEmployeeCommand, Response<bool>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateEmployeeCommand> _validator;
        public UpdateEmployeeHandler(IEmployeeRepository repository, IMapper mapper, IValidator<UpdateEmployeeCommand> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Response<bool>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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
                var employee = _mapper.Map<Domain.Entities.Employee>(request);
                response.Data = await _repository.UpdateAsync(employee, cancellationToken);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "¡Empleado Actualizado Exitosamente!";
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
