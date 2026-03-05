import { Routes, Route } from 'react-router-dom';
import { Header } from '@/widgets/header/ui/Header/Header.js';
import { Footer } from '@/widgets/footer/ui/Footer/Footer.js';
import { HomePage } from '@/pages/home/ui/HomePage/HomePage.js';
import { NewsPage } from '@/pages/news/ui/NewsPage/NewsPage.js';
import { NewsDetailPage } from '@/pages/news-detail/ui/NewsDetailPage/NewsDetailPage.js';
import { CoursesPage } from '@/pages/courses/ui/CoursesPage/CoursesPage.js';
import { SchedulersPage } from '@/pages/schedulers/ui/SchedulersPage/SchedulersPage.js';
import { AboutusPage } from '@/pages/aboutus/ui/AboutusPage/AboutusPage.js';
import { NotFoundPage } from '@/pages/not-found/ui/NotFoundPage/NotFoundPage.js';

function App() {
  return (
    <>
      <div id="persistent-header">
        <Header />
      </div>
      <main>
        <div id="app">
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/news" element={<NewsPage />} />
            <Route path="/news/:id" element={<NewsDetailPage />} />
            <Route path="/courses" element={<CoursesPage />} />
            <Route path="/schedulers" element={<SchedulersPage />} />
            <Route path="/aboutus" element={<AboutusPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </div>
      </main>
      <div id="persistent-footer">
        <Footer />
      </div>
    </>
  );
}

export default App;
