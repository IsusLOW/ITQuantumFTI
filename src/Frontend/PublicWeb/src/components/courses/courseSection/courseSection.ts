import { createCourseItem } from '../courseItem/courseItem';
import type { CourseDto } from './api/courseTypes';
import { courseApi } from './api/courseApi';
import styles from './courseSection.module.css';

export function CourseSection(): string {
    const containerId = `courses-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" class="${styles.root}">
            <div class="container">
                <div class="${styles.title}">Курсы IT-Квантум</div>
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
        console.error('❌ Courses container не найден:', containerId);
        return;
    }

    try {
        console.log('🚀 Загружаем курсы для:', containerId);
        const courses: CourseDto[] = await courseApi.getAll();
        console.log('✅ Получено курсов:', courses.length);

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

        console.log('✅ Курсы отрисованы!');
    } catch (error) {
        console.error('❌ Ошибка курсов:', error);
        const containerEl = container.querySelector(`.${styles.grid}`)!;
        containerEl.innerHTML = `<div class="${styles.loading}">Ошибка загрузки</div>`;
    }
}