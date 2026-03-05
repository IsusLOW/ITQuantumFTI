import { Map } from '@/components/map/Map.js';
import { CourseSection } from '@/components/courses/courseSection/CourseSection.js';
import { MentorSection } from '@/components/mentors/mentorSection/MentorSection.js';
import { NewsSection } from '@/components/news/newssection/NewsSection.js';
import { Slider } from '@/components/slider/Slider.js';
import { GallerySection } from '@/components/gallery/GallerySection.js';
import styles from './home.module.css';

export function Home() {
  return (
    <>
      <div className={styles.fullWidthSlider}>
        <Slider />
      </div>
      <div className="container">
        <NewsSection pageSize={4} showTitle={true} />
      </div>
      <MentorSection />
      <CourseSection />
      <GallerySection />
      <div className="container">
        <Map />
      </div>
    </>
  );
}
