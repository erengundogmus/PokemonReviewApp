using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.AuditLogs;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerInterface
    {
        private DataContext context;
        private IMapper mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            this.context.Add(reviewer);
            bool isReviewerSaved = this.context.SaveChanges() > 0;

            if (isReviewerSaved)
            {
                var reviewerLog = new ReviewerLog
                {
                    Action = "POST",
                    ReviewerId = reviewer.Id,
                    NewFirstName = reviewer.FirstName,
                    NewLastName = reviewer.LastName,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.ReviewerLog.Add(reviewerLog);
                return Save();
            }

            return false;
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            reviewer.IsDeleted = true;
            reviewer.DeletedAt = DateTime.UtcNow;
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return this.context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return this.context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return this.context.Reviews.Include(r => r.Pokemon).Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return this.context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = this.context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            this.context.ChangeTracker.Clear();

            var existingReviewer = this.context.Reviewers.FirstOrDefault(r => r.Id == reviewer.Id);

            if (existingReviewer != null)
            {
                var reviewerLog = new ReviewerLog
                {
                    Action = "PUT",
                    ReviewerId = reviewer.Id,
                    OldFirstName = existingReviewer.FirstName,
                    NewFirstName = reviewer.FirstName,
                    OldLastName = existingReviewer.LastName,
                    NewLastName = reviewer.LastName,
                    LoggedAt = DateTime.UtcNow
                };

                this.context.ReviewerLog.Add(reviewerLog);

                existingReviewer.FirstName = reviewer.FirstName;
                existingReviewer.LastName = reviewer.LastName;
            }

            return Save();
        }
    }
}
