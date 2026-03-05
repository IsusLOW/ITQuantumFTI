/**
 * Маршруты приложения
 */
export const ROUTES = {
  HOME: '/',
  NEWS: '/news',
  COURSES: '/courses',
  SCHEDULERS: '/schedulers',
  ABOUT: '/aboutus'
} as const;

export type RoutePath = typeof ROUTES[keyof typeof ROUTES];

/**
 * Маршрут новости с параметром
 */
export function getNewsDetailRoute(id: number): string {
  return `/news/${id}`;
}

/**
 * Проверка валидности маршрута
 */
export function isValidRoute(path: string): path is RoutePath {
  return Object.values(ROUTES).includes(path as RoutePath);
}

/**
 * Получить все маршруты
 */
export function getAllRoutes(): RoutePath[] {
  return Object.values(ROUTES);
}
