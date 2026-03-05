export interface CourseDto {
  readonly id: number;
  readonly name: string;
  readonly description?: string;
  readonly createdAt?: string;
  readonly updatedAt?: string;
}

export type CreateCourseDto = Omit<CourseDto, 'id' | 'createdAt' | 'updatedAt'>;

export type UpdateCourseDto = Partial<CreateCourseDto>;
