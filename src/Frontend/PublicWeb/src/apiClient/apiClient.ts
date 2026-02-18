class ApiClient {
    private baseURL: string;
    private defaultHeaders: Record<string, string>;

    constructor(baseURL?: string) {
        // Просто используем переданный URL или дефолтный
        this.baseURL = baseURL || '/api';
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

        try {
            const response = await fetch(url, config);

            if (response.status === 204) {
                return undefined as T;
            }

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(`HTTP ${response.status}: ${errorText || response.statusText}`);
            }

            const contentType = response.headers.get('content-type');
            if (contentType && contentType.includes('application/json')) {
                return await response.json() as T;
            }

            return (await response.text()) as unknown as T;
        } catch (error) {
            console.error(`API Request failed [${method} ${endpoint}]:`, error);
            throw error;
        }
    }

    public async get<T>(endpoint: string, options?: RequestInit): Promise<T> {
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
}

// Экспортируем клиент с дефолтным URL
export const apiClient = new ApiClient();
export default apiClient;