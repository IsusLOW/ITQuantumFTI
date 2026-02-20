import { NewsSection } from "@/components/news/newssection/newsSection.js";

export function News(): string {
  return `
    <section class="container">
      ${NewsSection('news-page-root', 0, false)}
    </section>
  `;
}