import type { NewsDto } from "./newsTypes";
import {apiClient} from '../../../../apiClient/apiClient';


export const newsApi = {
    async getAll(): Promise<NewsDto[]> {
        return apiClient.get<NewsDto[]>('/news/v1/News');
    },

    async getNewsWithPagination(pageNumber: number, pageSize: number): Promise<NewsDto[]> {
        const params = new URLSearchParams({ pageNumber: pageNumber.toString(), pageSize: pageSize.toString() });
        return apiClient.get<NewsDto[]>(`/news/v1/News?${params}`);
    }
}
