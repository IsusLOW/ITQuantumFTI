import type { GalleryImageDto } from '../types/gallery.types.js';
import { apiClient } from '@/shared/api/apiClient.js';

export const galleryApi = {
  async getAll(): Promise<GalleryImageDto[]> {
    return apiClient.get<GalleryImageDto[]>('/v1/Gallery');
  }
};
