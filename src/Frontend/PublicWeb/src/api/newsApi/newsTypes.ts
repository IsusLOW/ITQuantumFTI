export interface NewsDto {
  readonly id: number;
  readonly head: string;
  readonly description: string;
  readonly imageUrls: string[];
  readonly createdAt: string;
  readonly author?: string;
}

export type CreateNewsDto = Omit<NewsDto, 'id' | 'createdAt'>;

export type UpdateNewsDto = Partial<CreateNewsDto>;
