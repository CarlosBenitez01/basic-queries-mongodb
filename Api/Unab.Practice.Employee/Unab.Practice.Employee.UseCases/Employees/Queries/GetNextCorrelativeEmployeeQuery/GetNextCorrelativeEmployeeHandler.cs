using MediatR;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Transversal;

namespace Unab.Practice.Employees.UseCases.Employees.Queries.GetNextCorrelativeEmployeeQuery
{
    internal sealed class GetNextCorrelativeEmployeeHandler: IRequestHandler<GetNextCorrelativeEmployeeQuery, Response<string>>
    {
        private readonly IEmployeeRepository _repository;
        public GetNextCorrelativeEmployeeHandler(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Response<string>> Handle(GetNextCorrelativeEmployeeQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            try
            {
                var count = await _repository.CountAsync(cancellationToken);
                response.Data = $"E-{count + 1}";
                response.IsSuccess = true;
                response.Message = "¡Correlativo Generado Exitosamente!";
                
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
