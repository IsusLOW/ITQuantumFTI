import { initSlider } from '@/components/slider/slider.js';
import { newsApi } from '@/api/newsApi/newsApi.js';
import type { NewsDto } from '@/api/newsApi/newsTypes.js';
import { NewsItem } from '../newsitem/newsitem.js';
import styles from './newsSection.module.css';

interface NewsSectionState {
  currentPage: number;
  pageSize: number;
  isLoading: boolean;
  hasMore: boolean;
  allNews: NewsDto[];
}

const state: Map<string, NewsSectionState> = new Map();

/**
 * @param containerId - ID контейнера (опционально)
 * @param pageSize - Количество новостей (0 = бесконечная прокрутка, 4 = главная)
 * @param showTitle - Показывать заголовок "Новости"
 */
export function NewsSection(containerId?: string, pageSize: number = 4, showTitle: boolean = true): string {
  const id = containerId || `news-${Math.random().toString(36).slice(2)}`;
  const showInfiniteScroll = pageSize === 0;

  return `
    <div id="${id}" class="${styles.container} news-section-root">
      <div class="${styles.newsSection}">
        ${showTitle ? `<div class="${styles.title}">Новости</div>` : ''}
        <div class="${styles.grid}" id="${id}-grid">
          <div class="${styles.newsLoading}">Загрузка новостей...</div>
        </div>
        ${showInfiniteScroll ? `
        <div id="${id}-sentinel" class="${styles.sentinel}">
          <div class="${styles.loadingSpinner}"></div>
          <span>Загрузка...</span>
        </div>
        <div id="${id}-end" class="${styles.endMessage}" style="display: none;">
          <span>Все новости загружены</span>
        </div>
        ` : ''}
      </div>
    </div>
  `;
}

/**
 * @param containerId - ID контейнера
 * @param pageSize - 0 для infinite scroll, >0 для фиксированного количества
 */
export async function initNewsSection(containerId: string, pageSize: number = 4): Promise<void> {
  const container = document.getElementById(containerId);
  const grid = document.getElementById(`${containerId}-grid`);

  if (!container || !grid) {
    return;
  }

  const showInfiniteScroll = pageSize === 0;

  // Инициализация состояния
  state.set(containerId, {
    currentPage: 0,
    pageSize: showInfiniteScroll ? 6 : pageSize,
    isLoading: false,
    hasMore: true,
    allNews: []
  });

  // Загружаем первую порцию новостей
  await loadMoreNews(containerId, showInfiniteScroll);

  // Используем window scroll event вместо Intersection Observer
  if (showInfiniteScroll) {
    const handleScroll = () => {
      const sectionState = state.get(containerId);
      if (!sectionState || sectionState.isLoading || !sectionState.hasMore) {
        return;
      }

      // Проверяем насколько прокрутили
      const scrollTop = window.scrollY;
      const windowHeight = window.innerHeight;
      const documentHeight = document.documentElement.scrollHeight;
      
      // Если до конца страницы осталось меньше 200px
      const remaining = documentHeight - (scrollTop + windowHeight);
      
      if (remaining < 200) {
        loadMoreNews(containerId, true);
      }
    };

    // Добавляем listener
    window.addEventListener('scroll', handleScroll);
    (container as any)._scrollListener = handleScroll;
    
    // Проверяем сразу
    handleScroll();
  }
}

async function loadMoreNews(containerId: string, showInfiniteScroll: boolean = false): Promise<void> {
  const sectionState = state.get(containerId);
  if (!sectionState || sectionState.isLoading) {
    return;
  }

  // Для не-infinite scroll проверяем, загружено ли уже всё
  if (!showInfiniteScroll && sectionState.currentPage > 0) {
    return;
  }

  sectionState.isLoading = true;
  updateLoadingState(containerId, true);

  try {
    const nextPage = sectionState.currentPage + 1;
    let newNews: NewsDto[];

    try {
      newNews = await newsApi.getNewsWithPagination(nextPage, sectionState.pageSize);
    } catch (error) {
      // Fallback к getAll если пагинация не работает
      const allNews = await newsApi.getAll();
      const start = (nextPage - 1) * sectionState.pageSize;
      newNews = allNews.slice(start, start + sectionState.pageSize);
    }

    if (newNews.length === 0) {
      sectionState.hasMore = false;
      if (showInfiniteScroll) {
        showEndMessage(containerId, true);
      }
    } else {
      sectionState.allNews = [...sectionState.allNews, ...newNews];
      sectionState.currentPage = nextPage;
      
      // Рендерим новости
      renderNews(containerId, sectionState.allNews);
      
      // Ждём пока DOM обновится
      await new Promise(r => setTimeout(r, 200));
      
      // Инициализируем слайдеры
      await initNewsSliders(containerId);

      // Для не-infinite scroll ограничиваем pageSize
      if (!showInfiniteScroll && sectionState.allNews.length >= sectionState.pageSize) {
        sectionState.hasMore = false;
      }

      // Проверяем, есть ли ещё данные
      if (showInfiniteScroll && newNews.length < sectionState.pageSize) {
        sectionState.hasMore = false;
        showEndMessage(containerId, true);
      }
    }
  } catch (error) {
    if (showInfiniteScroll) {
      showError(containerId);
    }
  } finally {
    sectionState.isLoading = false;
    updateLoadingState(containerId, false);
  }
}

function renderNews(containerId: string, news: NewsDto[]): void {
  const grid = document.getElementById(`${containerId}-grid`);
  if (!grid) return;

  grid.innerHTML = news.map(item => NewsItem(item)).join('');
}

function updateLoadingState(containerId: string, isLoading: boolean): void {
  const sentinel = document.getElementById(`${containerId}-sentinel`);
  if (sentinel) {
    sentinel.style.display = isLoading ? 'flex' : 'none';
  }
}

function showEndMessage(containerId: string, show: boolean): void {
  const endMessage = document.getElementById(`${containerId}-end`);
  if (endMessage) {
    endMessage.style.display = show ? 'flex' : 'none';
  }
}

function showError(containerId: string): void {
  const sentinel = document.getElementById(`${containerId}-sentinel`);
  if (sentinel) {
    sentinel.innerHTML = `
      <div class="${styles.errorRetry}">
        <span>Ошибка загрузки</span>
        <button class="${styles.retryButton}" onclick="window.retryLoadNews('${containerId}')">
          Повторить
        </button>
      </div>
    `;
  }
}

async function initNewsSliders(containerId: string): Promise<void> {
  await new Promise(r => setTimeout(r, 100));

  const container = document.getElementById(containerId);
  if (!container) return;

  const newsSliders = container.querySelectorAll('.slider-root');

  for (const sliderContainer of Array.from(newsSliders)) {
    const containerEl = sliderContainer as HTMLElement;
    if (containerEl.id) {
      await initSlider(containerEl.id);
    }
  }
}

// Глобальная функция для повторной загрузки
(window as any).retryLoadNews = async (containerId: string) => {
  const sectionState = state.get(containerId);
  if (sectionState) {
    sectionState.hasMore = true;
    await loadMoreNews(containerId);
  }
};

// Очистка при уничтожении
export function cleanupNewsSection(containerId: string): void {
  const container = document.getElementById(containerId);
  if (container && (container as any)._newsObserver) {
    (container as any)._newsObserver.disconnect();
    state.delete(containerId);
  }
}
