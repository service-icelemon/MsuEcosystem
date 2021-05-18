using Application.Services.InfoService.SubjectFeatures.Commands;
using Application.Services.InfoService.SubjectFeatures.Queries;
using Domain.Entitties.MsuInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Subject>> GetSubjectList()
        {
            return await _mediator.Send(new GetSubjectList.Query());
        }

        [HttpGet("{subjectId}")]
        public async Task<Subject> GetSubjectById(string subjectId)
        {
            return await _mediator.Send(new GetSubjectById.Query(subjectId));
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody] Subject subject)
        {
            var result = await _mediator.Send(new CreateSubject.Command(subject));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody] Subject subject)
        {
            var result = await _mediator.Send(new UpdateSubject.Command(subject));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{subjectId}")]
        public async Task<IActionResult> DeleteSubject(string subjectId)
        {
            var result = await _mediator.Send(new DeleteSubject.Command(subjectId));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
