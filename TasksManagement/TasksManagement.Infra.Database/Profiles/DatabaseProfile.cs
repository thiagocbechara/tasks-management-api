using AutoMapper;
using TasksManagement.Domain.Entities;
using TasksManagement.Infra.Database.Entities;

namespace TasksManagement.Infra.Database.Profiles;

internal class DatabaseProfile : Profile
{
    public DatabaseProfile()
    {
        CreateMap<ProjectEntity, ProjectDbEntity>()
            .ForMember(e => e.Id, opt => opt.MapFrom(p => p.Code))
            .ForMember(e => e.OwnerId, opt => opt.MapFrom(p => p.Owner.Code))
            .ReverseMap()
            .ForMember(p => p.Code, opt => opt.MapFrom(e => e.Id));

        CreateMap<TaskEntity, TaskDbEntity>()
            .ForMember(e => e.Id, opt => opt.MapFrom(p => p.Code))
            .ReverseMap()
            .ForMember(p => p.Code, opt => opt.MapFrom(e => e.Id));


        CreateMap<TaskChangeEntity, TaskChangeDbEntity>()
            .ForMember(e => e.Id, opt => opt.MapFrom(p => p.Code))
            .ForMember(e => e.AuthorId, opt => opt.MapFrom(p => p.Author.Code))
            .ReverseMap()
            .ForMember(p => p.Code, opt => opt.MapFrom(e => e.Id));

        CreateMap<TaskCommentEntity, TaskCommentDbEntity>()
            .ForMember(e => e.Id, opt => opt.MapFrom(p => p.Code))
            .ForMember(e => e.AuthorId, opt => opt.MapFrom(p => p.Author.Code))
            .ReverseMap()
            .ForMember(p => p.Code, opt => opt.MapFrom(e => e.Id));

        CreateMap<UserEntity, UserDbEntity>()
            .ForMember(e => e.Id, opt => opt.MapFrom(p => p.Code))
            .ReverseMap()
            .ForMember(p => p.Code, opt => opt.MapFrom(e => e.Id));
    }
}
