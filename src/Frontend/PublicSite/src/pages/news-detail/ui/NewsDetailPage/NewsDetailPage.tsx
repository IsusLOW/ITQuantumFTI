import { useParams, Link } from 'react-router-dom';
import { useNewsDetail } from '@/features/view-news-detail/lib/useNewsDetail.js';
import { Slider } from '@/entities/slider/ui/Slider/Slider.js';
import styles from './NewsDetailPage.module.css';

export function NewsDetailPage() {
  const { id } = useParams<{ id: string }>();
  const { news, relatedNews, loading, error } = useNewsDetail(id);

  if (loading) {
    return (
      <div className={styles.newsDetail}>
        <div className={styles.loading}>
          <div className={styles.loadingSpinner}></div>
          <span>Загрузка новости...</span>
        </div>
      </div>
    );
  }

  if (error === 'not_found' || !news) {
    return (
      <div className="container">
        <div className={styles.notFound}>
          <h1>Такой новости не существует</h1>
          <p className={styles.notFoundText}>
            К сожалению, новость с таким ID не найдена
          </p>
          <Link to="/news" className={styles.backButton}>
            ← Вернуться к новостям
          </Link>
        </div>
      </div>
    );
  }

  if (error === 'error') {
    return (
      <div className="container">
        <div className={styles.notFound}>
          <h1>Ошибка загрузки</h1>
          <p className={styles.notFoundText}>Не удалось загрузить новость</p>
          <Link to="/news" className={styles.backButton}>
            ← Вернуться к новостям
          </Link>
        </div>
      </div>
    );
  }

  const hasMultipleImages = news.imageUrls && news.imageUrls.length > 1;
  const formattedDate = new Date(news.createdAt).toLocaleDateString('ru-RU', {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
  });

  return (
    <article className={styles.article}>
      <div className="container">
        {/* Hero Image / Slider */}
        <div className={styles.heroImage}>
          <div className={styles.heroImageContainer}>
            {hasMultipleImages ? (
              <Slider imageUrls={news.imageUrls} priority={true} />
            ) : news.imageUrls?.[0] ? (
              <img
                src={news.imageUrls[0]}
                alt={news.head}
                className={styles.heroImageImg}
                loading="eager"
                fetchPriority="high"
                decoding="async"
              />
            ) : (
              <div className={styles.heroImagePlaceholder}></div>
            )}
          </div>
        </div>

        <div className={styles.contentWrapper}>
          {/* Header */}
          <header className={styles.header}>
            <div className={styles.meta}>
              <span className={styles.category}>Новости</span>
              <span className={styles.date}>{formattedDate}</span>
            </div>

            <h1 className={styles.headline}>{news.head}</h1>

            <div className={styles.authorRow}>
              <div className={styles.author}>
                <span className={styles.authorLabel}>Автор</span>
                <span className={styles.authorName}>{news.author || 'IT-Квантум'}</span>
              </div>
              <div className={styles.shareButtons}>
                <button className={styles.shareButton} aria-label="Поделиться в Facebook">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M18 2h-3a5 5 0 0 0-5 5v3H7v4h3v8h4v-8h3l1-4h-4V7a1 1 0 0 1 1-1h3z" />
                  </svg>
                </button>
                <button className={styles.shareButton} aria-label="Поделиться в Twitter">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M23 3a10.9 10.9 0 0 1-3.14 1.53 4.48 4.48 0 0 0-7.86 3v1A10.66 10.66 0 0 1 3 4s-4 9 5 13a11.64 11.64 0 0 1-7 2c9 5 20 0 20-11.5a4.5 4.5 0 0 0-.08-.83A7.72 7.72 0 0 0 23 3z" />
                  </svg>
                </button>
                <button className={styles.shareButton} aria-label="Копировать ссылку">
                  <svg
                    width="20"
                    height="20"
                    viewBox="0 0 24 24"
                    fill="none"
                    stroke="currentColor"
                    strokeWidth="2"
                  >
                    <path d="M10 13a5 5 0 0 0 7.54.54l3-3a5 5 0 0 0-7.07-7.07l-1.72 1.71" />
                    <path d="M14 11a5 5 0 0 0-7.54-.54l-3 3a5 5 0 0 0 7.07 7.07l1.71-1.71" />
                  </svg>
                </button>
              </div>
            </div>
          </header>

          {/* Main Content */}
          <div className={styles.main}>
            <div className={styles.body}>
              <p className={styles.description}>{news.description}</p>
            </div>
          </div>

          {/* Related News */}
          {relatedNews.length > 0 && (
            <section className={styles.related}>
              <h2 className={styles.relatedTitle}>Читайте также</h2>
              <div className={styles.relatedGrid}>
                {relatedNews.map((item) => (
                  <Link
                    to={`/news/${item.id}`}
                    key={item.id}
                    className={styles.relatedCard}
                  >
                    {item.imageUrls?.[0] ? (
                      <div className={styles.relatedCardImage}>
                        <img
                          src={item.imageUrls[0]}
                          alt={item.head}
                          loading="lazy"
                          decoding="async"
                        />
                      </div>
                    ) : null}
                    <div className={styles.relatedCardContent}>
                      <h3 className={styles.relatedCardTitle}>{item.head}</h3>
                      <time className={styles.relatedCardDate}>
                        {new Date(item.createdAt).toLocaleDateString('ru-RU', {
                          day: 'numeric',
                          month: 'short',
                        })}
                      </time>
                    </div>
                  </Link>
                ))}
              </div>
            </section>
          )}
        </div>
      </div>
    </article>
  );
}
