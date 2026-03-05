import { useCourses } from '@/features/get-courses/lib/useCourses.js';
import { CourseCard } from '@/entities/course/ui/CourseCard/CourseCard.js';
import styles from '@/widgets/course-section/ui/CourseSectionWidget/CourseSectionWidget.module.css';

interface CourseSectionWidgetProps {
  showTitle?: boolean;
}

export function CourseSectionWidget({ showTitle = true }: CourseSectionWidgetProps) {
  const { courses, loading, error } = useCourses(12);

  if (loading) {
    return (
      <div className={styles.root}>
        <div className="container">
          {showTitle && <div className={styles.title}>Курсы IT-Квантум</div>}
          <section className={styles.grid}>
            <div className={styles.loading}>Загрузка курсов...</div>
          </section>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className={styles.root}>
        <div className="container">
          {showTitle && <div className={styles.title}>Курсы IT-Квантум</div>}
          <section className={styles.grid}>
            <div className={styles.loading}>{error}</div>
          </section>
        </div>
      </div>
    );
  }

  if (courses.length === 0) {
    return (
      <div className={styles.root}>
        <div className="container">
          {showTitle && <div className={styles.title}>Курсы IT-Квантум</div>}
          <section className={styles.grid}>
            <div className={styles.loading}>Нет курсов</div>
          </section>
        </div>
      </div>
    );
  }

  return (
    <div className={styles.root}>
      <div className="container">
        {showTitle && <div className={styles.title}>Курсы IT-Квантум</div>}
        <section className={styles.grid}>
          {courses.map((course) => (
            <div key={course.id} className={styles.item}>
              <CourseCard course={course} />
            </div>
          ))}
        </section>
      </div>
    </div>
  );
}
