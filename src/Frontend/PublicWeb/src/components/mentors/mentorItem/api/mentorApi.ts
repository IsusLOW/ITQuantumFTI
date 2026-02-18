import type { MentorDto } from './mentorTypes';
import {apiClient} from '../../../../apiClient/apiClient';


export const mentorApi = {
    async getAll(): Promise<MentorDto[]> {
        return apiClient.get<MentorDto[]>('/mentor/v1/Mentor');
    }
}
