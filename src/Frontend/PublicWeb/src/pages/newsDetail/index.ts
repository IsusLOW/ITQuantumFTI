import styles from './index.module.css';
import { SliderNewsItem } from '@/components/slider/slider.js';

export interface NewsDetailPageProps {
  id: string;
}

export function NewsDetailPage(props: NewsDetailPageProps): string {
  return `
    <div class="${styles.newsDetail}" id="news-detail-page" data-news-id="${props.id}">
      <div class="${styles.loading}">
        <div class="${styles.loadingSpinner}"></div>
        <span>Загрузка новости...</span>
      </div>
    </div>
  `;
}

export async function initNewsDetailPage(containerId: string, newsId: string): Promise<void> {
  const container = document.getElementById(containerId);
  if (!container) {
    console.error('News detail container not found');
    return;
  }

  try {
    const { newsApi } = await import('@/api/newsApi/newsApi.js');
    
    const allNews = await newsApi.getAll();
    const news = allNews.find(n => n.id === parseInt(newsId));

    if (!news) {
      container.innerHTML = `
        <div class="container">
          <div class="${styles.notFound}">
            <h1>Такой новости не существует</h1>
            <p class="${styles.notFoundText}">К сожалению, новость с таким ID не найдена</p>
            <a href="/news" class="${styles.backButton}">← Вернуться к новостям</a>
          </div>
        </div>
      `;
      return;
    }

    // Получаем связанные новости (следующие 3 после текущей)
    const currentIndex = allNews.findIndex(n => n.id === news.id);
    const relatedNews = allNews.slice(currentIndex + 1, currentIndex + 4);

    container.innerHTML = renderNewsDetail(news, relatedNews);
    
    if (news.imageUrls && news.imageUrls.length > 1) {
      await initSliders(container);
    }
  } catch (error) {
    console.error('Error loading news detail:', error);
    container.innerHTML = `
      <div class="container">
        <div class="${styles.notFound}">
          <h1>Ошибка загрузки</h1>
          <p class="${styles.notFoundText}">Не удалось загрузить новость</p>
          <a href="/news" class="${styles.backButton}">← Вернуться к новостям</a>
        </div>
      </div>
    `;
  }
}

function renderNewsDetail(news: any, relatedNews: any[]): string {
  const hasMultipleImages = news.imageUrls && news.imageUrls.length > 1;
  const formattedDate = new Date(news.createdAt).toLocaleDateString('ru-RU', {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  });

  return `
    <article class="${styles.article}">
      <div class="container">
        <!-- Hero Image / Slider -->
        <div class="${styles.heroImage}">
          <div class="${styles.heroImageContainer}">
            ${hasMultipleImages 
              ? SliderNewsItem(news.imageUrls)
              : news.imageUrls?.[0]
                ? `<img src="${news.imageUrls[0]}" alt="${news.head}" class="${styles.heroImageImg}" loading="eager" />`
                : '<div class="${styles.heroImagePlaceholder}"></div>'
            }
          </div>
        </div>

        <div class="${styles.contentWrapper}">
          <!-- Header -->
          <header class="${styles.header}">
            <div class="${styles.meta}">
              <span class="${styles.category}">Новости</span>
              <span class="${styles.date}">${formattedDate}</span>
            </div>

            <h1 class="${styles.headline}">${news.head}</h1>

            <div class="${styles.authorRow}">
              <div class="${styles.author}">
                <span class="${styles.authorLabel}">Автор</span>
                <span class="${styles.authorName}">${news.author || 'IT-Квантум'}</span>
              </div>
              <div class="${styles.shareButtons}">
                <button class="${styles.shareButton}" aria-label="Поделиться в Facebook">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M18 2h-3a5 5 0 0 0-5 5v3H7v4h3v8h4v-8h3l1-4h-4V7a1 1 0 0 1 1-1h3z"/>
                  </svg>
                </button>
                <button class="${styles.shareButton}" aria-label="Поделиться в Twitter">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M23 3a10.9 10.9 0 0 1-3.14 1.53 4.48 4.48 0 0 0-7.86 3v1A10.66 10.66 0 0 1 3 4s-4 9 5 13a11.64 11.64 0 0 1-7 2c9 5 20 0 20-11.5a4.5 4.5 0 0 0-.08-.83A7.72 7.72 0 0 0 23 3z"/>
                  </svg>
                </button>
                <button class="${styles.shareButton}" aria-label="Копировать ссылку">
                  <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                    <path d="M10 13a5 5 0 0 0 7.54.54l3-3a5 5 0 0 0-7.07-7.07l-1.72 1.71"/>
                    <path d="M14 11a5 5 0 0 0-7.54-.54l-3 3a5 5 0 0 0 7.07 7.07l1.71-1.71"/>
                  </svg>
                </button>
              </div>
            </div>
          </header>

          <!-- Main Content -->
          <div class="${styles.main}">
            <div class="${styles.body}">
              <p class="${styles.description}">${news.description}</p>
            </div>
          </div>

          <!-- Related News -->
          ${relatedNews.length > 0 ? `
            <section class="${styles.related}">
              <h2 class="${styles.relatedTitle}">Читайте также</h2>
              <div class="${styles.relatedGrid}">
                ${relatedNews.map(item => `
                  <a href="/news/${item.id}" class="${styles.relatedCard}">
                    ${item.imageUrls?.[0] 
                      ? `<div class="${styles.relatedCardImage}"><img src="${item.imageUrls[0]}" alt="${item.head}" loading="lazy" /></div>`
                      : ''
                    }
                    <div class="${styles.relatedCardContent}">
                      <h3 class="${styles.relatedCardTitle}">${item.head}</h3>
                      <time class="${styles.relatedCardDate}">${new Date(item.createdAt).toLocaleDateString('ru-RU', { day: 'numeric', month: 'short' })}</time>
                    </div>
                  </a>
                `).join('')}
              </div>
            </section>
          ` : ''}
        </div>
      </div>
    </article>
  `;
}

async function initSliders(container: HTMLElement): Promise<void> {
  const { initSlider } = await import('@/components/slider/slider.js');
  await new Promise(r => setTimeout(r, 100));
  
  const sliderContainers = container.querySelectorAll('.slider-root');
  for (const sliderContainer of Array.from(sliderContainers)) {
    const sliderEl = sliderContainer as HTMLElement;
    if (sliderEl.id) {
      await initSlider(sliderEl.id);
    }
  }
}
