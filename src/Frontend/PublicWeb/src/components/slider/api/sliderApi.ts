import {apiClient} from '../../../apiClient/apiClient'
import type { SliderDto } from './sliderTypes';

export const sliderApi = {
    async getAll(): Promise<SliderDto[]> {
        return apiClient.get<SliderDto[]>('/slider/v1/Slider');
    },
}
