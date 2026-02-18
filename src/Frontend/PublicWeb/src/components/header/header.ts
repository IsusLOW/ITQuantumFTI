import styles from './header.module.css';

interface HeaderProps {
  activePath: string;
}

export function Header({ activePath }: HeaderProps): string {
  const isActive = (path: string): string => 
    activePath === path ? styles.navLinkSelected : '';
  console.log(activePath);

  return `
    <div class="${styles.container}">
      <div class="${styles.infobox}">
        <div class="${styles.item}">
          <a href="tel:+37353379483" class="${styles.valignWrapper} ${styles.topLink}">
            <i class="material-icons ${styles.valign}">phone</i>
            <span class="${styles.valign}">(+373) 533-79493</span>
          </a>
        </div>
        <div class="${styles.item}">
          <a href="mailto:it.quantum.fti@yandex.com" class="${styles.valignWrapper} ${styles.topLink}">
            <i class="material-icons ${styles.valign}">mail</i>
            <span class="${styles.valign}">it.quantum.fti@yandex.com</span>
          </a>
        </div>
      </div>
      
      <header class="${styles.header}">
        <div class="${styles.logoContainer}">
          <a href="/" class="${styles.logoLink} ${isActive('/')}">
            <img src="/images/quant_logo.webp" alt="Quantum FTI logo" class="${styles.logoImg}" />
          </a>
        </div>
        <nav class="${styles.nav}">
          <a href="/news" class="${styles.navLink} ${isActive('/news')}">Новости</a>
          <a href="/courses" class="${styles.navLink} ${isActive('/courses')}">Курсы</a>
          <a href="/schedulers" class="${styles.navLink} ${isActive('/schedulers')}">Расписание</a>
          <a href="/aboutus" class="${styles.navLink} ${isActive('/aboutus')}">О нас</a>
        </nav>
      </header>
    </div>
  `;
}
