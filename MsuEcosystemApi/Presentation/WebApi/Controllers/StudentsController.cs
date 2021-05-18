using Application.Services.InfoService.StudentFeatures.Commands;
using Application.Services.InfoService.StudentFeatures.Queries;
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
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{facultyId}")]
        public async Task<IEnumerable<StudentPreviewModel>> GetEditionsList(string facultyId)
        {
            return await _mediator.Send(new GetStudentsByFaculty.Query(facultyId));
        }

        [HttpGet("{studentId}")]
        public async Task<StudentViewModel> GetStudentById(string studentId)
        {
            return await _mediator.Send(new GetStudentById.Query(studentId));
        }

        [HttpGet("{studentCard}")]
        public async Task<StudentViewModel> GetStudentByCard(int studentCard)
        {
            return await _mediator.Send(new GetStudentByStudentCard.Query(studentCard));
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            var result = await _mediator.Send(new CreateStudent.Command(student));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
        {
            var result = await _mediator.Send(new UpdateStudent.Command(student));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> DeleteStudent(string studentId)
        {
            var result = await _mediator.Send(new DeleteStudent.Command(studentId));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
