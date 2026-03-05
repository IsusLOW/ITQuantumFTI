import { NewsSectionWidget } from '@/widgets/news-section/ui/NewsSectionWidget/NewsSectionWidget.js';

export function NewsPage() {
  return (
    <section className="container">
      <NewsSectionWidget pageSize={0} showTitle={false} />
    </section>
  );
}
