import type { CourseDto } from "../courseSection/api/courseTypes";
import './courseItem.css'

export function createCourseItem(course: CourseDto): HTMLElement {
  const link = document.createElement('a');
  link.className = 'hexagon'; // 🔥 обычный класс
  link.href = `/courses/${course.id}`;
  link.textContent = course.name;
  link.setAttribute('aria-label', course.description || course.name);
  return link;
}