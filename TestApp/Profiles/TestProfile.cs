using AutoMapper;
using TestApp.Domain.Models;
using TestApp.DTOs;

namespace TestApp.Profiles
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<Test, TestDto>().PreserveReferences();
            //CreateMap<Test, TestDto>().MaxDepth(1);
        }
    }
}
