export interface SliderDto {
  readonly id: number;
  readonly title: string;
  readonly imageUrl: string;
  readonly createdAt?: string;
}

export type CreateSliderDto = Omit<SliderDto, 'id' | 'createdAt'>;

export type UpdateSliderDto = Partial<CreateSliderDto>;
