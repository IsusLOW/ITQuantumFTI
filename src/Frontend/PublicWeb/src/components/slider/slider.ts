import styles from './slider.module.css';
import { sliderApi } from '@/api/sliderApi/sliderApi.js';
import type { SliderDto } from './sliderTypes.js';

// Кэш для слайдов
let cachedSlides: SliderDto[] | null = null;
let cachePromise: Promise<SliderDto[]> | null = null;

export function Slider(): string {
  const containerId = `slider-${Math.random().toString(36).slice(2)}`;
  return `
    <div id="${containerId}" class="${styles.sliderRoot} slider-root">
      <div class="${styles.sliderLoading}">Загрузка...</div>
    </div>
  `;
}

export function SliderNewsItem(imageUrls: string[]): string {
  const containerId = `news-slider-${Math.random().toString(36).slice(2)}`;
  return `
    <div id="${containerId}"
         class="${styles.sliderRoot} slider-root"
         data-newsitem-slider="true"
         data-static-images="${imageUrls.join('|')}">
      <div class="${styles.sliderLoading}">Загрузка...</div>
    </div>
  `;
}

/**
 * Предварительная загрузка слайдов (pre-fetch)
 */
export async function preloadSlider(): Promise<SliderDto[]> {
  if (cachedSlides) return cachedSlides;
  
  if (!cachePromise) {
    cachePromise = sliderApi.getAll()
      .then(slides => {
        cachedSlides = slides;
        return slides;
      })
      .catch(error => {
        console.error('Slider preload error:', error);
        cachePromise = null;
        return [];
      });
  }
  
  return cachePromise;
}

/**
 * Инициализация слайдера
 */
export async function initSlider(containerId: string): Promise<void> {
  const container = document.getElementById(containerId);
  if (!container) {
    console.error('❌ Container NOT FOUND:', containerId);
    return;
  }

  const dataAttr = container.getAttribute('data-static-images');

  try {
    let slides: SliderDto[];

    if (dataAttr) {
      // Статические изображения из data-атрибута
      const images = dataAttr.split('|');
      slides = images.map((img, i) => ({
        id: i,
        imageUrl: img,
        title: ''
      } as SliderDto));
      
      // Рендерим сразу
      renderSlider(container, slides);
    } else {
      // Используем кэш или загружаем
      if (cachedSlides) {
        slides = cachedSlides;
        renderSlider(container, slides);
      } else {
        // Показываем лоадер пока ждём API
        const slides = await sliderApi.getAll();
        cachedSlides = slides;
        renderSlider(container, slides);
      }
    }
  } catch (error) {
    console.error("Slider error:", error);
    container.innerHTML = `<div class="${styles.slideError}">Ошибка загрузки</div>`;
  }
}

/**
 * Рендер слайдера
 */
function renderSlider(container: HTMLElement, slides: SliderDto[]): void {
  if (!slides?.length) {
    container.innerHTML = `<div class="${styles.slideEmpty}">Нет слайдов</div>`;
    return;
  }

  // Рендер HTML
  container.innerHTML = createSliderHTML(slides);

  // Инициализация логики
  initSliderLogic(container, slides);
}

function createSliderHTML(slides: SliderDto[]): string {
  return `
    <div class="${styles.slider}">
      <div class="${styles.sliderTrack}">
        ${slides.map((slide) => `
          <div class="${styles.slide}">
            <img src="${slide.imageUrl}" alt="${slide.title || 'Слайд'}" loading="eager"/>
          </div>
        `).join('')}
      </div>
      <button class="${styles.sliderBtn} ${styles.sliderPrev}">‹</button>
      <button class="${styles.sliderBtn} ${styles.sliderNext}">›</button>
      <div class="${styles.sliderDots}"></div>
    </div>
  `;
}

function initSliderLogic(container: HTMLElement, slides: SliderDto[]) {
  let index = 0;
  let intervalId = 0;

  const track = container.querySelector(`.${styles.sliderTrack}`) as HTMLElement;

  function updateSlider() {
    track.style.transform = `translateX(-${index * 100}%)`;
  }

  const nextBtn = container.querySelector(`.${styles.sliderNext}`) as HTMLButtonElement | null;
  const prevBtn = container.querySelector(`.${styles.sliderPrev}`) as HTMLButtonElement | null;

  const handleNext = () => {
    index = (index + 1) % slides.length;
    updateSlider();
    resumeAutoPlay();
  };
  
  const handlePrev = () => {
    index = (index - 1 + slides.length) % slides.length;
    updateSlider();
    resumeAutoPlay();
  };

  if (nextBtn) nextBtn.addEventListener('click', handleNext);
  if (prevBtn) prevBtn.addEventListener('click', handlePrev);

  const slider = container.querySelector(`.${styles.slider}`) as HTMLElement;
  slider.addEventListener('mouseenter', pauseAutoPlay);
  slider.addEventListener('mouseleave', resumeAutoPlay);

  function startAutoPlay() {
    if (slides.length <= 1) return;
    intervalId = window.setInterval(handleNext, 5000);
  }

  function pauseAutoPlay() {
    if (intervalId) {
      clearInterval(intervalId);
      intervalId = 0;
    }
  }

  function resumeAutoPlay() {
    if (!intervalId && slides.length > 1) startAutoPlay();
  }

  updateSlider();
  startAutoPlay();
}
