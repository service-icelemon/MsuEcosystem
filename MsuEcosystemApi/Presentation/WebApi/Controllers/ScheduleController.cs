using Application.Services.ScheduleService.ClassTimeFeatures.Commands;
using Application.Services.ScheduleService.ScheduleFeatures.Commands;
using Domain.Entitties.Schedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddGroupSchedule([FromBody] GroupShedule groupShedule)
        {
            var result = await _mediator.Send(new CreateGroupSchedule.Command(groupShedule));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupSchedule(string id)
        {
            var result = await _mediator.Send(new DeleteGroupSchedule.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("classtime")]
        public async Task<IActionResult> AddClassTime([FromBody] ClassTime classTime)
        {
            var result = await _mediator.Send(new CreateClassTime.Command(classTime));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
