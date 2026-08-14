using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        //Include(r => r.Pokemon) sayesinde pokemonun adını direk idsinden alabiliyoruz
        public Review GetReview(int reviewId)
        {
            return this.context.Reviews.Include(r => r.Pokemon).Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return this.context.Reviews.Include(r => r.Pokemon).Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return this.context.Reviews.Include(r => r.Pokemon).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return this.context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool CreateReview(Review review)
        {
            this.context.Add(review);
            return Save();
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
            this.context.ChangeTracker.Clear();

            this.context.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            this.context.Remove(review);
            return Save();
        }
    }
}
