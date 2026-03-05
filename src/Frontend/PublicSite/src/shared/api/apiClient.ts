import { ApiError } from './ApiError.js';
import { APP_CONFIG } from '@/shared/constants/config.js';

class ApiClient {
  private readonly baseURL: string;
  private readonly defaultHeaders: Record<string, string>;
  private readonly cache = new Map<string, { data: unknown; timestamp: number }>();
  private readonly CACHE_TTL = APP_CONFIG.CACHE.TTL;
  private readonly MAX_RETRIES = APP_CONFIG.API.RETRIES;

  constructor(baseURL?: string) {
    this.baseURL = baseURL || APP_CONFIG.API.BASE_URL;
    this.defaultHeaders = {
      'Content-Type': 'application/json',
      'Accept': 'application/json'
    };
  }

  private async request<T>(
    method: string,
    endpoint: string,
    data?: unknown,
    options: RequestInit = {}
  ): Promise<T> {
    return this.requestWithRetry<T>(method, endpoint, data, options, this.MAX_RETRIES);
  }

  private async requestWithRetry<T>(
    method: string,
    endpoint: string,
    data?: unknown,
    options: RequestInit = {},
    retries = 3
  ): Promise<T> {
    let lastError: Error | null = null;

    for (let attempt = 0; attempt < retries; attempt++) {
      try {
        const url = this.baseURL + endpoint;
        const headers = { ...this.defaultHeaders, ...options.headers };

        const config: RequestInit = {
          method,
          headers,
          credentials: 'include',
          ...options
        };

        if (data && method !== 'GET' && method !== 'HEAD') {
          config.body = JSON.stringify(data);
        }

        const response = await fetch(url, config);

        if (response.status === 204) {
          return undefined as T;
        }

        if (!response.ok) {
          const errorText = await response.text();
          throw new ApiError(
            response.status,
            response.statusText,
            errorText || response.statusText
          );
        }

        const contentType = response.headers.get('content-type');
        let result: T;

        if (contentType && contentType.includes('application/json')) {
          result = await response.json() as T;
        } else {
          result = (await response.text()) as unknown as T;
        }

        // Кэшируем только GET запросы
        if (method === 'GET') {
          this.cache.set(endpoint, { data: result, timestamp: Date.now() });
        }

        return result;

      } catch (error) {
        lastError = error as Error;

        // Не повторяем 4xx ошибки
        if (ApiError.isApiError(error as ApiError) && (error as ApiError).status >= 400 && (error as ApiError).status < 500) {
          throw error;
        }

        // Ждём перед повторной попыткой (exponential backoff)
        if (attempt < retries - 1) {
          const delay = 1000 * (attempt + 1);
          console.warn(`API request failed, retrying in ${delay}ms... [${method} ${endpoint}]`);
          await new Promise(resolve => setTimeout(resolve, delay));
        }
      }
    }

    console.error(`API request failed after ${retries} attempts [${method} ${endpoint}]`, lastError);
    throw lastError;
  }

  public async get<T>(endpoint: string, options?: RequestInit, useCache = true): Promise<T> {
    // Проверяем кэш
    if (useCache) {
      const cached = this.cache.get(endpoint);
      if (cached && Date.now() - cached.timestamp < this.CACHE_TTL) {
        return cached.data as T;
      }
    }

    return this.request<T>('GET', endpoint, undefined, options);
  }

  public async post<T>(endpoint: string, data?: unknown, options?: RequestInit): Promise<T> {
    return this.request<T>('POST', endpoint, data, options);
  }

  public async put<T>(endpoint: string, data?: unknown, options?: RequestInit): Promise<T> {
    return this.request<T>('PUT', endpoint, data, options);
  }

  public async delete<T>(endpoint: string, options?: RequestInit): Promise<T> {
    return this.request<T>('DELETE', endpoint, undefined, options);
  }

  public setAuthToken(token: string | null): void {
    if (token) {
      this.defaultHeaders['Authorization'] = `Bearer ${token}`;
    } else {
      delete this.defaultHeaders['Authorization'];
    }
  }

  public clearCache(): void {
    this.cache.clear();
  }

  public invalidateCache(endpoint: string): void {
    this.cache.delete(endpoint);
  }
}

// Экспортируем клиент с дефолтным URL
export const apiClient = new ApiClient();
export default apiClient;
