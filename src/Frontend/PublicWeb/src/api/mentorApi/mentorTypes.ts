export interface MentorDto {
  readonly id: number;
  readonly fullName: string;
  readonly avatar: string;
  readonly description: string;
}

export type CreateMentorDto = Omit<MentorDto, 'id'>;

export type UpdateMentorDto = Partial<CreateMentorDto>;
