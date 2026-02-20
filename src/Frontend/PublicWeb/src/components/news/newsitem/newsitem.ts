import { SliderNewsItem } from '@/components/slider/slider.js';
import type { NewsDto } from '@/api/newsApi/newsTypes.js';
import styles from './newsitem.module.css';

export function NewsItem(newsItem: NewsDto): string {

    const hasMultipleImages = newsItem.imageUrls?.length > 1;
    const smallDesc = newsItem.description.substring(0, 200) + '...';

    return `
        <section class="${styles.grid}">
            <article class="${styles.gridItem}">
                <div class="${styles.gridItem_image}">
                    ${hasMultipleImages 
                        ? SliderNewsItem(newsItem.imageUrls)  // ← Наш слайдер!
                        : newsItem.imageUrls?.[0] 
                          ? `<img loading="lazy" decoding="async" class="${styles.gridItem_image_img}" src="${newsItem.imageUrls[0]}" />`
                          : ''
                    }
                </div>
                <div class="${styles.gridItem_info}">
                    <h2 class="${styles.gridItem_info_h2}">
                        <a href="/news/${newsItem.id}">${newsItem.head}</a>
                    </h2>
                    <div class="${styles.newsDescription}">${smallDesc}</div>
                    <div class="${styles.gridItem_buttonWrap}">
                        <a class="${styles.atuinBtn}" href="/news/${newsItem.id}">Подробнее</a>
                    </div>
                </div>
            </article>
        </section>
    `;
}
