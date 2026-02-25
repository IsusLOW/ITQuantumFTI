using MentorService.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorService.Application.Services
{
    public interface IMentorAppService
    {
        Task<IReadOnlyList<MentorDto>> GetMentorsAsync();
        Task<MentorDto> GetMentorByIdAsync(int id);
        Task<MentorDto> CreateMentorAsync(CreateMentorDto dto);
        Task<MentorDto> UpdateMentorAsync(int id, UpdateMentorDto dto);
        Task DeleteMentorAsync(int id);
    }
}
