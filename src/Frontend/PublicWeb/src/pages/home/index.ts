import { Map } from "@/components/map/map.js";
import { CourseSection } from "@/components/courses/courseSection/courseSection.js";
import { MentorSection } from "@/components/mentors/mentorSection/mentorSection.js";
import { NewsSection } from "@/components/news/newssection/newsSection.js";
import { Slider } from "@/components/slider/slider.js";
import { GallerySection } from "@/components/gallery/gallerySection.js";
import styles from './home.module.css';

export function Home(): string {
  return `
    <div class="${styles.fullWidthSlider}">
      ${Slider()}
    </div>
    <div class="container">
      ${NewsSection(undefined, 4, true)}
    </div>
    ${MentorSection()}
    ${CourseSection()}
    ${GallerySection()}
    <div class="container">
      ${Map()}
    </div>
  `;
}