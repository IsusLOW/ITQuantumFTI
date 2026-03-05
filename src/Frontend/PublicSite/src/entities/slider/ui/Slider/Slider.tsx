import { useEffect, useState, useRef } from 'react';
import { sliderApi } from '@/entities/slider/api/slider.api.js';
import { preloadSlider, getCachedSlides } from '@/entities/slider/lib/slider-preload.js';
import type { SliderDto } from '@/entities/slider/types/slider.types.js';
import styles from './Slider.module.css';

interface SliderProps {
  containerId?: string;
  imageUrls?: string[];
  priority?: boolean; // Для LCP оптимизации
  inNewsCard?: boolean; // Для стилизации в карточке новости
}

export function Slider({ containerId, imageUrls, priority = false, inNewsCard = false }: SliderProps) {
  const id = containerId || `slider-${Math.random().toString(36).slice(2)}`;
  const [slides, setSlides] = useState<SliderDto[]>([]);
  const [loading, setLoading] = useState(!imageUrls);
  const [error, setError] = useState<string | null>(null);
  const [currentIndex, setCurrentIndex] = useState(0);
  const intervalRef = useRef<number | null>(null);

  useEffect(() => {
    async function loadSlides() {
      // Если есть imageUrls, используем их
      if (imageUrls) {
        const staticSlides: SliderDto[] = imageUrls.map((img, i) => ({
          id: i,
          imageUrl: img,
          title: '',
        }));
        setSlides(staticSlides);
        setLoading(false);
        return;
      }

      // Проверяем глобальный кэш
      const cached = getCachedSlides();
      if (cached && cached.length > 0) {
        setSlides(cached);
        setLoading(false);
        return;
      }

      // Загружаем из API
      try {
        const data = await preloadSlider();
        if (data.length > 0) {
          setSlides(data);
        }
      } catch (err) {
        setError('Ошибка загрузки');
        console.error('Slider loading error:', err);
      } finally {
        setLoading(false);
      }
    }

    loadSlides();
  }, [imageUrls]);

  useEffect(() => {
    if (slides.length <= 1) return;

    const startAutoPlay = () => {
      intervalRef.current = window.setInterval(() => {
        setCurrentIndex((prev) => (prev + 1) % slides.length);
      }, 5000);
    };

    const pauseAutoPlay = () => {
      if (intervalRef.current) {
        clearInterval(intervalRef.current);
        intervalRef.current = null;
      }
    };

    startAutoPlay();

    return () => {
      if (intervalRef.current) {
        clearInterval(intervalRef.current);
      }
    };
  }, [slides.length]);

  const handleNext = () => {
    setCurrentIndex((prev) => (prev + 1) % slides.length);
  };

  const handlePrev = () => {
    setCurrentIndex((prev) => (prev - 1 + slides.length) % slides.length);
  };

  if (loading) {
    return (
      <div id={id} className={`${styles.sliderRoot} slider-root`}>
        <div className={styles.sliderLoading}>
          {/* Skeleton placeholder для предотвращения CLS */}
          <div className={styles.skeletonPlaceholder}></div>
        </div>
      </div>
    );
  }

  if (error || slides.length === 0) {
    return (
      <div id={id} className={`${styles.sliderRoot} slider-root`}>
        <div className={styles.slideError}>{error || 'Нет слайдов'}</div>
      </div>
    );
  }

  // Рендерим только первый слайд сразу для LCP, остальные лениво
  return (
    <div id={id} className={`${styles.sliderRoot} slider-root`} data-in-news-card={inNewsCard || undefined}>
      <div className={styles.slider}>
        <div
          className={styles.sliderTrack}
          style={{ transform: `translateX(-${currentIndex * 100}%)` }}
        >
          {slides.map((slide, index) => {
            const isFirst = index === 0;
            return (
              <div key={slide.id} className={styles.slide}>
                {inNewsCard ? (
                  <div className="slideImageWrapper">
                    <img
                      src={slide.imageUrl}
                      alt={slide.title || 'Слайд'}
                      loading={priority && isFirst ? 'eager' : 'lazy'}
                      decoding="async"
                      fetchPriority={priority && isFirst ? 'high' : 'auto'}
                      className={`${styles.slideImage} slideImage`}
                    />
                  </div>
                ) : (
                  <img
                    src={slide.imageUrl}
                    alt={slide.title || 'Слайд'}
                    loading={priority && isFirst ? 'eager' : 'lazy'}
                    decoding="async"
                    fetchPriority={priority && isFirst ? 'high' : 'auto'}
                    className={styles.slideImage}
                  />
                )}
              </div>
            );
          })}
        </div>
        {slides.length > 1 && (
          <>
            <button
              className={`${styles.sliderBtn} ${styles.sliderPrev}`}
              onClick={handlePrev}
            >
              ‹
            </button>
            <button
              className={`${styles.sliderBtn} ${styles.sliderNext}`}
              onClick={handleNext}
            >
              ›
            </button>
            <div className={styles.sliderDots}></div>
          </>
        )}
      </div>
    </div>
  );
}
