import { defineConfig } from 'vite';

export default defineConfig({
  server: {
    port: 5173,
    open: true,
    proxy: {
      '/api': {
        target: 'http://localhost:5000',  // ← локально
        changeOrigin: true,
        secure: false,
      }
    }
  },
  // 🔥 ДЛЯ ПРОДАКШЕНА - build proxy
  build: {
    // ...
  }
});
