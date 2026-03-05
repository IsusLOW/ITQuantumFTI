import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { preloadSlider } from '@/entities/slider/lib/slider-preload.js';
import '@/app/index.css';
import App from '@/app/App.js';

// Предзагрузка слайдера для улучшения LCP
preloadSlider();

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </StrictMode>
);
