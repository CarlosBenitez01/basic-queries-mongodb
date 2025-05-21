using AutoMapper;
using MediatR;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetAllEmployeeQuery
{
    internal sealed class GetAllEmployeeHandler: IRequestHandler<GetAllEmployeeQuery, Response<IEnumerable<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public GetAllEmployeeHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<Response<IEnumerable<EmployeeDto>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<IEnumerable<EmployeeDto>>();
            try
            {
                var employees = await _repository.GetAllAsync(cancellationToken);
                response.Data = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
                if (response.Data is not null)
                {
                    response.IsSuccess = true;
                    response.Message = "¡Consulta Exisota!";
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
