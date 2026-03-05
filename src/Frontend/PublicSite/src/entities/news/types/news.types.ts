export interface NewsDto {
  readonly id: number;
  readonly head: string;
  readonly description: string;
  readonly author?: string;
  readonly imageUrls?: string[];
  readonly createdAt: string;
  readonly updatedAt?: string;
}

export type CreateNewsDto = Omit<NewsDto, 'id' | 'createdAt' | 'updatedAt'>;

export type UpdateNewsDto = Partial<CreateNewsDto>;
