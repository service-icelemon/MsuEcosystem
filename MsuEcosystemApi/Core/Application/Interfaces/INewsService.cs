using Domain.Entitties.News;
using Domain.Entitties.News.NewsService;
using Domain.Entitties.News.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<Draft> GetDraft(string id);
        Task<NewsServiceResponse> AddDraft(Draft draft);
        Task<NewsServiceResponse> DeleteDraft(string id);
        NewsServiceResponse UpdateDraft(Draft updatedDraft);
        Task<IEnumerable<DraftPreviewModel>> GetUserDraftsList(string authorId);
        Task<IEnumerable<DraftPreviewModel>> GetDraftsReadyForReviewList();
        Task<NewsServiceResponse> AddDraftReview(Review review);
        NewsServiceResponse UpdateReview(Review updatedReview);
        Task<IEnumerable<ReviewPreviewModel>> GetReviewList();
        NewsServiceResponse DeleteReview(string reviewId);
        NewsServiceResponse SendFeedback(string draftId);
        Task<Review> GetDraftReview(string draftId);
        Task<NewsServiceResponse> Publish(string reviewId);
        Task<IEnumerable<PublicationPreviewModel>> GetPublications();
        Task<PublicationViewModel> GetPublicationById(string id);
        Task<NewsServiceResponse> DeletePublication(string id);
    }
}
