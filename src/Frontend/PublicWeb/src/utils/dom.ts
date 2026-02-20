/**
 * Создать HTML элемент из строки
 */
export function createElement(html: string): HTMLElement {
  const template = document.createElement('template');
  template.innerHTML = html.trim();
  const child = template.content.firstChild as HTMLElement;
  
  if (!child) {
    throw new Error('Failed to create element from HTML');
  }
  
  return child;
}

/**
 * Найти элемент по селектору
 */
export function findElement<T extends HTMLElement>(
  parent: ParentNode,
  selector: string
): T | null {
  return parent.querySelector<T>(selector);
}

/**
 * Найти все элементы по селектору
 */
export function findElements<T extends HTMLElement>(
  parent: ParentNode,
  selector: string
): T[] {
  return Array.from(parent.querySelectorAll<T>(selector));
}

/**
 * Добавить обработчик события с автоматической очисткой
 */
export function addDisposableListener<K extends keyof HTMLElementEventMap>(
  element: HTMLElement,
  type: K,
  listener: (this: HTMLElement, ev: HTMLElementEventMap[K]) => void,
  options?: boolean | AddEventListenerOptions
): () => void {
  element.addEventListener(type, listener, options);
  
  return () => {
    element.removeEventListener(type, listener, options);
  };
}

/**
 * Debounce функция
 */
export function debounce<T extends (...args: unknown[]) => void>(
  func: T,
  wait: number
): (...args: Parameters<T>) => void {
  let timeout: ReturnType<typeof setTimeout> | null = null;

  return function executedFunction(...args: Parameters<T>) {
    const later = () => {
      timeout = null;
      func(...args);
    };

    if (timeout) {
      clearTimeout(timeout);
    }
    timeout = setTimeout(later, wait);
  };
}

/**
 * Throttle функция
 */
export function throttle<T extends (...args: unknown[]) => void>(
  func: T,
  limit: number
): (...args: Parameters<T>) => void {
  let inThrottle: boolean;

  return function executedFunction(...args: Parameters<T>) {
    if (!inThrottle) {
      func(...args);
      inThrottle = true;
      setTimeout(() => (inThrottle = false), limit);
    }
  };
}
