/**
 * Конфигурация приложения
 */
export const APP_CONFIG = {
  API: {
    BASE_URL: (import.meta.env.VITE_API_URL as string) || '/api',
    TIMEOUT: Number(import.meta.env.VITE_API_TIMEOUT) || 30000,
    RETRIES: Number(import.meta.env.VITE_API_RETRIES) || 3,
  },
  CACHE: {
    TTL: Number(import.meta.env.VITE_CACHE_TTL) || 5 * 60 * 1000, // 5 минут
  },
  APP: {
    TITLE: (import.meta.env.VITE_APP_TITLE as string) || 'IT-Квантум',
    VERSION: (import.meta.env.VITE_APP_VERSION as string) || '1.0.0',
  },
  PAGINATION: {
    DEFAULT_PAGE_SIZE: 6,
    NEWS_PAGE_SIZE: 4,
    COURSES_PAGE_SIZE: 12,
  },
} as const;

/**
 * Алиасы для версий API
 */
export const API_VERSIONS = {
  V1: 'v1',
} as const;
