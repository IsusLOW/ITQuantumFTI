import { Map } from "../components/map/map";
import { CourseSection } from "../components/courses/courseSection/courseSection";
import { MentorSection } from "../components/mentors/mentorSection/mentorSection";
import { NewsSection } from "../components/news/newssection/newsSection";
import { Slider } from "../components/slider/slider";

export function Home(): string {
    return `
        <section>
            ${Slider()}
            ${NewsSection()}
            ${MentorSection()}
            ${CourseSection()}
            ${Map()}
        </section>
    `;
}