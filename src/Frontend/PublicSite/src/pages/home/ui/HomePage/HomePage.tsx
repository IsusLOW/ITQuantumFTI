import { SliderSectionWidget } from '@/widgets/slider-section/ui/SliderSectionWidget/SliderSectionWidget.js';
import { NewsSectionWidget } from '@/widgets/news-section/ui/NewsSectionWidget/NewsSectionWidget.js';
import { MentorSectionWidget } from '@/widgets/mentor-section/ui/MentorSectionWidget/MentorSectionWidget.js';
import { CourseSectionWidget } from '@/widgets/course-section/ui/CourseSectionWidget/CourseSectionWidget.js';
import { GallerySectionWidget } from '@/widgets/gallery-section/ui/GallerySectionWidget/GallerySectionWidget.js';
import { MapSectionWidget } from '@/widgets/map-section/ui/MapSectionWidget/MapSectionWidget.js';
import styles from './HomePage.module.css';

export function HomePage() {
  return (
    <>
      <div className={styles.fullWidthSlider}>
        {/* priority=true для LCP оптимизации - главный слайдер загружается первым */}
        <SliderSectionWidget priority={true} />
      </div>
      <div className="container">
        <NewsSectionWidget pageSize={4} showTitle={true} />
      </div>
      <MentorSectionWidget />
      <CourseSectionWidget />
      <GallerySectionWidget />
      <div className="container">
        <MapSectionWidget />
      </div>
    </>
  );
}
