import type { MentorDto, CreateMentorDto, UpdateMentorDto } from './mentorTypes.js';
import { apiClient } from '@/apiClient/apiClient.js';

export const mentorApi = {
  async getAll(): Promise<MentorDto[]> {
    return apiClient.get<MentorDto[]>('/mentor/v1/Mentor');
  },

  async getById(id: number): Promise<MentorDto> {
    return apiClient.get<MentorDto>(`/mentor/v1/Mentor/${id}`);
  },

  async create(mentor: CreateMentorDto): Promise<MentorDto> {
    const result = await apiClient.post<MentorDto>('/mentor/v1/Mentor', mentor);
    apiClient.invalidateCache('/mentor/v1/Mentor');
    return result;
  },

  async update(id: number, mentor: UpdateMentorDto): Promise<MentorDto> {
    const result = await apiClient.put<MentorDto>(`/mentor/v1/Mentor/${id}`, mentor);
    apiClient.invalidateCache('/mentor/v1/Mentor');
    return result;
  },

  async delete(id: number): Promise<void> {
    await apiClient.delete<void>(`/mentor/v1/Mentor/${id}`);
    apiClient.invalidateCache('/mentor/v1/Mentor');
  }
};
