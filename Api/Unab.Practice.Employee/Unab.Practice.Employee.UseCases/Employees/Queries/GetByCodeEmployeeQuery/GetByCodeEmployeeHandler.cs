using AutoMapper;
using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByCodeEmployeeQuery
{
    internal sealed class GetByCodeEmployeeHandler: IRequestHandler<GetByCodeEmployeeQuery, Response<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetByCodeEmployeeQuery> _validator;
        public GetByCodeEmployeeHandler(IEmployeeRepository repository, IMapper mapper, IValidator<GetByCodeEmployeeQuery> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Response<EmployeeDto>> Handle(GetByCodeEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<EmployeeDto>();
            try
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    response.Errors = validationResult.Errors.Select(x => new BaseError { PropertyMessage = x.PropertyName, ErrorMessage = x.ErrorMessage }).ToList();
                    response.IsSuccess = false;
                    return response;
                }
                var employee = await _repository.GetByCodeAsync(request.Code, cancellationToken);
                response.Data = _mapper.Map<EmployeeDto>(employee);
                if (response.Data is not null)
                {
                    response.IsSuccess = true;
                    response.Message = "¡Consulta Existosa!";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "¡Registro no encontrado!";
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
