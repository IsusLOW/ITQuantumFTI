import { NewsSection } from '@/components/news/newssection/NewsSection.js';

export function News() {
  return (
    <section className="container">
      <NewsSection pageSize={0} showTitle={false} />
    </section>
  );
}
