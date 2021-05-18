using Application.Services.InfoService.EducationFormsFeatures.Commands;
using Application.Services.InfoService.EducationFormsFeatures.Queries;
using Domain.Entitties.MsuInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationFormsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EducationFormsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<EducationForm>> GetEducationFormList()
        {
            return await _mediator.Send(new GetEducationFormList.Query());
        }

        [HttpGet("{facultyId}")]
        public async Task<EducationForm> GetEducationFormById(string facultyId)
        {
            return await _mediator.Send(new GetEducationFormById.Query(facultyId));
        }

        [HttpPost]
        public async Task<IActionResult> AddEducationForm([FromBody] EducationForm educationForm)
        {
            var result = await _mediator.Send(new CreateEducationForm.Command(educationForm));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEducationForm([FromBody] EducationForm educationForm)
        {
            var result = await _mediator.Send(new UpdateEducationForm.Command(educationForm));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEducationForm(string id)
        {
            var result = await _mediator.Send(new DeleteEducationForm.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
