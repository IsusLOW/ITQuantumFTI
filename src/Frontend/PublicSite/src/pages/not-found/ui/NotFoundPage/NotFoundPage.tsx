import { Link } from 'react-router-dom';
import styles from './NotFoundPage.module.css';

export function NotFoundPage() {
  return (
    <div className={styles.container}>
      <div className={styles.content}>
        <h1 className={styles.code}>404</h1>
        <h2 className={styles.title}>Страница не найдена</h2>
        <p className={styles.description}>
          К сожалению, страница, которую вы ищете, не существует или была перемещена.
        </p>
        <Link to="/" className={styles.homeLink}>
          Вернуться на главную
        </Link>
      </div>
    </div>
  );
}
