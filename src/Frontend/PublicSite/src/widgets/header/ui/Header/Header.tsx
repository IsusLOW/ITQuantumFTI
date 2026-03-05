import { NavLink } from 'react-router-dom';
import styles from './Header.module.css';

const logoStyle: React.CSSProperties = {
  display: 'inline-block',
  verticalAlign: 'middle',
  marginBottom: '10px',
  paddingBottom: '10px',
  width: '350px',
  height: 'auto',
  maxWidth: 'none',
  flexShrink: 0,
};

export function Header() {
  return (
    <div className={styles.container}>
      <div className={styles.infobox}>
        <div className={styles.item}>
          <a href="tel:+37353379483" className={`${styles.valignWrapper} ${styles.topLink}`}>
            <i className={`material-icons ${styles.valign}`}>phone</i>
            <span className={styles.valign}>(+373) 533-79493</span>
          </a>
        </div>
        <div className={styles.item}>
          <a href="mailto:it.quantum.fti@yandex.com" className={`${styles.valignWrapper} ${styles.topLink}`}>
            <i className={`material-icons ${styles.valign}`}>mail</i>
            <span className={styles.valign}>it.quantum.fti@yandex.com</span>
          </a>
        </div>
      </div>

      <header className={styles.header}>
        <div className={styles.logoContainer}>
          <NavLink to="/" className={`${styles.logoLink}`}>
            <img src="/images/quant_logo.webp" alt="Quantum FTI logo" className={styles.logoImg} style={logoStyle} />
          </NavLink>
        </div>
        <nav className={styles.nav}>
          <NavLink to="/news" className={styles.navLink} end>Новости</NavLink>
          <NavLink to="/courses" className={styles.navLink} end>Курсы</NavLink>
          <NavLink to="/schedulers" className={styles.navLink} end>Расписание</NavLink>
          <NavLink to="/aboutus" className={styles.navLink} end>О нас</NavLink>
        </nav>
      </header>
    </div>
  );
}
