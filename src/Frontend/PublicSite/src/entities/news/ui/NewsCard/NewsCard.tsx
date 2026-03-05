import { Link } from 'react-router-dom';
import type { NewsDto } from '@/entities/news/types/news.types.js';
import { Slider } from '@/entities/slider/ui/Slider/Slider.js';
import styles from './NewsCard.module.css';

interface NewsCardProps {
  news: NewsDto;
}

export function NewsCard({ news }: NewsCardProps) {
  const hasMultipleImages = news.imageUrls?.length > 1;
  const smallDesc = news.description?.substring(0, 200) + '...';

  return (
    <section className={styles.grid}>
      <article className={styles.gridItem}>
        <div className={styles.gridItem_image}>
          {hasMultipleImages ? (
            <Slider inNewsCard imageUrls={news.imageUrls} containerId={`news-slider-${news.id}`} />
          ) : news.imageUrls?.[0] ? (
            <img
              loading="lazy"
              decoding="async"
              className={styles.gridItem_image_img}
              src={news.imageUrls[0]}
              alt={news.head}
              width="400"
              height="300"
            />
          ) : null}
        </div>
        <div className={styles.gridItem_info}>
          <h2 className={styles.gridItem_info_h2}>
            <Link to={`/news/${news.id}`}>{news.head}</Link>
          </h2>
          <div className={styles.newsDescription}>{smallDesc}</div>
          <div className={styles.gridItem_buttonWrap}>
            <Link className={styles.atuinBtn} to={`/news/${news.id}`}>
              Подробнее
            </Link>
          </div>
        </div>
      </article>
    </section>
  );
}
