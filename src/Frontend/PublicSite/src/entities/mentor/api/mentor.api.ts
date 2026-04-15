import type { MentorDto } from '../types/mentor.types.js';
import { apiClient } from '@/shared/api/apiClient.js';

export const mentorApi = {
  async getAll(): Promise<MentorDto[]> {
    return apiClient.get<MentorDto[]>('/v1/Mentor');
  }
};
