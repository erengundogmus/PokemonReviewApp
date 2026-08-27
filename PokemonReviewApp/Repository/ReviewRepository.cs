using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewInterface
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Review GetReview(int reviewId)
        {
            return this.context.Reviews.Where(r => r.Id == reviewId).Include(r => r.Pokemon).Include(r => r.Reviewer).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return this.context.Reviews.Where(r => r.Pokemon.Id == pokeId).Include(r => r.Pokemon).Include(r => r.Reviewer).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return this.context.Reviews.Include(r => r.Pokemon).Include(r => r.Reviewer).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return this.context.Reviews.Any(r => r.Id == reviewId);
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
                        ReviewId = review.Id,
                        NewTitle = review.Title,
                        NewText = review.Text,
                        NewRating = (int?)review.Rating,
                        NewReviewerId = review.Reviewer?.Id,
                        NewPokemonId = review.Pokemon?.Id,
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
            return this.context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
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
                        ReviewId = review.Id,
                        NewTitle = review.Title,
                        NewText = review.Text,
                        NewRating = (int?)review.Rating,
                        NewReviewerId = review.Reviewer?.Id ?? existingReview.Reviewer?.Id,
                        NewPokemonId = review.Pokemon?.Id ?? existingReview.Pokemon?.Id,
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
            review.IsDeleted = true;
            review.DeletedAt = DateTime.UtcNow;
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            this.context.RemoveRange(reviews);
            return Save();
        }
    }
}