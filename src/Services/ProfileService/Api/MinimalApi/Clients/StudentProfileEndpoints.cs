using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Application.DTOs.Clients;
using ProfileService.Application.Services.Clients;

namespace ProfileService.Api.MinimalApi.Clients
{
    public static class StudentProfileEndpoints 
    {
        public static void MapStudentProfileEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/Student").WithOpenApi();

            group.MapGet("/list", GetStudentProfiles).WithOpenApi();
            group.MapGet("{id:int}", GetStudentProfileById);
            group.MapPost("", CreateStudent);
            group.MapPut("{id:int}", UpdateStudent);
            group.MapDelete("{id:int}", DeleteStudent);
        }

        private static async Task<IResult> GetStudentProfiles(IStudentProfileService service)
        {
            var students = await service.GetProfilesAsync();
            return Results.Ok(students);
        }

        private static async Task<IResult> GetStudentProfileById(int id, IStudentProfileService service)
        {
            var student = await service.GetProfileByIdAsync(id);
            return Results.Ok(student);
        }

        private static async Task<IResult> CreateStudent(CreateStudentProfileDto dto, IStudentProfileService service)
        {
            var student = await service.CreateProfileAsync(dto);
            return Results.Ok(student);
        }

        private static async Task<IResult> UpdateStudent(int id, UpdateStudentProfileDto dto, IStudentProfileService service)
        {
            var updatedStudent = await service.UpdateProfileAsync(id, dto);
            return Results.Ok(updatedStudent);
        }

        private static async Task<IResult> DeleteStudent(int id, IStudentProfileService service)
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        }
    }
}