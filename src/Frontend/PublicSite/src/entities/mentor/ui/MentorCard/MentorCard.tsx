import type { MentorDto } from '@/entities/mentor/types/mentor.types.js';
import styles from './MentorCard.module.css';

interface MentorCardProps {
  mentor: MentorDto;
}

export function MentorCard({ mentor }: MentorCardProps) {
  const imgSrc = mentor.avatar && mentor.avatar !== ''
    ? mentor.avatar
    : '/images/teacher-logo.png';

  return (
    <div className={styles.teamMember}>
      <img
        className={styles.teamMember_img}
        src={imgSrc}
        alt={mentor.fullName}
      />
      <h3 className={styles.teamMember_h3}>{mentor.fullName}</h3>
      <p className={styles.teamMember_p}>
        {decodeURIComponent(mentor.description).replace(/<[^>]*>/g, '')}
      </p>
    </div>
  );
}
