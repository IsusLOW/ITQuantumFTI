import type { SliderDto } from '../types/slider.types.js';
import { apiClient } from '@/shared/api/apiClient.js';

export const sliderApi = {
  async getAll(): Promise<SliderDto[]> {
    return apiClient.get<SliderDto[]>('/v1/Slider');
  }
};
