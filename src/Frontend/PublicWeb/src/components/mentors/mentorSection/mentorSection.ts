import type { MentorDto } from '@/api/mentorApi/mentorTypes.js';
import { mentorApi } from '@/api/mentorApi/mentorApi.js';
import { createMentorItem } from '../mentorItem/mentorItem.js';
import styles from './mentorSection.module.css';

export function MentorSection(): string {
    const containerId = `mentor-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" class="${styles.backgroundColor}">
            <div class="container">
                <div class="${styles.title}">Наши преподаватели</div>
                <section class="${styles.teamSection}">
                    <div class="${styles.loading}">Загрузка преподавателей...</div>
                </section>
            </div>
        </div>
    `;
}

export async function initMentorSection(containerId: string): Promise<void> {
    const container = document.getElementById(containerId) as HTMLElement;
    if (!container) {
        return;
    }

    try {
        const mentors: MentorDto[] = await mentorApi.getAll();

        if (!mentors.length) {
            const teamSection = container.querySelector(`.${styles.teamSection}`)!;
            teamSection.innerHTML = '<div class="${styles.loading}">Нет преподавателей</div>';
            return;
        }

        const teamSection = container.querySelector(`.${styles.teamSection}`)!;
        teamSection.innerHTML = '';

        mentors.slice(0, 12).forEach(mentor => {
            const mentorCard = createMentorItem(mentor);
            teamSection.appendChild(mentorCard);
        });
    } catch (error) {
        container.innerHTML = '<div class="${styles.loading}">Ошибка загрузки</div>';
    }
}
