import type { MentorDto } from "../mentorItem/api/mentorTypes";
import {mentorApi} from "../mentorItem/api/mentorApi"
import { createMentorItem } from "../mentorItem/mentorItem";
import styles from "./mentorSection.module.css";

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
        console.error('❌ Mentor container не найден:', containerId);
        return;
    }

    try {
        console.log('🚀 Загружаем менторов для:', containerId);
        const mentors: MentorDto[] = await mentorApi.getAll();
        console.log('✅ Получено менторов:', mentors.length);

        if (!mentors.length) {
            // 🔥 ИЗМЕНИТЕ ЭТУ СТРОКУ:
            const teamSection = container.querySelector(`.${styles.teamSection}`)!;
            teamSection.innerHTML = '<div class="${styles.loading}">Нет преподавателей</div>';
            return;
        }

        // 🔥 И И ЭТУ СТРОКУ:
        const teamSection = container.querySelector(`.${styles.teamSection}`)!;
        teamSection.innerHTML = '';

        mentors.slice(0, 12).forEach(mentor => {
            const mentorCard = createMentorItem(mentor);
            teamSection.appendChild(mentorCard);
        });

        console.log('✅ Менторы отрисованы!');
    } catch (error) {
        console.error('❌ Ошибка менторов:', error);
        container.innerHTML = '<div class="${styles.loading}">Ошибка загрузки</div>';
    }
}
