import type { NewsDto } from '../types/news.types.js';
import { newsApi } from '../api/news.api.js';

// Глобальный кэш для новостей
interface NewsCache {
  allNews: NewsDto[];
  loaded: boolean;
  timestamp: number;
}

const NEWS_CACHE_TTL = 5 * 60 * 1000; // 5 минут
let newsCache: NewsCache | null = null;
let loadPromise: Promise<NewsDto[]> | null = null;

/**
 * Предзагрузка новостей - вызывается при инициализации приложения
 */
export function preloadNews(pageSize: number = 6): Promise<NewsDto[]> {
  console.log('[News Preload] Cache check:', newsCache?.loaded);
  
  if (newsCache && newsCache.loaded && Date.now() - newsCache.timestamp < NEWS_CACHE_TTL) {
    console.log('[News Preload] Returning cached:', newsCache.allNews.length);
    return Promise.resolve(newsCache.allNews);
  }

  if (!loadPromise) {
    console.log('[News Preload] Fetching from API...');
    loadPromise = newsApi.getAll()
      .then(news => {
        console.log('[News Preload] Loaded:', news.length, 'news');
        newsCache = {
          allNews: news,
          loaded: true,
          timestamp: Date.now()
        };
        return news;
      })
      .catch(error => {
        console.error('[News Preload] ERROR:', error);
        loadPromise = null;
        return [];
      });
  }

  return loadPromise;
}

/**
 * Получение кэшированных новостей
 */
export function getCachedNews(): NewsDto[] {
  return newsCache?.allNews || [];
}

/**
 * Загрузка следующей страницы новостей
 */
export async function loadMoreNewsFromCache(page: number, pageSize: number): Promise<{ news: NewsDto[]; hasMore: boolean }> {
  // Ждём если загрузка ещё идёт
  if (loadPromise && (!newsCache || !newsCache.loaded)) {
    await loadPromise;
  }

  if (!newsCache || !newsCache.allNews.length) {
    return { news: [], hasMore: false };
  }

  const start = (page - 1) * pageSize;
  const end = start + pageSize;
  const news = newsCache.allNews.slice(start, end);

  return {
    news,
    hasMore: end < newsCache.allNews.length
  };
}

/**
 * Получение всех новостей (для detail страницы)
 */
export async function getAllNews(): Promise<NewsDto[]> {
  if (newsCache && newsCache.allNews.length > 0 && Date.now() - newsCache.timestamp < NEWS_CACHE_TTL) {
    return newsCache.allNews;
  }
  
  try {
    const allNews = await newsApi.getAll();
    newsCache = {
      allNews,
      loadedPages: Math.ceil(allNews.length / 6),
      hasMore: false,
      timestamp: Date.now()
    };
    return allNews;
  } catch (error) {
    console.error('Get all news error:', error);
    return newsCache?.allNews || [];
  }
}
