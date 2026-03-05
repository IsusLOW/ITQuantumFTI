import type { CourseDto } from '@/entities/course/types/course.types.js';
import styles from './CourseCard.module.css';

interface CourseCardProps {
  course: CourseDto;
}

export function CourseCard({ course }: CourseCardProps) {
  return (
    <a className={styles.hexagon} href={`/courses/${course.id}`} aria-label={course.description || course.name}>
      {course.name}
    </a>
  );
}
