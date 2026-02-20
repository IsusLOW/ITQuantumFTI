import styles from './footer.module.css';

/**
 * Генерирует HTML-строку для компонента футера (без active логики).
 * @param {_props} - Пока не используем, но оставляем для совместимости.
 * @returns {string} HTML-строка для футера.
 */
export function Footer(_props: any): string {
    return `
        <footer class="${styles.footer}">
            <div class="${styles.container}">
                <div class="${styles.footerTop}">
                    <div class="${styles.container}">
                        <div class="widget_ajax_wrap" id="widget_pos_footer">
                            <ul class="${styles.menu}">
                                <li>
                                    <a class="item" href="/">
                                        <span class="wrap">Главная</span>
                                    </a>
                                </li>
                                <li>
                                    <a class="item" href="/news">
                                        <span class="wrap">Новости</span>
                                    </a>
                                </li>
                                <li>
                                    <a class="item" href="/courses">
                                        <span class="wrap">Курсы</span>
                                    </a>
                                </li>
                                <li>
                                    <a class="item" href="/schedulers">
                                        <span class="wrap">Расписание</span>
                                    </a>
                                </li>
                                <li>
                                    <a class="item" href="/aboutus">
                                        <span class="wrap">О нас</span>
                                    </a>
                                </li>
                            </ul>
                            <div class="${styles.widget_html_block}">
                                <div class="${styles.footerInfo}">
                                    <div class="${styles.footerInfoBlock}">
                                        <div class="${styles.footerInfoBox}">
                                            <a href="tel:+37353379483" class="${styles.valignWrapper} ${styles.topLink}">
                                                <i class="material-icons ${styles.valign}">phone</i>
                                                <span class="${styles.valign}">(+373) 533-79493</span>
                                            </a>
                                        </div>
                                        <div class="${styles.footerInfoBox}">
                                            <a href="mailto:it.quantum.fti@yandex.com" class="${styles.valignWrapper} ${styles.topLink}">
                                                <i class="material-icons ${styles.valign}">mail</i>
                                                <span class="${styles.valign}">it.quantum.fti@yandex.com</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="${styles.footerMiddle}"></div>
            </div>
        </footer>
    `;
}
