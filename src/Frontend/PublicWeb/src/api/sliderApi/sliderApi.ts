import { apiClient } from '@/apiClient/apiClient.js';
import type { SliderDto, CreateSliderDto, UpdateSliderDto } from './sliderTypes.js';

export const sliderApi = {
  async getAll(): Promise<SliderDto[]> {
    return apiClient.get<SliderDto[]>('/slider/v1/Slider');
  },

  async create(slide: CreateSliderDto): Promise<SliderDto> {
    const result = await apiClient.post<SliderDto>('/slider/v1/Slider', slide);
    apiClient.invalidateCache('/slider/v1/Slider');
    return result;
  },

  async update(id: number, slide: UpdateSliderDto): Promise<SliderDto> {
    const result = await apiClient.put<SliderDto>(`/slider/v1/Slider/${id}`, slide);
    apiClient.invalidateCache('/slider/v1/Slider');
    return result;
  },

  async delete(id: number): Promise<void> {
    await apiClient.delete<void>(`/slider/v1/Slider/${id}`);
    apiClient.invalidateCache('/slider/v1/Slider');
  }
};
