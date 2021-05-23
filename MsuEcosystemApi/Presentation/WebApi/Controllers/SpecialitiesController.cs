using Application.Services.InfoService.SpecialityFeatures.Commands;
using Application.Services.InfoService.SpecialityFeatures.Queries;
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
    public class SpecialitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SpecialitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<SpecialityViewModel> GetSpecilityById(string specialityId)
        {
            return await _mediator.Send(new GetSpecialityById.Query(specialityId));
        }

        [HttpGet("departmentId")]
        public async Task<IEnumerable<SpecialityPreviewModel>> GetSpecilityList(string departmentId)
        {
            return await _mediator.Send(new GetSpecialitiesByDepartment.Query(departmentId));
        }

        [HttpPost]
        public async Task<IActionResult> AddSpeciality([FromBody] Speciality speciality)
        {
            var result = await _mediator.Send(new CreateSpeciality.Command(speciality));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpeciality([FromBody] Speciality speciality)
        {
            var result = await _mediator.Send(new UpdateSpeciality.Command(speciality));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{specialityId}")]
        public async Task<IActionResult> DeleteDepartment(string specialityId)
        {
            var result = await _mediator.Send(new DeleteSpeciality.Command(specialityId));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
