import { createCourseItem } from '../courseItem/courseItem.js';
import type { CourseDto } from '@/api/courseApi/courseTypes.js';
import { courseApi } from '@/api/courseApi/courseApi.js';
import styles from './courseSection.module.css';

export function CourseSection(showTitle: boolean = true): string {
    const containerId = `courses-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" class="${styles.root}">
            <div class="container">
                ${showTitle ? `<div class="${styles.title}">Курсы IT-Квантум</div>` : ''}
                <section class="${styles.grid}">
                    <div class="${styles.loading}">Загрузка курсов...</div>
                </section>
            </div>
        </div>
    `;
}

export async function initCoursesSection(containerId: string): Promise<void> {
    const container = document.getElementById(containerId) as HTMLElement;
    if (!container) {
        return;
    }

    try {
        const courses: CourseDto[] = await courseApi.getAll();

        if (!courses.length) {
            const gridSection = container.querySelector(`.${styles.grid}`)!;
            gridSection.innerHTML = `<div class="${styles.loading}">Нет курсов</div>`;
            return;
        }

        const gridSection = container.querySelector(`.${styles.grid}`)!;
        gridSection.innerHTML = '';

        courses.slice(0, 12).forEach(course => {
            const courseCard = createCourseItem(course);
            const item = document.createElement('div');
            item.className = styles.item;
            item.appendChild(courseCard);
            gridSection.appendChild(item);
        });
    } catch (error) {
        const containerEl = container.querySelector(`.${styles.grid}`)!;
        containerEl.innerHTML = `<div class="${styles.loading}">Ошибка загрузки</div>`;
    }
}