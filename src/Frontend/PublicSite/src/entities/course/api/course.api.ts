import type { CourseDto, CreateCourseDto, UpdateCourseDto } from '../types/course.types.js';
import { apiClient } from '@/shared/api/apiClient.js';

export const courseApi = {
  async getAll(): Promise<CourseDto[]> {
    return apiClient.get<CourseDto[]>('/v1/Course');
  },

  async getById(id: number): Promise<CourseDto> {
    return apiClient.get<CourseDto>(`/v1/Course/${id}`);
  },

  async create(course: CreateCourseDto): Promise<CourseDto> {
    const result = await apiClient.post<CourseDto>('/v1/Course', course);
    apiClient.invalidateCache('/v1/Course');
    return result;
  },

  async update(id: number, course: UpdateCourseDto): Promise<CourseDto> {
    const result = await apiClient.put<CourseDto>(`/v1/Course/${id}`, course);
    apiClient.invalidateCache('/v1/Course');
    return result;
  },

  async delete(id: number): Promise<void> {
    await apiClient.delete<void>(`/v1/Course/${id}`);
    apiClient.invalidateCache('/v1/Course');
  }
};
