import type { SliderDto } from '../types/slider.types.js';
import { sliderApi } from '../api/slider.api.js';

// Глобальный кэш для слайдера
let cachedSlides: SliderDto[] | null = null;
let loadPromise: Promise<SliderDto[]> | null = null;

/**
 * Предзагрузка слайдера - вызывается при инициализации приложения
 */
export function preloadSlider(): Promise<SliderDto[]> {
  if (cachedSlides) return Promise.resolve(cachedSlides);
  
  if (!loadPromise) {
    loadPromise = sliderApi.getAll()
      .then(slides => {
        cachedSlides = slides;
        return slides;
      })
      .catch(error => {
        console.error('Slider preload error:', error);
        loadPromise = null;
        return [];
      });
  }
  
  return loadPromise;
}

/**
 * Получение слайдов с использованием кэша
 */
export function getCachedSlides(): SliderDto[] | null {
  return cachedSlides;
}
