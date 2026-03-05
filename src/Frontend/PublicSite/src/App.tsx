import { Routes, Route } from 'react-router-dom';
import { Header } from './components/header/Header';
import { Footer } from './components/footer/Footer';
import { Home } from './pages/home/Home';
import { News } from './pages/news/News';
import { Courses } from './pages/courses/Courses';
import { Schedulers } from './pages/schedulers/Schedulers';
import { Aboutus } from './pages/aboutus/Aboutus';
import { NewsDetailPage } from './pages/newsDetail/NewsDetailPage';

function App() {
  return (
    <>
      <div id="persistent-header">
        <Header />
      </div>
      <main>
        <div id="app">
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/news" element={<News />} />
            <Route path="/news/:id" element={<NewsDetailPage />} />
            <Route path="/courses" element={<Courses />} />
            <Route path="/schedulers" element={<Schedulers />} />
            <Route path="/aboutus" element={<Aboutus />} />
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
