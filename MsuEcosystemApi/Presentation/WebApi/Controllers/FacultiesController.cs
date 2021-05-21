using Application.Services.InfoService.FacultyFeatures.Commands;
using Application.Services.InfoService.FacultyFeatures.Queries;
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
    public class FacultiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FacultiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<FacultyPreviewModel>> GetFacultyList()
        {
            return await _mediator.Send(new GetFacultyList.Query());
        }

        [HttpGet("{facultyId}")]
        public async Task<FacultyViewModel> GetFacultyById(string facultyId)
        {
            return await _mediator.Send(new GetFacultyById.Query(facultyId));
        }

        [HttpPost]
        public async Task<IActionResult> AddFaculty([FromBody] Faculty faculty)
        {
            var result = await _mediator.Send(new CreateFaculty.Command(faculty));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFaculty([FromBody] Faculty faculty)
        {
            var result = await _mediator.Send(new UpdateFaculty.Command(faculty));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{facultyId}")]
        public async Task<IActionResult> DeleteFaculty(string facultyId)
        {
            var result = await _mediator.Send(new DeleteFaculty.Command(facultyId));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
