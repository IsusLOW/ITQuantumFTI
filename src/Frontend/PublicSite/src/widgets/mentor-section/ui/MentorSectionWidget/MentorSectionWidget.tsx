import { useMentors } from '@/features/get-mentors/lib/useMentors.js';
import { MentorCard } from '@/entities/mentor/ui/MentorCard/MentorCard.js';
import styles from './MentorSectionWidget.module.css';

export function MentorSectionWidget() {
  const { mentors, loading, error } = useMentors(12);

  if (loading) {
    return (
      <div className={styles.backgroundColor}>
        <div className="container">
          <div className={styles.title}>Наши преподаватели</div>
          <section className={styles.teamSection}>
            <div className={styles.loading}>Загрузка преподавателей...</div>
          </section>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className={styles.backgroundColor}>
        <div className="container">
          <div className={styles.title}>Наши преподаватели</div>
          <section className={styles.teamSection}>
            <div className={styles.loading}>{error}</div>
          </section>
        </div>
      </div>
    );
  }

  if (mentors.length === 0) {
    return (
      <div className={styles.backgroundColor}>
        <div className="container">
          <div className={styles.title}>Наши преподаватели</div>
          <section className={styles.teamSection}>
            <div className={styles.loading}>Нет преподавателей</div>
          </section>
        </div>
      </div>
    );
  }

  return (
    <div className={styles.backgroundColor}>
      <div className="container">
        <div className={styles.title}>Наши преподаватели</div>
        <section className={styles.teamSection}>
          {mentors.map((mentor) => (
            <MentorCard key={mentor.id} mentor={mentor} />
          ))}
        </section>
      </div>
    </div>
  );
}
