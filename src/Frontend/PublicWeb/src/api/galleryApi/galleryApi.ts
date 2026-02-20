import { apiClient } from '@/apiClient/apiClient.js';

export interface GalleryImageDto {
  readonly id: number;
  readonly imageUrl: string;
  readonly title?: string;
  readonly order: number;
  readonly createdAt: string;
}

export const galleryApi = {
  async getAll(): Promise<GalleryImageDto[]> {
    return apiClient.get<GalleryImageDto[]>('/gallery/v1/Gallery');
  }
};
