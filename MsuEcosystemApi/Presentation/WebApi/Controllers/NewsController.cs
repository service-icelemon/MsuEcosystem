using Application.Interfaces;
using Domain.Entitties.News;
using Domain.Entitties.News.NewsService;
using Domain.Entitties.News.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IUserService _userService;

        public NewsController(INewsService newsService, IUserService userService)
        {
            _newsService = newsService;
            _userService = userService;
        }

        //GET: api/<NewsController>
        [HttpPost("AddDraft")]
        public async Task<IActionResult> AddDraft(string title, string text, string previewImageUrl, bool isReadyForReview)
        {
            var draft = new Draft
            {
                Id = Guid.NewGuid().ToString(),
                PreviewImageUrl = previewImageUrl,
                Title = title,
                Text = text,
                AuthorId = _userService.GetCurrentUser(User).Result.Id,
                IsReadyForReview = isReadyForReview,
                IsReviewed = false
            };
            var result = await _newsService.AddDraft(draft);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        // GET api/<NewsController>/5
        [HttpGet("GetDraft")]
        public async Task<Draft> GetDraftById(string id)
        {
            return await _newsService.GetDraft(id);
        }

        [HttpPut("SetReadyforReview")]
        public IActionResult UpdateDraft(string id)
        {
            var draft = new Draft
            {
                Id = id,
                IsReadyForReview = true
            };
            var result = _newsService.UpdateDraft(draft);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("UpdateDraft")]
        public IActionResult UpdateDraft(string id, string title, string text, string previewImageUrl, bool? isReadyForReview)
        {
            var currentUser = _userService.GetCurrentUser(User).Result;
            var oldDraftVersion = _newsService.GetDraft(id).Result;

            if (currentUser.Id == oldDraftVersion.AuthorId)
            {
                var updated = new Draft
                {
                    Id = id,
                    AuthorId = oldDraftVersion.AuthorId,
                    Text = text ?? oldDraftVersion.Text,
                    Title = title ?? oldDraftVersion.Title,
                    PreviewImageUrl = previewImageUrl ?? oldDraftVersion.PreviewImageUrl,
                    IsReadyForReview = isReadyForReview ?? oldDraftVersion.IsReadyForReview
                };
                var result = _newsService.UpdateDraft(updated);
                if (result.Succeeded)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Изменить черновик может только его автор");
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("DeleteDraft")]
        public async Task<IActionResult> DeleteArticle(string id)
        {
            var result = await _newsService.DeleteDraft(id);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetDraftsList")]
        public async Task<IEnumerable<DraftPreviewModel>> GetDraftsList(string authorId)
        {
            return await _newsService.GetUserDraftsList(authorId);
        }

        [HttpGet("GetDraftsForReviewList")]
        public async Task<IEnumerable<DraftPreviewModel>> GetDraftsForReviewList()
        {
            return await _newsService.GetDraftsReadyForReviewList();
        }

        [HttpPost("AddReview")]
        public async Task<IActionResult> AddReview(string title, string text, string previewImageUrl, string reviewText, string draftId)
        {
            var review = new Review
            {
                Id = Guid.NewGuid().ToString(),
                ReviewerId = _userService.GetCurrentUser(User).Result.Id,
                newPreviewImageUrl = previewImageUrl,
                EditetTitle = title,
                EditedText = text,
                DraftId = draftId,
                ReviewText = reviewText
            };
            var result = await _newsService.AddDraftReview(review);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetReviewList")]
        public async Task<IEnumerable<ReviewPreviewModel>> GetReviewList()
        {
            return await _newsService.GetReviewList();
        }

        [HttpPost("SendFeedBack")]
        public IActionResult SendFeedBack(string draftId)
        {
            var result = _newsService.SendFeedback(draftId);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("DeleteReview")]
        public IActionResult DeleteReview(string id)
        {
            var result = _newsService.DeleteReview(id);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetDraftReview")]
        public async Task<Review> GetReview(string draftId)
        {
            return await _newsService.GetDraftReview(draftId);
        }

        [HttpPost("Publish")]
        public async Task<IActionResult> Publish(string reviewId)
        {
            var result = await _newsService.Publish(reviewId);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetPublicationList")]
        public async Task<IEnumerable<PublicationPreviewModel>> GetPublicationList()
        {
            return await _newsService.GetPublications();
        }

        [HttpGet("GetPublicationById")]
        public async Task<PublicationViewModel> GetPublicationById(string id)
        {
            return await _newsService.GetPublicationById(id);
        }

        [HttpDelete("DeletePublication")]
        public async Task<IActionResult> DeletePublication(string id)
        {
            var result = await _newsService.DeletePublication(id);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
