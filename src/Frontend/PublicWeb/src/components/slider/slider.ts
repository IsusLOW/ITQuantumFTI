import styles from './slider.module.css';
import { sliderApi } from './api/sliderApi';
import type { SliderDto } from './api/sliderTypes';

export function Slider(): string {
    const containerId = `slider-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" class="${styles.sliderRoot} slider-root">  <!-- ✅ класс -->
            <div class="${styles.sliderLoading}">Загрузка...</div>
        </div>
    `;
}

export function SliderNewsItem(imageUrls: string[]): string {
    const containerId = `news-slider-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" 
             class="${styles.sliderRoot} slider-root" 
             data-newsitem-slider="true"  /* 🔥 МАРКЕР */
             data-static-images="${imageUrls.join('|')}">
            <div class="${styles.sliderLoading}">Загрузка...</div>
        </div>
    `;
}

/**
 * ВЫЗЫВАЙ ЭТО после рендера HTML!
 */
export async function initSlider(containerId: string): Promise<void> {
    console.log('🚀 initSlider START:', containerId);

    const container = document.getElementById(containerId) as HTMLElement;
    if (!container) {
        console.error('❌ Container NOT FOUND:', containerId);
        return;
    }

    try {
        const dataAttr = container.getAttribute('data-static-images');
        console.log('🔍 dataAttr:', dataAttr);
        
        let slides: SliderDto[];
        
        if (dataAttr) {
            console.log('🔥 NewsItem slider detected');
            const images = dataAttr.split('|');
            slides = images.map((img, i) => ({
                id: i,
                imageUrl: img,
                title: ''
            } as SliderDto));
        } else {
            console.log('🔥 Main slider — loading API');
            slides = await sliderApi.getAll();
        }
        
        if (!slides?.length) {
            container.innerHTML = `<div class="${styles.slideEmpty}">Нет слайдов</div>`;
            return;
        }
        
        // 🔥 1. РЕНДЕР СЛАЙДЕРА
        container.innerHTML = createSliderHTML(slides);
        
        // 🔥 2. HOVER ДЛЯ NEWS SLIDER - ПОСЛЕ РЕНДЕРА!
        if (container.hasAttribute('data-newsitem-slider')) {
            console.log('🎯 Adding hover to news slider');
            const images = container.querySelectorAll('img') as NodeListOf<HTMLImageElement>;
            images.forEach(img => {
                img.style.transition = 'transform 280ms ease-in-out';
                img.style.transform = 'scale(1)';
                img.addEventListener('mouseenter', () => img.style.transform = 'scale(1.1)');
                img.addEventListener('mouseleave', () => img.style.transform = 'scale(1)');
            });
        }
        
        // 🔥 3. ЛОГИКА СЛАЙДЕРА
        initSliderLogic(container, slides);
        
    } catch (error) {
        console.error("Slider error:", error);
        container.innerHTML = `<div class="${styles.slideError}">Ошибка загрузки</div>`;
    }
}

// Остальной код БЕЗ ИЗМЕНЕНИЙ (createSliderHTML + initSliderLogic из предыдущего сообщения)
function createSliderHTML(slides: SliderDto[]): string {
    return `
        <div class="${styles.slider}">
            <div class="${styles.sliderTrack}">
                ${slides.map((slide, i) => `
                    <div class="${styles.slide}">
                        <img src="${slide.imageUrl}" alt="${slide.title || 'Слайд'}" loading="lazy"/>
                    </div>
                `).join('')}
            </div>
            <button class="${styles.sliderBtn} ${styles.sliderPrev}">‹</button>
            <button class="${styles.sliderBtn} ${styles.sliderNext}">›</button>
            <div class="${styles.sliderDots}"></div>
        </div>
    `;
}

function initSliderLogic(container: HTMLElement, slides: SliderDto[]) {
    let index = 0;
    let intervalId = 0;
    
    const track = container.querySelector(`.${styles.sliderTrack}`) as HTMLElement;
    
    function updateSlider() {
        track.style.transform = `translateX(-${index * 100}%)`;
    }
    
    const nextBtn = container.querySelector(`.${styles.sliderNext}`) as HTMLButtonElement | null;
    const prevBtn = container.querySelector(`.${styles.sliderPrev}`) as HTMLButtonElement | null;
    
    const handleNext = () => { 
        index = (index + 1) % slides.length; 
        updateSlider(); 
        resumeAutoPlay(); 
    };
    const handlePrev = () => { 
        index = (index - 1 + slides.length) % slides.length; 
        updateSlider(); 
        resumeAutoPlay(); 
    };
    
    if (nextBtn) nextBtn.addEventListener('click', handleNext);
    if (prevBtn) prevBtn.addEventListener('click', handlePrev);
    
    const slider = container.querySelector(`.${styles.slider}`) as HTMLElement;
    slider.addEventListener('mouseenter', pauseAutoPlay);
    slider.addEventListener('mouseleave', resumeAutoPlay);
    
    function startAutoPlay() {
        if (slides.length <= 1) return;
        intervalId = window.setInterval(handleNext, 5000);
    }
    
    function pauseAutoPlay() {
        if (intervalId) {
            clearInterval(intervalId);
            intervalId = 0;
        }
    }
    
    function resumeAutoPlay() {
        if (!intervalId && slides.length > 1) startAutoPlay();
    }
    
    updateSlider();
    startAutoPlay();
}
