import styles from './Footer.module.css';

export function Footer() {
  return (
    <footer className={styles.footer}>
      <div className={styles.container}>
        <div className={styles.footerTop}>
          <div className={styles.container}>
            <div className="widget_ajax_wrap" id="widget_pos_footer">
              <ul className={styles.menu}>
                <li>
                  <a className="item" href="/">
                    <span className="wrap">Главная</span>
                  </a>
                </li>
                <li>
                  <a className="item" href="/news">
                    <span className="wrap">Новости</span>
                  </a>
                </li>
                <li>
                  <a className="item" href="/courses">
                    <span className="wrap">Курсы</span>
                  </a>
                </li>
                <li>
                  <a className="item" href="/schedulers">
                    <span className="wrap">Расписание</span>
                  </a>
                </li>
                <li>
                  <a className="item" href="/aboutus">
                    <span className="wrap">О нас</span>
                  </a>
                </li>
              </ul>
              <div className={styles.widget_html_block}>
                <div className={styles.footerInfo}>
                  <div className={styles.footerInfoBlock}>
                    <div className={styles.footerInfoBox}>
                      <a href="tel:+37353379483" className={`${styles.valignWrapper} ${styles.topLink}`}>
                        <i className={`material-icons ${styles.valign}`}>phone</i>
                        <span className={styles.valign}>(+373) 533-79493</span>
                      </a>
                    </div>
                    <div className={styles.footerInfoBox}>
                      <a href="mailto:it.quantum.fti@yandex.com" className={`${styles.valignWrapper} ${styles.topLink}`}>
                        <i className={`material-icons ${styles.valign}`}>mail</i>
                        <span className={styles.valign}>it.quantum.fti@yandex.com</span>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div className={styles.footerMiddle}></div>
      </div>
    </footer>
  );
}
