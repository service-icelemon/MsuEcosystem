using Application.Services.InfoService.TeacherFeatures.Commands;
using Application.Services.InfoService.TeacherFeatures.Queries;
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
    public class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{facultyId}")]
        public async Task<IEnumerable<TeacherPreviewModel>> GetTeacherList(string facultyId)
        {
            return await _mediator.Send(new GetTeachersByFaculty.Query(facultyId));
        }

        [HttpGet("{teacherId}")]
        public async Task<TeacherViewModel> GetTeacherById(string studentId)
        {
            return await _mediator.Send(new GetTeacherById.Query(studentId));
        }

        [HttpGet("{departmentId}")]
        public async Task<IEnumerable<TeacherPreviewModel>> GetTeacherByDepartmentId(string departmentId)
        {
            return await _mediator.Send(new GetTeachersByDepartment.Query(departmentId));
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
        {
            var result = await _mediator.Send(new CreateTeacher.Command(teacher));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher([FromBody] Teacher teacher)
        {
            var result = await _mediator.Send(new UpdateTeacher.Command(teacher));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> DeleteTeacher(string teacherId)
        {
            var result = await _mediator.Send(new DeleteTeacher.Command(teacherId));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
