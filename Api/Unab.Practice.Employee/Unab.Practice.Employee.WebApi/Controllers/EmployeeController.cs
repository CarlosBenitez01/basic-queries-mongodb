using Microsoft.AspNetCore.Mvc;

using Unab.Practice.Employees.Transversal;
using MediatR;
using Unab.Practice.Employees.UseCases.Employees.Commands.CreateEmployeeCommand;
using Unab.Practice.Employees.UseCases.Employees.Commands.UpdateEmployeeCommand;
using MongoDB.Bson;
using Unab.Practice.Employees.UseCases.Employees.Commands.DeleteEmployeeCommand;
using Unab.Practice.Employees.Dto.Dtos;
using Unab.Practice.Employees.UseCases.Employees.Queries.GetNextCorrelativeEmployeeQuery;
using Unab.Practice.Employees.UseCases.Employees.Queries.GetByIdEmployeeQuery;
using Unab.Practice.Employees.UseCases.Employees.Queries.GetAllEmployeeQuery;

namespace Unab.Practice.Employee.WebApi.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [HttpGet("next-correlative")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<string?>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNextCorrelativeAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNextCorrelativeEmployeeQuery(), cancellationToken);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool?>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            if (command == null)
            {
                response.Message = "El cuerpo de la solicitud no puede ser nulo";
                return BadRequest(response);
            }
            response = await _mediator.Send(command, cancellationToken);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool?>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            if (command == null)
            {
                response.Message = "El cuerpo de la solicitud no puede ser nulo";
                return BadRequest(response);
            }

            command.Id = id;

            response = await _mediator.Send(command, cancellationToken);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPatch("delete/{id}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<bool?>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteEmployeeCommand { Id = id }, cancellationToken);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("get-by-id/{id}")]
        [ProducesResponseType(typeof(Response<EmployeeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<EmployeeDto?>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdEmployeeQuery { Id = id }, cancellationToken);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("get-all")]
        [ProducesResponseType(typeof(Response<IEnumerable<EmployeeDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<IEnumerable<EmployeeDto>?>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllEmployeeQuery(), cancellationToken);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
