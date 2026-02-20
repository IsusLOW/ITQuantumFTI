import './style.css';
import { Header } from './components/header/header.ts';
import { Footer } from './components/footer/footer.ts';
import { Home } from './pages/home';
import { News } from './pages/news';
import { Courses } from './pages/courses';
import { Schedulers } from './pages/schedulers';
import { Aboutus } from './pages/aboutus';
import { NewsDetailPage, initNewsDetailPage } from './pages/newsDetail';
import { ROUTES } from '@/constants/routes';
import { preloadSlider, initSlider } from './components/slider/slider.ts';
import { initNewsSection } from './components/news/newssection/newsSection.ts';
import { initMentorSection } from './components/mentors/mentorSection/mentorSection.ts';
import { initGallerySection } from './components/gallery/gallerySection.ts';
import headerStyles from './components/header/header.module.css';
import { initCoursesSection } from './components/courses/courseSection/courseSection.ts';
import { initMap } from './components/map/map.ts';
//http://localhost:5173/aboutus
// Предварительная загрузка слайдера — сразу при загрузке модуля
const sliderPreloadPromise = preloadSlider();

const app = document.getElementById('app')!;
const headerEl = document.getElementById('persistent-header')!;
const footerEl = document.getElementById('persistent-footer')!;

// Разовая отрисовка
let currentPath = window.location.pathname;
headerEl.innerHTML = Header({ activePath: currentPath });
footerEl.innerHTML = Footer({ activePath: currentPath });

// Объект страниц
const pages: Record<string, () => string> = {
  [ROUTES.HOME]: Home,
  [ROUTES.NEWS]: News,
  [ROUTES.COURSES]: Courses,
  [ROUTES.SCHEDULERS]: Schedulers,
  [ROUTES.ABOUT]: Aboutus
};

// Проверка маршрута новости /news/:id
function getNewsIdFromPath(path: string): string | null {
  const match = path.match(/^\/news\/(\d+)$/);
  return match ? match[1] : null;
}

async function initHomeComponents() {
  // 1. Слайдер — самый высокий приоритет (ждём preloaded данные)
  const mainSlider = document.querySelector('.slider-root:not(.news-slider-root)') as HTMLElement;
  if (mainSlider?.id) {
    // Ждём preloaded данные (уже готовы или скоро будут)
    await sliderPreloadPromise;
    initSlider(mainSlider.id);
  }

  // 2. NewsSection — 4 новости (параллельно, не блокируем)
  const newsContainers = document.querySelectorAll('.news-section-root');
  for (const container of Array.from(newsContainers)) {
    const containerEl = container as HTMLElement;
    if (containerEl.id && !containerEl.closest('[id^="news-page-root"]')) {
      initNewsSection(containerEl.id, 4); // Не ждём
    }
  }

  // 3. Остальные компоненты с задержкой (не критично)
  setTimeout(async () => {
    const mentorContainers = document.querySelectorAll('[id^="mentor-"]');
    for (const container of Array.from(mentorContainers)) {
      const id = (container as HTMLElement).id;
      await initMentorSection(id);
    }
  }, 100);

  setTimeout(async () => {
    const galleryContainers = document.querySelectorAll('[id^="gallery-"]');
    for (const container of Array.from(galleryContainers)) {
      const id = (container as HTMLElement).id;
      await initGallerySection(id);
    }
  }, 150);

  setTimeout(async () => {
    const courseContainers = document.querySelectorAll('[id^="courses-"]');
    for (const container of Array.from(courseContainers)) {
      const id = (container as HTMLElement).id;
      await initCoursesSection(id);
    }
  }, 200);

  setTimeout(async () => {
    const mapContainers = document.querySelectorAll('[id^="map-"]');
    for (const container of Array.from(mapContainers)) {
      const id = (container as HTMLElement).id;
      await initMap(id);
    }
  }, 300);
}

async function initNewsComponents() {
  // Страница новостей - infinite scroll (pageSize=0)
  await initNewsSection('news-page-root', 0);
}

async function initCoursesComponents() {
  // Страница курсов - загружаем все курсы
  const coursesContainer = document.querySelector('[id^="courses-"]') as HTMLElement;
  if (coursesContainer?.id) {
    await initCoursesSection(coursesContainer.id);
  }
}

// Очистка при уходе со страницы новостей
function cleanupNewsPage() {
  const newsContainer = document.getElementById('news-page-root');
  if (newsContainer) {
    // Очищаем scroll listener
    const scrollListener = (newsContainer as any)._scrollListener;
    if (scrollListener) {
      window.removeEventListener('scroll', scrollListener);
    }
  }
}

async function router() {
  const path = window.location.pathname;
  
  // Очистка перед сменой страницы
  if (path !== '/news' && !path.startsWith('/news/')) {
    cleanupNewsPage();
  }
  
  // Прокрутка наверх при смене страницы
  window.scrollTo({ top: 0, behavior: 'auto' });
  
  updateActiveClasses(path);

  // Проверка маршрута новости
  const newsId = getNewsIdFromPath(path);
  
  if (newsId) {
    // Страница новости /news/:id
    app.innerHTML = NewsDetailPage({ id: newsId });
    await initNewsDetailPage('news-detail-page', newsId);
  } else {
    // Обычные страницы
    const pageContent = pages[path]?.() || '<h1 style="color: #0c2867; text-align: center; padding: 50px;">404</h1>';
    app.innerHTML = pageContent;

    // Инициализация компонентов для текущей страницы
    if (path === '/') {
      await initHomeComponents();
    } else if (path === '/news') {
      await initNewsComponents();
    } else if (path === '/courses') {
      await initCoursesComponents();
    }
  }
}

function updateActiveClasses(path: string) {
  const currentHeaderLinks = headerEl.querySelectorAll('a[href]') as NodeListOf<HTMLAnchorElement>;
  const currentFooterLinks = footerEl.querySelectorAll('a[href]') as NodeListOf<HTMLAnchorElement>;

  // Header — используем CSS-модульный класс
  currentHeaderLinks.forEach(link => {
    link.classList.remove(headerStyles.navLinkSelected);
    if (link.getAttribute('href') === path) {
      link.classList.add(headerStyles.navLinkSelected);
    }
  });

  // Footer — обычный класс
  currentFooterLinks.forEach(link => {
    link.classList.remove('active');
    const href = link.getAttribute('href');
    if (href === path ||
      (href === '/course' && path === '/courses') ||
      (href === '/schedulers' && path === '/schedule') ||
      (href === '/aboutus' && path === '/about')) {
      link.classList.add('active');
    }
  });
}

// SPA navigation
document.addEventListener('click', (e: MouseEvent) => {
  const a = (e.target as HTMLElement).closest('a[href]');
  if (a instanceof HTMLAnchorElement &&
    a.hostname === location.hostname &&
    !a.hasAttribute('target') &&
    !a.href.startsWith('mailto') &&
    !a.href.startsWith('tel:')) {
    e.preventDefault();
    history.pushState({}, '', a.href);
    router();
  }
});

window.addEventListener('popstate', router);
router();
