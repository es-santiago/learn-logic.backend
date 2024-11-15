using AutoMapper;
using LearnLogic.Domain.DTO;
using LearnLogic.Domain.ViewModels;
using LearnLogic.Infra.Data.Entities;

namespace LearnLogic.Infra.CrossCutting.IoC.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapperViewModelDTO();
            MapperDTOEntity();
        }

        private void MapperViewModelDTO()
        {
            CreateMap<UserViewModel, UserDTO>().ReverseMap();
            CreateMap<QuestionViewModel, QuestionDTO>().ReverseMap();
            CreateMap<QuestionItemsViewModel, QuestionItemsDTO>().ReverseMap();
            CreateMap<QuestionAnswerViewModel, QuestionAnswerDTO>().ReverseMap();
        }

        private void MapperDTOEntity()
        {
            CreateMap<UserDTO, UserEntity>().ReverseMap();
            CreateMap<QuestionDTO, QuestionEntity>().ReverseMap();
            CreateMap<QuestionItemsDTO, QuestionItemsEntity>().ReverseMap();
            CreateMap<QuestionAnswerDTO, QuestionAnswerEntity>().ReverseMap();
        }
    }
}
