import { useEffect } from 'react';
import styles from './Map.module.css';

export function Map() {
  useEffect(() => {
    const container = document.getElementById('map');
    if (!container) return;

    const oldScripts = container.querySelectorAll('script[src*="yandex"]');
    oldScripts.forEach((script) => script.remove());

    const script = document.createElement('script');
    script.type = 'text/javascript';
    script.async = true;
    script.charset = 'utf-8';
    script.src =
      'https://api-maps.yandex.ru/services/constructor/1.0/js/?um=constructor%3Adb12954bed5411dea1ff574f4ffdcf19c9464f3b3fd7f9451db3eea31a0a6f44&width=100%25&height=500&lang=ru_RU&scroll=true';
    container.appendChild(script);

    return () => {
      const scripts = container.querySelectorAll('script[src*="yandex"]');
      scripts.forEach((script) => script.remove());
    };
  }, []);

  return (
    <div className={styles.mapContainer}>
      <div id="map" className={styles.map}></div>
    </div>
  );
}
