import { useState, useEffect, useRef, useCallback } from 'react';
import { newsApi } from '@/entities/news/api/news.api.js';
import type { NewsDto } from '@/entities/news/types/news.types.js';

interface UseNewsOptions {
  pageSize?: number; // 0 = infinite scroll
}

export function useNews(options: UseNewsOptions = {}) {
  const { pageSize = 4 } = options;
  const showInfiniteScroll = pageSize === 0;
  const actualPageSize = showInfiniteScroll ? 6 : pageSize;

  const [news, setNews] = useState<NewsDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [hasMore, setHasMore] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const currentPageRef = useRef(1);
  const allNewsRef = useRef<NewsDto[]>([]);
  const loadingRef = useRef(false);

  const loadMoreNews = useCallback(async () => {
    if (loadingRef.current || !hasMore) return;
    
    loadingRef.current = true;
    setLoading(true);
    setError(null);

    try {
      // Если это первая загрузка, получаем все новости
      if (currentPageRef.current === 1 && allNewsRef.current.length === 0) {
        const allNews = await newsApi.getAll();
        allNewsRef.current = allNews;
      }

      const start = 0;
      const end = currentPageRef.current * actualPageSize;
      const displayNews = allNewsRef.current.slice(start, end);

      setNews(displayNews);
      setHasMore(end < allNewsRef.current.length);
      currentPageRef.current += 1;
    } catch (err) {
      console.error('News loading error:', err);
      setError('Ошибка загрузки');
    } finally {
      loadingRef.current = false;
      setLoading(false);
    }
  }, [actualPageSize]);

  // Для infinite scroll на странице новостей
  useEffect(() => {
    if (!showInfiniteScroll) return;

    const handleScroll = () => {
      if (loadingRef.current || !hasMore) return;

      const scrollTop = window.scrollY;
      const windowHeight = window.innerHeight;
      const documentHeight = document.documentElement.scrollHeight;
      const remaining = documentHeight - (scrollTop + windowHeight);

      if (remaining < 200) {
        loadMoreNews();
      }
    };

    window.addEventListener('scroll', handleScroll);
    // Проверяем сразу при монтировании
    handleScroll();
    
    return () => window.removeEventListener('scroll', handleScroll);
  }, [showInfiniteScroll, hasMore, loadMoreNews]);

  // Для фиксированного размера на главной
  useEffect(() => {
    if (showInfiniteScroll) return;
    
    loadMoreNews();
  }, [showInfiniteScroll, loadMoreNews]);

  return { news, loading, error, hasMore };
}
