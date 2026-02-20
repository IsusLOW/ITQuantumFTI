export interface CourseDto {
  readonly id: number;
  readonly name: string;
  readonly description: string;
  readonly workProg: string;
  readonly imageUrls: string[];
}

export type CreateCourseDto = Omit<CourseDto, 'id'>;

export type UpdateCourseDto = Partial<CreateCourseDto>;
