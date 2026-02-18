import { initSlider } from '../../slider/slider';
import { newsApi } from '../newsitem/api/newsApi';
import type { NewsDto } from '../newsitem/api/newsTypes';
import { NewsItem } from '../newsitem/newsitem';
import styles from './newsSection.module.css';

export function NewsSection(): string {
    const containerId = `news-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" class="${styles.container} news-section-root">
            <div class="${styles.newsSection}">
                <div class="${styles.title}">Новости</div>
                <div class="${styles.grid}">
                    <div class="${styles.newsLoading}">Загрузка новостей...</div>
                </div>
            </div>
        </div>
    `;
}


export async function initNewsSection(containerId: string): Promise<void> {
    const container = document.getElementById(containerId) as HTMLElement;
    if (!container) return;
    
    try {
        let newsItems: NewsDto[];
        try {
            newsItems = await newsApi.getNewsWithPagination(1, 4);
        } catch {
            const allNews = await newsApi.getAll();
            newsItems = allNews.slice(0, 4);
        }
        
        const limitedNews = newsItems.slice(0, 4);
        
        if (!limitedNews.length) {
            const grid = container.querySelector(`.${styles.grid}`);
            grid!.innerHTML = `<div class="${styles.newsEmpty}">Нет новостей</div>`;
            return;
        }
        
        const grid = container.querySelector(`.${styles.grid}`)!;
        grid.innerHTML = limitedNews.map(item => NewsItem(item)).join('');
        
        // 🔥 НОВОЕ: Инициализируем слайдеры в новостях!
        await initNewsSliders(container);
        
    } catch (error) {
        console.error("NewsSection error:", error);
        container.innerHTML = `<div class="news-error">Ошибка загрузки новостей</div>`;
    }
}

// 🔥 НОВАЯ функция
async function initNewsSliders(container: HTMLElement): Promise<void> {
    // Ждем DOM update
    await new Promise(r => setTimeout(r, 100));
    
    const newsSliders = container.querySelectorAll('.slider-root');
    console.log('📰 News sliders found:', newsSliders.length);
    
    for (const sliderContainer of Array.from(newsSliders)) {
        const containerEl = sliderContainer as HTMLElement;
        if (containerEl.id) {
            console.log('🎯 Init news slider:', containerEl.id);
            await initSlider(containerEl.id);
        }
    }
}