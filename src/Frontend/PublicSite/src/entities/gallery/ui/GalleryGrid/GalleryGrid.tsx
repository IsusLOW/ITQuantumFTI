import { useEffect, useState } from 'react';
import { galleryApi } from '@/entities/gallery/api/gallery.api.js';
import type { GalleryImageDto } from '@/entities/gallery/types/gallery.types.js';
import styles from './GalleryGrid.module.css';

export function GalleryGrid() {
  const [images, setImages] = useState<GalleryImageDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadGallery() {
      try {
        const data = await galleryApi.getAll();
        setImages(data.slice(0, 6));
      } catch (err) {
        setError('Ошибка загрузки');
        console.error('Gallery loading error:', err);
      } finally {
        setLoading(false);
      }
    }

    loadGallery();
  }, []);

  if (loading) {
    return (
      <div className={styles.backgroundColor}>
        <div className="container">
          <div className={styles.title}>Галерея IT-Квантум</div>
          <div className={styles.gallery}>
            <div className={styles.loading}>Загрузка галереи...</div>
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className={styles.backgroundColor}>
        <div className="container">
          <div className={styles.title}>Галерея IT-Квантум</div>
          <div className={styles.gallery}>
            <div className={styles.loading}>{error}</div>
          </div>
        </div>
      </div>
    );
  }

  if (images.length === 0) {
    return (
      <div className={styles.backgroundColor}>
        <div className="container">
          <div className={styles.title}>Галерея IT-Квантум</div>
          <div className={styles.gallery}>
            <div className={styles.loading}>Галерея пуста</div>
          </div>
        </div>
      </div>
    );
  }

  const displayImages = images;

  return (
    <div className={styles.backgroundColor}>
      <div className="container">
        <div className={styles.title}>Галерея IT-Квантум</div>
        <div className={styles.gallery}>
          <div className={styles.topImage}>
            <img
              src={displayImages[0]?.imageUrl}
              alt={displayImages[0]?.title || 'IT-Квантум'}
              loading="lazy"
              decoding="async"
              className={styles.galleryImage}
            />
          </div>
          {displayImages[1] && (
            <div className={styles.bottomLeftImage}>
              <img
                src={displayImages[1].imageUrl}
                alt={displayImages[1].title || 'IT-Квантум'}
                loading="lazy"
                decoding="async"
                className={styles.galleryImage}
              />
            </div>
          )}
          {displayImages[2] && (
            <div className={styles.bottomRightImage}>
              <img
                src={displayImages[2].imageUrl}
                alt={displayImages[2].title || 'IT-Квантум'}
                loading="lazy"
                decoding="async"
                className={styles.galleryImage}
              />
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
