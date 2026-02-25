using AutoMapper;
using CourseService.Application.DTOs;
using CourseService.Domain.Entities;

namespace CourseService.Application.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();
        }
    }
}