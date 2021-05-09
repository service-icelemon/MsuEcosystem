using Application.Services.ScheduleService.ClassTimeFeatures.Commands;
using Application.Services.ScheduleService.ClassTimeFeatures.Queries;
using Application.Services.ScheduleService.ClassTypeFeatures.Commands;
using Application.Services.ScheduleService.ClassTypeFeatures.Queries;
using Application.Services.ScheduleService.ScheduleFeatures.Commands;
using Application.Services.ScheduleService.ScheduleFeatures.Queries;
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

        [HttpGet("{groupNumber}")]
        public async Task<IActionResult> GetGroupSchedule(int groupNumber)
        {
            var result = await _mediator.Send(new GetGroupSchedule.Query(groupNumber));
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("не найдено");
        }

        [HttpGet("facultyId")]
        public async Task<IActionResult> GetGroupScheduleList(string facultyId)
        {
            var result = await _mediator.Send(new GetGroupScheduleList.Query(facultyId));
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("не найдено");
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

        [HttpPut]
        public async Task<IActionResult> UpdateGroupSchedule([FromBody] GroupShedule groupShedule)
        {
            var result = await _mediator.Send(new UpdateGroupSchedule.Command(groupShedule));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("classtime")]
        public async Task<IActionResult> GetClassTimeList()
        {
            var result = await _mediator.Send(new GetClassTimeList.Query());
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("не найдено");
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

        [HttpDelete("classtime/{id}")]
        public async Task<IActionResult> DeleteClassTime(string id)
        {
            var result = await _mediator.Send(new DeleteClassTime.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("classtime")]
        public async Task<IActionResult> UpdateClassTime([FromBody] ClassTime classTime)
        {
            var result = await _mediator.Send(new UpdateClassTime.Command(classTime));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("classtype")]
        public async Task<IActionResult> GetClassTypeList()
        {
            var result = await _mediator.Send(new GetClassTypeList.Query());
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("не найдено");
        }

        [HttpPost("classtype")]
        public async Task<IActionResult> AddClassType([FromBody] ClassType classType)
        {
            var result = await _mediator.Send(new CreateClassType.Command(classType));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("classtype/{id}")]
        public async Task<IActionResult> DeleteClassType(string id)
        {
            var result = await _mediator.Send(new DeleteClassType.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("classtype")]
        public async Task<IActionResult> UpdateClassType([FromBody] ClassType classType)
        {
            var result = await _mediator.Send(new UpdateClassType.Command(classType));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
