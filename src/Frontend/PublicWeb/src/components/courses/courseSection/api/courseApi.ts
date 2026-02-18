import type { CourseDto } from "./courseTypes";
import {apiClient} from '../../../../apiClient/apiClient';


export const courseApi = {
    async getAll(): Promise<CourseDto[]> {
        return apiClient.get<CourseDto[]>('/course/v1/Course');
    }
}
