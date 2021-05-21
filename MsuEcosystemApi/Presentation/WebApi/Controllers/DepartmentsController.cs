using Application.Services.InfoService.DepartmentFeatures.Commands;
using Application.Services.InfoService.DepartmentFeatures.Queries;
using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentPreviewModel>> GetDepartmentsByFaculty(string facultyId)
        {
            return await _mediator.Send(new GetDepartmentsByFaculty.Query(facultyId));
        }

        [HttpGet("{departmentId}")]
        public async Task<DepartmentViewModel> GetDepartmentById(string departmentId)
        {
            return await _mediator.Send(new GetDepartmentById.Query(departmentId));
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
            var result = await _mediator.Send(new CreateDepartment.Command(department));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher([FromBody] Department department)
        {
            var result = await _mediator.Send(new UpdateDepartment.Command(department));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(string departmentId)
        {
            var result = await _mediator.Send(new DeleteDepartment.Command(departmentId));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
