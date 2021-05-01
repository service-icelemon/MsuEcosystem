using Application.Services.LibraryService.AuthorFeatures.Commands;
using Application.Services.LibraryService.AuthorFeatures.Queries;
using Application.Services.LibraryService.BorrowedEditionFeatures.Commands;
using Application.Services.LibraryService.BorrowedEditionFeatures.Queries;
using Application.Services.LibraryService.EditionFeatures.Commands;
using Application.Services.LibraryService.EditionFeatures.Queries;
using Application.Services.LibraryService.EditionRequestFeatures.Commands;
using Application.Services.LibraryService.EditionRequestFeatures.Queries;
using Application.Services.LibraryService.EditionTypeFeatures.Commands;
using Application.Services.LibraryService.EditionTypeFeatures.Queries;
using Application.Services.LibraryService.GenreService.Commands;
using Application.Services.LibraryService.GenreService.Queries;
using Application.Services.LibraryService.PickUpPointsFeatures.Commands;
using Application.Services.LibraryService.PickUpPointsFeatures.Queires;
using Application.Services.LibraryService.PublishingHouseFeatures.Commands;
using Application.Services.LibraryService.PublishingHouseFeatures.Queries;
using Domain.Entitties.Library;
using Domain.Entitties.Library.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibraryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LibraryController>
        [HttpGet("editions")]
        public async Task<IEnumerable<EditionViewModel>> GetEditionsList()
        {
            return await _mediator.Send(new GetEditionList.Query());
        }

        [HttpGet("editions/{id}")]
        public async Task<EditionViewModel> GetEditionById(string id)
        {
            return await _mediator.Send(new GetEdition.Query(id));
        }

        [HttpPost("editions")]
        public async Task<IActionResult> AddEdition([FromBody] Edition edition)
        {
            var result = await _mediator.Send(new CreateEdition.Command(edition));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        // POST api/<LibraryController>
        [HttpPut("editions")]
        public async Task<IActionResult> UpdateEdition([FromBody] Edition edition)
        {
            var result = await _mediator.Send(new UpdateEdition.Command(edition));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("editions/{id}")]
        public async Task<IActionResult> DeleteEdition(string id)
        {
            var result = await _mediator.Send(new DeleteEdition.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("authors")]
        public async Task<IEnumerable<Author>> GetAuthorList()
        {
            return await _mediator.Send(new GetAuthorList.Query());
        }

        [HttpGet("authors/{id}")]
        public async Task<Author> GetAuthorById(string id)
        {
            return await _mediator.Send(new GetAuthor.Query(id));
        }

        [HttpPost("authors")]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            var result = await _mediator.Send(new CreateAuthor.Command(author));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("authors")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            var result = await _mediator.Send(new UpdateAuthor.Command(author));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("authors/{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            var result = await _mediator.Send(new DeleteAuthor.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("genres")]
        public async Task<IEnumerable<Genre>> GetGenreList()
        {
            return await _mediator.Send(new GetGenreList.Query());
        }

        [HttpGet("genres/{id}")]
        public async Task<Genre> GetGenreById(string id)
        {
            return await _mediator.Send(new GetGenre.Query(id));
        }

        [HttpPost("genres")]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            var result = await _mediator.Send(new CreateGenre.Command(genre));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("genres")]
        public async Task<IActionResult> UpdateGenre([FromBody] Genre genre)
        {
            var result = await _mediator.Send(new UpdateGenre.Command(genre));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("genres/{id}")]
        public async Task<IActionResult> DeleteGenre(string id)
        {
            var result = await _mediator.Send(new DeleteGenre.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("editiontypes")]
        public async Task<IEnumerable<EditionType>> GetEditionTypeList()
        {
            return await _mediator.Send(new GetEditionTypeList.Query());
        }

        [HttpGet("editiontypes/{id}")]
        public async Task<EditionType> GetEditionTypeById(string id)
        {
            return await _mediator.Send(new GetEditionType.Query(id));
        }

        [HttpPost("editiontypes")]
        public async Task<IActionResult> AddEditionType([FromBody] EditionType editionType)
        {
            var result = await _mediator.Send(new CreateEditionType.Command(editionType));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("editiontypes")]
        public async Task<IActionResult> UpdateGenre([FromBody] EditionType editionType)
        {
            var result = await _mediator.Send(new UpdateEditionType.Command(editionType));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("editiontypes/{id}")]
        public async Task<IActionResult> DeleteEditionType(string id)
        {
            var result = await _mediator.Send(new DeleteEditionType.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("publishinghouses")]
        public async Task<IEnumerable<PublishingHouse>> GetPublishingHouseList()
        {
            return await _mediator.Send(new GetPublishingHouseList.Query());
        }

        [HttpGet("publishinghouses/{id}")]
        public async Task<PublishingHouse> GetPublishingHouseById(string id)
        {
            return await _mediator.Send(new GetPublishingHouse.Query(id));
        }

        [HttpPost("publishinghouses")]
        public async Task<IActionResult> AddPublishingHouse([FromBody] PublishingHouse publishingHouse)
        {
            var result = await _mediator.Send(new CreatePublishingHouse.Command(publishingHouse));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("publishinghouses")]
        public async Task<IActionResult> UpdatePublishingHouse([FromBody] PublishingHouse publishingHouse)
        {
            var result = await _mediator.Send(new UpdatePublishingHouse.Command(publishingHouse));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("publishinghouses/{id}")]
        public async Task<IActionResult> DeletePublishingHouse(string id)
        {
            var result = await _mediator.Send(new DeletePublishingHouse.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("pickuppoints")]
        public async Task<IEnumerable<PickUpPoint>> GetPickUppointsList()
        {
            return await _mediator.Send(new GetPickUpPointList.Query());
        }

        [HttpGet("pickuppoints/{id}")]
        public async Task<PickUpPoint> GetPickUpPointById(string id)
        {
            return await _mediator.Send(new GetPickUpPoint.Query(id));
        }

        [HttpPost("pickuppoints")]
        public async Task<IActionResult> AddPickUpPoint([FromBody] PickUpPoint pickUpPoint)
        {
            var result = await _mediator.Send(new CreatePickUpPoint.Command(pickUpPoint));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("pickuppoints")]
        public async Task<IActionResult> UpdatePickUpPoint([FromBody] PickUpPoint pickUpPoint)
        {
            var result = await _mediator.Send(new UpdatePickUpPoint.Command(pickUpPoint));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("pickuppoints/{id}")]
        public async Task<IActionResult> DeletePickUpPoint(string id)
        {
            var result = await _mediator.Send(new DeletePickUpPoint.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("editionrequest")]
        public async Task<IEnumerable<EditionRequest>> GetEditionRequestList()
        {
            return await _mediator.Send(new GetEditionRequestList.Query());
        }

        [HttpGet("editionrequest/{id}")]
        public async Task<EditionRequest> GetEditionRequestById(string id)
        {
            return await _mediator.Send(new GetEditionRequest.Query(id));
        }

        [HttpPost("editionrequest")]
        public async Task<IActionResult> AddEditionRequest([FromBody] EditionRequest editionRequest)
        {
            var result = await _mediator.Send(new CreateEditionRequest.Command(editionRequest));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("editionrequest")]
        public async Task<IActionResult> UpdateEditionRequest([FromBody] EditionRequest editionRequest)
        {
            var result = await _mediator.Send(new UpdateEditionRequest.Command(editionRequest));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("editionrequest/{id}")]
        public async Task<IActionResult> DeleteEditionRequest(string id)
        {
            var result = await _mediator.Send(new DeleteEditionRequest.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("borrowededitions")]
        public async Task<IEnumerable<BorrowedEdition>> GetBorrowedEditionList()
        {
            return await _mediator.Send(new GetBorrowedEditionsList.Query());
        }

        [HttpGet("borrowededitions/{readerid}")]
        public async Task<IEnumerable<BorrowedEdition>> GetReaderBorrowedEdition(string readerId)
        {
            return await _mediator.Send(new GetReaderBorrowedEditions.Query(readerId));
        }

        [HttpGet("borrowededitions/{id}")]
        public async Task<BorrowedEdition> GetBorrowedEditionById(string id)
        {
            return await _mediator.Send(new GetBorrowedEdition.Query(id));
        }

        [HttpPost("borrowededitions")]
        public async Task<IActionResult> AddBorrowedEdition([FromBody] BorrowedEdition borrowedEdition)
        {
            var result = await _mediator.Send(new CreateBorrowedEdition.Command(borrowedEdition));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("borrowededitions")]
        public async Task<IActionResult> UpdateBorrowedEdition([FromBody] BorrowedEdition borrowedEdition)
        {
            var result = await _mediator.Send(new UpdateBorrowedEdition.Command(borrowedEdition));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("borrowededitions/{id}")]
        public async Task<IActionResult> DeleteBorrowedEdition(string id)
        {
            var result = await _mediator.Send(new DeleteBorrowedEdition.Command(id));
            if (result.Successed)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
