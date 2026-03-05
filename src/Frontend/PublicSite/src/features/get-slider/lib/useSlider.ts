import { useState, useEffect, useRef } from 'react';
import { sliderApi } from '@/entities/slider/api/slider.api.js';
import type { SliderDto } from '@/entities/slider/types/slider.types.js';

interface UseSliderOptions {
  imageUrls?: string[];
  autoPlay?: boolean;
  autoPlayInterval?: number;
}

export function useSlider(options: UseSliderOptions = {}) {
  const { imageUrls, autoPlay = true, autoPlayInterval = 5000 } = options;
  const [slides, setSlides] = useState<SliderDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [currentIndex, setCurrentIndex] = useState(0);
  const intervalRef = useRef<number | null>(null);

  useEffect(() => {
    async function loadSlides() {
      try {
        if (imageUrls) {
          const staticSlides: SliderDto[] = imageUrls.map((img, i) => ({
            id: i,
            imageUrl: img,
            title: '',
          }));
          setSlides(staticSlides);
        } else {
          const data = await sliderApi.getAll();
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
    if (!autoPlay || slides.length <= 1) return;

    intervalRef.current = window.setInterval(() => {
      setCurrentIndex((prev) => (prev + 1) % slides.length);
    }, autoPlayInterval);

    return () => {
      if (intervalRef.current) {
        clearInterval(intervalRef.current);
      }
    };
  }, [autoPlay, autoPlayInterval, slides.length]);

  const handleNext = () => {
    setCurrentIndex((prev) => (prev + 1) % slides.length);
  };

  const handlePrev = () => {
    setCurrentIndex((prev) => (prev - 1 + slides.length) % slides.length);
  };

  return { slides, loading, error, currentIndex, handleNext, handlePrev };
}
