import { useState, useEffect } from 'react';
import { getAllNews } from '@/entities/news/lib/news-preload.js';
import type { NewsDto } from '@/entities/news/types/news.types.js';

export function useNewsDetail(id: string | undefined) {
  const [news, setNews] = useState<NewsDto | null>(null);
  const [relatedNews, setRelatedNews] = useState<NewsDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadNews() {
      if (!id) return;

      try {
        // Используем кэш для быстрой загрузки
        const allNews = await getAllNews();
        const foundNews = allNews.find((n) => n.id === parseInt(id));

        if (!foundNews) {
          setError('not_found');
          setLoading(false);
          return;
        }

        setNews(foundNews);

        const currentIndex = allNews.findIndex((n) => n.id === foundNews.id);
        const related = allNews.slice(currentIndex + 1, currentIndex + 4);
        setRelatedNews(related);
      } catch (err) {
        console.error('Error loading news detail:', err);
        setError('error');
      } finally {
        setLoading(false);
      }
    }

    loadNews();
  }, [id]);

  return { news, relatedNews, loading, error };
}
