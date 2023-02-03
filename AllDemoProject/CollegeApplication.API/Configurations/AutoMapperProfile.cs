using AutoMapper;
using CollegeApplication.Core.Domain.ResponseModel;
using CollegeApplication.Infra.Domain.Entity;
using CollegeApplication.Shared;

namespace CollegeApplication.API.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PagedList<Student>, PagedList<StudentResponseModel>>();
            CreateMap<Student, StudentResponseModel>();
        }
    }
}
