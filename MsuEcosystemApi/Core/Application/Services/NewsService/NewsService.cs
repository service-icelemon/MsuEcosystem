using Application.Interfaces;
using Domain.Entitties.Identity.ViewModels;
using Domain.Entitties.News;
using Domain.Entitties.News.NewsService;
using Domain.Entitties.News.ViewModels;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly IUserService _userService;
        private readonly MsuNewsContext _newsContext;

        public NewsService(MsuNewsContext context, IUserService userService)
        {
            _newsContext = context;
            _userService = userService;
        }

        public async Task<Draft> GetDraft(string id)
        {
            var result = await _newsContext.Drafts.FindAsync(id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<NewsServiceResponse> AddDraft(Draft draft)
        {
            await _newsContext.Drafts.AddAsync(draft);
            _newsContext.SaveChanges();
            return new NewsServiceResponse
            {
                Succeeded = true,
                Message = "Черновик успешно сохранён"
            };
        }

        public NewsServiceResponse UpdateDraft(Draft updatedDraft)
        {
            _newsContext.Drafts.Update(updatedDraft);
            _newsContext.SaveChanges();
            return new NewsServiceResponse
            {
                Succeeded = true,
                Message = "Черновик успешно обновлён"
            };
        }

        public async Task<NewsServiceResponse> DeleteDraft(string id)
        {
            var draft = await _newsContext.Drafts.FindAsync(id);
            if (draft != null)
            {
                _newsContext.Drafts.Remove(draft);
                _newsContext.SaveChanges();
                return new NewsServiceResponse
                {
                    Succeeded = true,
                    Message = "Черновик успешно удалён"
                };
            }
            return new NewsServiceResponse
            {
                Succeeded = false,
                Message = "Такого черновика не существует"
            };
        }

        public async Task<IEnumerable<DraftPreviewModel>> GetUserDraftsList(string authorId)
        {
            return await _newsContext.Drafts
                .Where(i => i.AuthorId == authorId)
                .Select(i => new DraftPreviewModel
                {
                    Id = i.Id,
                    PreviewImageUrl = i.PreviewImageUrl,
                    Title = i.Title
                }).ToListAsync();
        }

        public async Task<IEnumerable<DraftPreviewModel>> GetDraftsReadyForReviewList()
        {
            return await _newsContext.Drafts
                .Where(i => i.IsReadyForReview && !i.IsReviewed)
                .Select(i => new DraftPreviewModel
                {
                    Id = i.Id,
                    PreviewImageUrl = i.PreviewImageUrl,
                    Title = i.Title
                }).ToListAsync();
        }

        public async Task<NewsServiceResponse> AddDraftReview(Review review)
        {
            await _newsContext.Reviews.AddAsync(review);
            _newsContext.SaveChanges();
            return new NewsServiceResponse
            {
                Succeeded = false,
                Message = "Черновик был добавлен"
            };
        }

        public async Task<IEnumerable<ReviewPreviewModel>> GetReviewList()
        {
            return await _newsContext.Reviews.Select(i => new ReviewPreviewModel 
            { 
                ReviewId = i.Id,
                Title = i.Draft.Title,
                PreviewImageUrl = i.newPreviewImageUrl,
                Author = _userService.GetUserById(i.Draft.AuthorId).Result
            }).ToListAsync();
        }

        public NewsServiceResponse SendFeedback(string draftId)
        {
            var draft = _newsContext.Drafts.Find(draftId);
            draft.IsReviewed = true;
            _newsContext.Drafts.Update(draft);
            _newsContext.SaveChanges();
            return new NewsServiceResponse
            {
                Succeeded = true,
                Message = "Рецензия отправлена"
            };
        }

        public NewsServiceResponse UpdateReview(Review updatedReview)
        {
            if (_newsContext.Publications.Find(updatedReview.ReviewerId) != null)
            {
                _newsContext.Reviews.Update(updatedReview);
                _newsContext.SaveChanges();
                return new NewsServiceResponse
                {
                    Succeeded = true,
                    Message = "Рецензия успешно обновлён"
                };
            }
            return new NewsServiceResponse
            {
                Succeeded = false,
                Message = "Так как статья была опубликована рецензию изменить нельзя"
            };
        }

        public NewsServiceResponse DeleteReview(string reviewId)
        {
            var review = _newsContext.Reviews.Find(reviewId);
            if (_newsContext.Publications.Find(review.ReviewerId) != null)
            {
                return new NewsServiceResponse
                {
                    Succeeded = false,
                    Message = "Так как статья была опубликована рецензию удалить нельзя"
                };
                
            }
            _newsContext.Reviews.Remove(review);
            _newsContext.SaveChanges();
            return new NewsServiceResponse
            {
                Succeeded = true,
                Message = "Рецензия успешно удалена"
            };
        }

        public async Task<Review> GetDraftReview(string draftId)
        {
            return await _newsContext.Reviews.Where(i => i.DraftId == draftId).FirstOrDefaultAsync();
        }

        public async Task<NewsServiceResponse> Publish(string reviewId)
        {
            var review = await _newsContext.Reviews.FindAsync(reviewId);
            var publication = new Publication
            {
                Id = Guid.NewGuid().ToString(),
                ReviewId = review.Id
            };
            await _newsContext.Publications.AddAsync(publication);
            _newsContext.SaveChanges();
            return new NewsServiceResponse
            {
                Succeeded = true,
                Message = "Статья была опубликована"
            };
        }

        public async Task<IEnumerable<PublicationPreviewModel>> GetPublications()
        {
            return await _newsContext.Publications
                        .Select(i =>
                        new PublicationPreviewModel
                        {
                            Id = i.Id,
                            PreviewImageUrl = i.EditedArticle.newPreviewImageUrl,
                            Title = i.EditedArticle.EditetTitle,
                            DateTime = i.DateTime,
                            Author = _userService.GetUserById(i.EditedArticle.Draft.AuthorId).Result,
                            Editor = _userService.GetUserById(i.EditedArticle.ReviewerId).Result
                        }).ToListAsync();
        }

        public async Task<PublicationViewModel> GetPublicationById(string id)
        {
            var result = _newsContext.Publications
                .Include(i => i.EditedArticle)
                .ThenInclude(i => i.Draft)
                .Where(i => i.Id == id)
                .FirstOrDefault();
            var author = await _userService.GetUserById(result.EditedArticle.Draft.AuthorId);
            var editor = await _userService.GetUserById(result.EditedArticle.ReviewerId);

            return new PublicationViewModel
            {
                Id = result.Id,
                PreviewImageUrl = result.EditedArticle.newPreviewImageUrl,
                Title = result.EditedArticle.EditetTitle,
                Text = result.EditedArticle.EditedText,
                DateTime  =result.DateTime,
                Author = new UserPreviewModel
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    FatherName = author.FatherName
                },
                Editor = new UserPreviewModel
                {
                    Id = editor.Id,
                    FirstName = editor.FirstName,
                    LastName = editor.LastName,
                    FatherName = editor.FatherName
                }
            };
        }

        public async Task<NewsServiceResponse> DeletePublication(string id)
        {
            var publication = await _newsContext.Publications.FindAsync(id);
            if (publication != null)
            {
                _newsContext.Publications.Remove(publication);
                _newsContext.SaveChanges();
                return new NewsServiceResponse
                {
                    Succeeded = true,
                    Message = "Публикация успешно удалён"
                };
            }
            return new NewsServiceResponse
            {
                Succeeded = false,
                Message = "Такой публикации не существует"
            };
        }
    }
}
