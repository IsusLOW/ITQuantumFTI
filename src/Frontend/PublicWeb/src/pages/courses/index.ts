import { CourseSection } from "@/components/courses/courseSection/courseSection.js";

export function Courses(): string {
  return `
    <section>
      ${CourseSection(false)}
    </section>
  `;
}