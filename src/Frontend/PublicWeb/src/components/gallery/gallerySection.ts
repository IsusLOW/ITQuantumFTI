import { galleryApi, type GalleryImageDto } from '@/api/galleryApi/galleryApi.js';
import styles from './gallerySection.module.css';

export function GallerySection(): string {
  const containerId = `gallery-${Math.random().toString(36).slice(2)}`;
  return `
    <div id="${containerId}" class="${styles.backgroundColor}">
      <div class="container">
        <div class="${styles.title}">Галерея IT-Квантум</div>
        <div class="${styles.gallery}" id="${containerId}-gallery">
          <div class="${styles.loading}">Загрузка галереи...</div>
        </div>
      </div>
    </div>
  `;
}

export async function initGallerySection(containerId: string): Promise<void> {
  const container = document.getElementById(containerId);
  const galleryEl = document.getElementById(`${containerId}-gallery`);
  
  if (!container || !galleryEl) {
    return;
  }

  try {
    const images: GalleryImageDto[] = await galleryApi.getAll();

    if (!images || images.length === 0) {
      galleryEl.innerHTML = `<div class="${styles.loading}">Галерея пуста</div>`;
      return;
    }

    // Берём первые 6 изображений для сетки
    const displayImages = images.slice(0, 6);
    
    // Создаём сетку: 1 большое фото + 2 маленьких
    galleryEl.innerHTML = `
      <div class="${styles.topImage}">
        <img src="${displayImages[0]?.imageUrl}" alt="${displayImages[0]?.title || 'IT-Квантум'}" loading="lazy" />
      </div>
      ${displayImages[1] ? `
      <div class="${styles.bottomLeftImage}">
        <img src="${displayImages[1].imageUrl}" alt="${displayImages[1].title || 'IT-Квантум'}" loading="lazy" />
      </div>
      ` : ''}
      ${displayImages[2] ? `
      <div class="${styles.bottomRightImage}">
        <img src="${displayImages[2].imageUrl}" alt="${displayImages[2].title || 'IT-Квантум'}" loading="lazy" />
      </div>
      ` : ''}
    `;
  } catch (error) {
    galleryEl.innerHTML = `<div class="${styles.loading}">Ошибка загрузки</div>`;
  }
}
