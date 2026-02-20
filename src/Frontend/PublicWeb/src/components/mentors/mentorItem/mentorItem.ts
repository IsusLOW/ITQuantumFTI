import type { MentorDto } from '@/api/mentorApi/mentorTypes.js';
import styles from './mentorItem.module.css';

export function createMentorItem(mentor: MentorDto): HTMLElement {
    const div = document.createElement('div');
    div.className = styles.teamMember;
    
    const imgSrc = mentor.avatar && mentor.avatar !== '' 
        ? mentor.avatar 
        : '/images/teacher-logo.png';
    
    div.innerHTML = `
        <img class="${styles.teamMember_img}" src="${imgSrc}" alt="${mentor.fullName}"/>
        <h3 class="${styles.teamMember_h3}">${mentor.fullName}</h3>
        <p class="${styles.teamMember_p}">${decodeURIComponent(mentor.description).replace(/<[^>]*>/g, '')}</p>
    `;
    
    return div;
}
