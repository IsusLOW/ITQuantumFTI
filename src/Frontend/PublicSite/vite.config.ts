import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react';
import { fileURLToPath } from 'url';
import { dirname, resolve } from 'path';

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
  // Загружаем переменные окружения
  const env = loadEnv(mode, process.cwd(), '');
  const apiUrl = env.VITE_API_URL || '/api';

  return {
    plugins: [react()],
    resolve: {
      alias: {
        '@': resolve(__dirname, 'src'),
        '@app': resolve(__dirname, 'src/app'),
        '@pages': resolve(__dirname, 'src/pages'),
        '@widgets': resolve(__dirname, 'src/widgets'),
        '@features': resolve(__dirname, 'src/features'),
        '@entities': resolve(__dirname, 'src/entities'),
        '@shared': resolve(__dirname, 'src/shared'),
      },
    },
    server: {
      port: 5174,
      open: true,
      proxy: {
        '/api': {
          target: apiUrl.replace('/api', 'http://localhost:5000'),
          changeOrigin: true,
          secure: false,
        }
      }
    },
    // SPA роутинг - перенаправляем все запросы на index.html
    appType: 'spa',
    preview: {
      port: 4173,
      open: true,
    },
    build: {
      outDir: 'dist',
      sourcemap: true,
    },
    // Определяем глобальные константы
    define: {
      __APP_VERSION__: JSON.stringify(env.VITE_APP_VERSION || '1.0.0'),
      __APP_TITLE__: JSON.stringify(env.VITE_APP_TITLE || 'IT-Квантум'),
    },
  };
});
