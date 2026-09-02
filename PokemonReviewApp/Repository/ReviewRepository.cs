using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using System.Security.Claims;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewRepository(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUser()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context?.User?.Identity?.IsAuthenticated != true)
                return "System";

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? context.User.FindFirst("sub")?.Value ?? "UnknownID";

            var userName = context.User.Identity.Name ?? "UnknownUser";

            return $"{userId} ({userName})";
        }

        public Review GetReview(int reviewId)
        {
            return this.context.Reviews.Where(r => r.Id == reviewId && !r.IsDeleted).Include(r => r.Pokemon).Include(r => r.Reviewer).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return this.context.Reviews.Where(r => r.Pokemon.Id == pokeId && !r.IsDeleted).Include(r => r.Pokemon).Include(r => r.Reviewer).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return this.context.Reviews.Where(r => !r.IsDeleted).Include(r => r.Pokemon).Include(r => r.Reviewer).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return this.context.Reviews.Any(r => r.Id == reviewId && !r.IsDeleted);
        }

        public bool CreateReview(Review review)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();

                this.context.Reviews.Add(review);

                if (Save())
                {
                    var reviewLog = new ReviewLog
                    {
                        Action = "POST",
                        Status = "Active",
                        PerformedBy = GetCurrentUser(),
                        ReviewId = review.Id,
                        NewTitle = review.Title,
                        NewText = review.Text,
                        NewRating = (int?)review.Rating,
                        NewReviewerId = review.ReviewerId,
                        NewPokemonId = review.PokemonId,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.ReviewLog.Add(reviewLog);

                    if (Save())
                    {
                        transaction.Commit();
                        return true;
                    }
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return this.context.Reviewers.Where(r => r.Id == reviewerId && !r.IsDeleted).Include(e => e.Reviews.Where(rv => !rv.IsDeleted)).FirstOrDefault();
        }

        public bool UpdateReview(Review review)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                this.context.ChangeTracker.Clear();
                var existingReview = this.context.Reviews
                    .AsNoTracking()
                    .Include(r => r.Reviewer)
                    .Include(r => r.Pokemon)
                    .FirstOrDefault(r => r.Id == review.Id);

                if (existingReview != null)
                {
                    var reviewLog = new ReviewLog
                    {
                        Action = "PUT",
                        Status = "Updated",
                        PerformedBy = GetCurrentUser(),
                        ReviewId = review.Id,
                        NewTitle = review.Title,
                        NewText = review.Text,
                        NewRating = (int?)review.Rating,
                        NewReviewerId = review.ReviewerId,
                        NewPokemonId = review.PokemonId,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.ReviewLog.Add(reviewLog);
                }

                var reviewToUpdate = this.context.Reviews.FirstOrDefault(r => r.Id == review.Id);
                if (reviewToUpdate != null)
                {
                    reviewToUpdate.Title = review.Title;
                    reviewToUpdate.Text = review.Text;
                    reviewToUpdate.Rating = review.Rating;
                    reviewToUpdate.PokemonId = review.PokemonId;
                    reviewToUpdate.ReviewerId = review.ReviewerId;

                    if (Save())
                    {
                        transaction.Commit();
                        return true;
                    }
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool DeleteReview(Review review)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                var reviewToUpdate = this.context.Reviews.FirstOrDefault(r => r.Id == review.Id);
                if (reviewToUpdate != null)
                {
                    reviewToUpdate.IsDeleted = true;
                    reviewToUpdate.DeletedAt = DateTime.UtcNow;
                    this.context.Reviews.Update(reviewToUpdate);

                    var reviewLog = new ReviewLog
                    {
                        Action = "DELETE",
                        Status = "Deleted",
                        PerformedBy = GetCurrentUser(),
                        ReviewId = reviewToUpdate.Id,
                        NewTitle = reviewToUpdate.Title,
                        NewText = reviewToUpdate.Text,
                        NewRating = (int?)reviewToUpdate.Rating,
                        NewReviewerId = reviewToUpdate.ReviewerId,
                        NewPokemonId = reviewToUpdate.PokemonId,
                        LoggedAt = DateTime.UtcNow
                    };

                    this.context.ReviewLog.Add(reviewLog);

                    if (Save())
                    {
                        transaction.Commit();
                        return true;
                    }
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            using var transaction = this.context.Database.BeginTransaction();
            try
            {
                foreach (var review in reviews)
                {
                    var reviewToUpdate = this.context.Reviews.FirstOrDefault(r => r.Id == review.Id);
                    if (reviewToUpdate != null)
                    {
                        reviewToUpdate.IsDeleted = true;
                        reviewToUpdate.DeletedAt = DateTime.UtcNow;
                        this.context.Reviews.Update(reviewToUpdate);

                        var reviewLog = new ReviewLog
                        {
                            Action = "DELETE",
                            Status = "Deleted",
                            PerformedBy = GetCurrentUser(),
                            ReviewId = reviewToUpdate.Id,
                            NewTitle = reviewToUpdate.Title,
                            NewText = reviewToUpdate.Text,
                            NewRating = (int?)reviewToUpdate.Rating,
                            NewReviewerId = reviewToUpdate.ReviewerId,
                            NewPokemonId = reviewToUpdate.PokemonId,
                            LoggedAt = DateTime.UtcNow
                        };

                        this.context.ReviewLog.Add(reviewLog);
                    }
                }

                if (Save())
                {
                    transaction.Commit();
                    return true;
                }

                transaction.Rollback();
                return false;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}