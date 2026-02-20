import type { NewsDto } from "./newsTypes.js";
import { apiClient } from '@/apiClient/apiClient.js';


export const newsApi = {
  async getAll(): Promise<NewsDto[]> {
    return apiClient.get<NewsDto[]>('/news/v1/News');
  },

  async getById(id: number): Promise<NewsDto | null> {
    try {
      return await apiClient.get<NewsDto>(`/news/v1/News/${id}`);
    } catch (error) {
      // Если API не поддерживает get by id, получаем все и фильтруем
      const allNews = await this.getAll();
      return allNews.find(n => n.id === id) || null;
    }
  },

  async getNewsWithPagination(pageNumber: number, pageSize: number): Promise<NewsDto[]> {
    const params = new URLSearchParams({
      pageNumber: pageNumber.toString(),
      pageSize: pageSize.toString()
    });
    return apiClient.get<NewsDto[]>(`/news/v1/News/paged?${params}`);
  }
};
