using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Application.Services.Shared
{
    public interface IProfileService<TReadDto, TCreateDto, TUpdateDto>
    {
        Task<IReadOnlyList<TReadDto>> GetProfilesAsync();
        Task<TReadDto> GetProfileByIdAsync(int id);
        Task<TReadDto> CreateProfileAsync(TCreateDto dto);
        Task<TReadDto> UpdateProfileAsync(int id, TUpdateDto dto);
        Task DeleteAsync(int id);
    }
}