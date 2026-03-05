/**
 * Базовый DTO для всех сущностей
 */
export interface BaseDto {
  readonly id: number;
}

/**
 * Ответ API с пагинацией
 */
export interface PagedResponse<T> {
  readonly items: T[];
  readonly pageNumber: number;
  readonly pageSize: number;
  readonly totalCount: number;
  readonly totalPages: number;
  readonly hasPreviousPage: boolean;
  readonly hasNextPage: boolean;
}

/**
 * Параметры пагинации
 */
export interface PaginationParams {
  pageNumber: number;
  pageSize: number;
}

/**
 * Статусы загрузки
 */
export type LoadingState = 'idle' | 'loading' | 'success' | 'error';

/**
 * Результат API запроса
 */
export interface ApiResult<T> {
  data: T | null;
  error: string | null;
  isLoading: boolean;
}
