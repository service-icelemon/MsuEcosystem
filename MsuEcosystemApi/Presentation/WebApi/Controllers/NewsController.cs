using Application.Services.NewsService.DraftFeatures.Commands;
using Application.Services.NewsService.DraftFeatures.Queries;
using Application.Services.NewsService.PublicationFeatures.Commands;
using Application.Services.NewsService.PublicationFeatures.Queries;
using Application.Services.NewsService.ReviewFeatures.Commands;
using Application.Services.NewsService.ReviewFeatures.Queries;
using Application.Services.UserService.Queries;
using Domain.Entitties.News.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpPost("drafts/create")]
        public async Task<IActionResult> AddDraft(string title, string text, string previewImageUrl, bool isReadyForReview)
        {
            var currentUser = await _mediator.Send(new GetCurrentUser.Query(User));
            var response = await _mediator.Send(new CreateDraft.Command(title, text, previewImageUrl, isReadyForReview, currentUser.User.Id));
            if (response.Successed)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        // GET api/<NewsController>/5
        [HttpGet("drafts/{id}")]
        public async Task<IActionResult> GetDraftById(string id)
        {
            var response = await _mediator.Send(new GetDraft.Query(id));
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("drafts/update")]
        public IActionResult UpdateDraft(string id, string title, string text,
            string previewImageUrl, bool? isReadyForReview)
        {
            var currentUser = _mediator.Send(new GetCurrentUser.Query(User)).Result;
            var oldDraftVersion = _mediator.Send(new GetDraft.Query(id)).Result;

            if (currentUser.User.Id == oldDraftVersion.AuthorId)
            {
                var result = _mediator.Send(new UpdateDraft.Command(title, text, previewImageUrl,
                    isReadyForReview, oldDraftVersion.IsReviewed, oldDraftVersion)).Result;
                return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
            }

            return BadRequest("Текст может изменить только его автор");
        }

        [HttpPut("ToggleReviewed")]
        public IActionResult UpdateDraft(string id)
        {
            var draft = _mediator.Send(new GetDraft.Query(id)).Result;
            var result = _mediator.Send(new UpdateDraft.Command(IsReadyForReview: false, IsReviewed: !draft.IsReviewed, OldDraft: draft)).Result;
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpDelete("drafts/{id}")]
        public async Task<IActionResult> DeleteDraft(string id)
        {
            var result = await _mediator.Send(new DeleteDraft.Command(id));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("drafts/list/{authorId}")]
        public async Task<IEnumerable<DraftPreviewModel>> GetDraftsList(string authorId)
        {
            return await _mediator.Send(new GetUserDraftsList.Query(authorId));
        }

        [HttpGet("drafts/ForReviewList/")]
        public async Task<IEnumerable<DraftPreviewModel>> GetDraftsForReviewList()
        {
            return await _mediator.Send(new GetDraftsForReview.Query());
        }

        [HttpPost("reviews/create")]
        public async Task<IActionResult> AddReview(string title, string text, string previewImageUrl, string reviewText, string draftId)
        {
            var currentUser = await _mediator.Send(new GetCurrentUser.Query(User));
            var result = await _mediator.Send(new CreateReview.Command(title, text,
                 previewImageUrl, reviewText, draftId, currentUser.User.Id));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("reviews/")]
        public async Task<IEnumerable<ReviewPreviewModel>> GetReviewList()
        {
            return await _mediator.Send(new GetReviewsList.Query());
        }

        [HttpDelete("reviews/delete/{id}")]
        public IActionResult DeleteReview(string id)
        {
            var result = _mediator.Send(new DeleteReview.Command(id)).Result;
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("reviews/draft/{id}")]
        public async Task<IActionResult> GetReview(string draftId)
        {
            var result = await _mediator.Send(new GetDraftReview.Query(draftId));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("publication/create/{reviewId}")]
        public async Task<IActionResult> Publish(string reviewId)
        {
            var result = _mediator.Send(new CreatePublication.Command(reviewId)).Result;
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("GetPublicationList")]
        public async Task<IEnumerable<PublicationPreviewModel>> GetPublicationList()
        {
            return await _mediator.Send(new GetPublicationsList.Query());
        }

        [HttpGet("publications/{id}")]
        public async Task<PublicationViewModel> GetPublicationById(string id)
        {
            return await _mediator.Send(new GetPublication.Query(id));
        }

        [HttpDelete("publications/delete/{id}")]
        public async Task<IActionResult> DeletePublication(string id)
        {
            var result = await _mediator.Send(new DeletePublication.Command(id));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
