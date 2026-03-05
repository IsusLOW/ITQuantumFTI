import { useState, useEffect } from 'react';
import { galleryApi } from '@/entities/gallery/api/gallery.api.js';
import type { GalleryImageDto } from '@/entities/gallery/types/gallery.types.js';

export function useGallery(limit?: number) {
  const [images, setImages] = useState<GalleryImageDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadGallery() {
      try {
        const data = await galleryApi.getAll();
        setImages(limit ? data.slice(0, limit) : data);
      } catch (err) {
        setError('Ошибка загрузки');
        console.error('Gallery loading error:', err);
      } finally {
        setLoading(false);
      }
    }

    loadGallery();
  }, [limit]);

  return { images, loading, error };
}
