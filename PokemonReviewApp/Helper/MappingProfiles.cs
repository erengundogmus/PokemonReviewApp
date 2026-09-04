using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.InputDtos;
using PokemonReviewApp.Models;
using PokemonReviewApp.OutputDtos;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonOutputDto>()
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.PokemonOwners.FirstOrDefault().Owner.Id))  //get pokemonda owner ve category bilgilerini de almak için mapping yaptık
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.PokemonOwners.FirstOrDefault().Owner.Name))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.PokemonCategories.FirstOrDefault().Category.Id))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.PokemonCategories.FirstOrDefault().Category.Name)).ReverseMap();
            CreateMap<Pokemon, PokemonInputDto>().ReverseMap();

            CreateMap<Category, CategoryOutputDto>().ReverseMap();
            CreateMap<Category, CategoryInputDto>().ReverseMap();

            CreateMap<Country, CountryOutputDto>().ReverseMap();
            CreateMap<Country, CountryInputDto>().ReverseMap();


            CreateMap<Owner, OwnerOutputDto>().ReverseMap();
            CreateMap<Owner, OwnerInputDto>().ReverseMap();

            CreateMap<Review, ReviewOutputDto>()
            .ForMember(dest => dest.PokemonId, opt => opt.MapFrom(src => src.Pokemon.Id))
            .ForMember(dest => dest.PokemonName, opt => opt.MapFrom(src => src.Pokemon.Name))
            .ForMember(dest => dest.ReviewerId, opt => opt.MapFrom(src => src.Reviewer.Id))
            .ForMember(dest => dest.ReviewerFirstName, opt => opt.MapFrom(src => src.Reviewer.FirstName))
            .ForMember(dest => dest.ReviewerLastName, opt => opt.MapFrom(src => src.Reviewer.LastName)).ReverseMap();
            CreateMap<Review, ReviewInputDto>().ReverseMap();

            CreateMap<Reviewer, ReviewerOutputDto>().ReverseMap();
            CreateMap<Reviewer, ReviewerInputDto>().ReverseMap();
            
            CreateMap<Food, FoodOutputDto>().ReverseMap();
            CreateMap<Food, FoodInputDto>().ReverseMap();

            CreateMap<Pokemon, PokemonDetailOutputDto>().ReverseMap();


        }
    }
}
