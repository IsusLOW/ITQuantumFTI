export interface MentorDto {
  readonly id: number;
  readonly fullName: string;
  readonly avatar?: string;
  readonly description: string;
  readonly createdAt?: string;
}

export type CreateMentorDto = Omit<MentorDto, 'id' | 'createdAt'>;

export type UpdateMentorDto = Partial<CreateMentorDto>;
