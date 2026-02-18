import mapStyles from './map.module.css';

export function Map(): string {
    const containerId = `map-${Math.random().toString(36).slice(2)}`;
    return `
        <div id="${containerId}" class="${mapStyles.mapContainer}">
            <div id="map" class="${mapStyles.map}"></div>
        </div>
    `;
}

export async function initMap(containerId: string): Promise<void> {
    const container = document.getElementById(containerId) as HTMLElement;
    if (!container) {
        console.error('❌ Map container не найден:', containerId);
        return;
    }

    // 🔥 ТОЧНО ваш Blazor JS
    await initializeMap(containerId);
}

function initializeMap(containerId: string) {
    const mapContainer = document.getElementById('map');
    if (!mapContainer) return;

    // Удаляем старые скрипты если есть
    const oldScripts = mapContainer.querySelectorAll('script[src*="yandex"]');
    oldScripts.forEach(script => script.remove());

    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.async = true;
    script.charset = 'utf-8';
    script.src = 'https://api-maps.yandex.ru/services/constructor/1.0/js/?um=constructor%3Adb12954bed5411dea1ff574f4ffdcf19c9464f3b3fd7f9451db3eea31a0a6f44&width=100%25&height=500&lang=ru_RU&scroll=true';
    mapContainer.appendChild(script);
}