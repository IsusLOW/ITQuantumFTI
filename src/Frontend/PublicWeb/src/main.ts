import './style.css'
import { Header } from './components/header/header.ts';
import { Footer } from './components/footer/footer.ts';
import { Home } from './pages/home.ts';
import { News } from './pages/news.ts';
import { Courses } from './pages/courses.ts';
import { Schedulers } from './pages/schedulers.ts';
import { Aboutus } from './pages/aboutus.ts';
import { initSlider } from './components/slider/slider.ts'; // 🔥 добавь импорт
import { initNewsSection } from './components/news/newssection/newsSection.ts';
import { initMentorSection } from './components/mentors/mentorSection/mentorSection.ts';
import headerStyles from './components/header/header.module.css';
import { initCoursesSection } from './components/courses/courseSection/courseSection.ts';
import { initMap } from './components/map/map.ts';

const app = document.getElementById('app')!;
const headerEl = document.getElementById('persistent-header')!;
const footerEl = document.getElementById('persistent-footer')!;

// Разовая отрисовка
let currentPath = window.location.pathname;
headerEl.innerHTML = Header({ activePath: currentPath });
footerEl.innerHTML = Footer({ activePath: currentPath });



// ✅ ПРОСТОЙ объект - все функции возвращают string
const pages: Record<string, () => string> = {
    '/': Home,
    '/news': News,
    '/courses': Courses,
    '/schedulers': Schedulers,
    '/aboutus': Aboutus
};

async function router() { // ✅ async router
    const path = window.location.pathname;
    updateActiveClasses(path);
    
    const pageContent = pages[path]?.() || '<h1 style="color: #0c2867; text-align: center; padding: 50px;">404</h1>';
    app.innerHTML = `<main>${pageContent}</main>`;
    
    // 🔥 Инициализируем слайдер только на главной
    if (path === '/') {
      initHomeComponents();
    }
}

async function initHomeComponents() {
  // 🔥 NewsSection сама инициализирует свои слайдеры
  document.querySelectorAll('.news-section-root').forEach(async (container) => {
    const containerEl = container as HTMLElement;
    if (containerEl.id) {
        await initNewsSection(containerEl.id); // Теперь включает слайдеры!
    }
  });

  setTimeout(async () => {
    console.log('⏰ МЕНТОРЫ Таймаут...');
    const mentorContainers = document.querySelectorAll('[id^="mentor-"]');
    
    if (mentorContainers.length === 0) {
        console.error('❌ МЕНТОРЫ НЕ НАЙДЕНЫ!');
        return;
    }
    
    setTimeout(async () => {
        const mapContainers = document.querySelectorAll('[id^="map-"]');
        for (const container of Array.from(mapContainers)) {
          const id = (container as HTMLElement).id;
          console.log('🗺️ Map найден:', id);
          await initMap(id);
        }
      }, 400);

    setTimeout(async () => {
        const courseContainers = document.querySelectorAll('[id^="courses-"]');
        for (const container of Array.from(courseContainers)) {
          const id = (container as HTMLElement).id;
          console.log('🎓 Курсы найдены:', id);
          await initCoursesSection(id);
        }
      }, 300);
    
    for (const container of Array.from(mentorContainers)) {
        const id = (container as HTMLElement).id;
        console.log('🎯 МЕНТОР НАЙДЕН:', id);
        await initMentorSection(id);
        }
    }, 200); // ждем рендер DOM

    // 🔥 Главный слайдер (только 1 на главной)
    setTimeout(() => {
    const mainSlider = document.querySelector('.slider-root:not(.news-slider-root)') as HTMLElement;
    if (mainSlider?.id) {
        console.log('🏠 Main slider:', mainSlider.id);
        initSlider(mainSlider.id);
        }
    }, 100);
}

function updateActiveClasses(path: string) {
    const currentHeaderLinks = headerEl.querySelectorAll('a[href]') as NodeListOf<HTMLAnchorElement>;
    const currentFooterLinks = footerEl.querySelectorAll('a[href]') as NodeListOf<HTMLAnchorElement>;

    // 🔥 Header — используем CSS-модульный класс!
    currentHeaderLinks.forEach(link => {
        link.classList.remove(headerStyles.navLinkSelected);
        if (link.getAttribute('href') === path) {
            link.classList.add(headerStyles.navLinkSelected);
        }
    });
    
    // Footer — обычный класс (или тоже импортируйте)
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
