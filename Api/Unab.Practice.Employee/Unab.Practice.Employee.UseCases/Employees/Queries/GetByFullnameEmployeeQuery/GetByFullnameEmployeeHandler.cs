using AutoMapper;
using FluentValidation;
using MediatR;
using MongoDB.Bson;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetByFullnameEmployeeQuery
{
    internal sealed class GetByFullnameEmployeeHandler: IRequestHandler<GetByFullnameEmployeeQuery, Response<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<GetByFullnameEmployeeQuery> _validator;
        public GetByFullnameEmployeeHandler(IEmployeeRepository repository, IMapper mapper, IValidator<GetByFullnameEmployeeQuery> validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<Response<IEnumerable<EmployeeDto>>> Handle(GetByFullnameEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<IEnumerable<EmployeeDto>>();
            try
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    response.Errors = validationResult.Errors.Select(x => new BaseError { PropertyMessage = x.PropertyName, ErrorMessage = x.ErrorMessage }).ToList();
                    response.IsSuccess = false;
                    return response;
                }
                var employee = await _repository.FindByFullNameAsync(request.Fullname, cancellationToken);
                response.Data = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
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
