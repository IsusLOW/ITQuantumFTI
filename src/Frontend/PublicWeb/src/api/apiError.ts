export class ApiError extends Error {
  public readonly status: number;
  public readonly statusText: string;

  constructor(status: number, statusText: string, message?: string) {
    super(message || statusText);
    this.name = 'ApiError';
    this.status = status;
    this.statusText = statusText;
  }

  static isApiError(error: unknown): error is ApiError {
    return error instanceof ApiError;
  }
}
