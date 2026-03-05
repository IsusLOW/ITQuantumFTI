import { useNews } from '@/features/get-news/lib/useNews.js';
import { NewsCard } from '@/entities/news/ui/NewsCard/NewsCard.js';
import styles from './NewsSectionWidget.module.css';

interface NewsSectionWidgetProps {
  pageSize?: number; // 0 = infinite scroll
  showTitle?: boolean;
}

export function NewsSectionWidget({ pageSize = 4, showTitle = true }: NewsSectionWidgetProps) {
  const { news, loading, error, hasMore } = useNews({ pageSize });
  const showInfiniteScroll = pageSize === 0;

  return (
    <div className={`${styles.container} news-section-root`}>
      <div className={styles.newsSection}>
        {showTitle && <div className={styles.title}>Новости</div>}
        <div className={styles.grid} key={news.length}>
          {news.map((item) => (
            <NewsCard key={item.id} news={item} />
          ))}
        </div>
        {showInfiniteScroll && (
          <>
            <div
              className={styles.sentinel}
              style={{ display: loading && news.length > 0 ? 'flex' : 'none' }}
            >
              <div className={styles.loadingSpinner}></div>
              <span>Загрузка...</span>
            </div>
            {!hasMore && news.length > 0 && (
              <div className={styles.endMessage}>
                <span>Все новости загружены</span>
              </div>
            )}
          </>
        )}
        {!showInfiniteScroll && loading && news.length === 0 && (
          <div className={styles.newsLoading}>Загрузка новостей...</div>
        )}
        {error && <div className={styles.errorRetry}>{error}</div>}
      </div>
    </div>
  );
}
