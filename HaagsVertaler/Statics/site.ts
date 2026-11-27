document.querySelector('.js-wis-tekst')?.addEventListener('click', () => {
  const textarea: HTMLTextAreaElement | null = document.querySelector('.js-nederlands');

  if (textarea) {
    textarea.value = '';
    textarea.focus();
  }
});

const kopieerTekst = document.querySelector('.js-kopieer-tekst');
if (kopieerTekst) {
  kopieerTekst.addEventListener('click', () => {
    const textarea: HTMLTextAreaElement | null = document.querySelector('.js-haags');
    textarea?.select();
    document.execCommand('copy');
  });
}

const vertaling = document.querySelector<HTMLElement>('.haags-output[data-net-vertaald]');
const opMobiel = window.matchMedia('(max-width: 47.49rem)').matches;

if (vertaling && opMobiel) {
  const invoer = document.querySelector<HTMLTextAreaElement>('.js-nederlands');
  invoer?.removeAttribute('autofocus');
  if (document.activeElement === invoer) invoer.blur();

  const rustig = window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  requestAnimationFrame(() => {
    vertaling.scrollIntoView({ behavior: rustig ? 'auto' : 'smooth', block: 'start' });
  });
}

document.addEventListener('DOMContentLoaded', function () {
  const link = document.getElementById('haagsnl-link');

  link?.addEventListener('click', function () {
    // @ts-ignore
    window.gtag('event', 'click', {
      event_category: 'ads',
      event_label: 'haagsnl-link',
    });
  });

  const extensionLink = document.getElementById('chrome-extension-link');

  extensionLink?.addEventListener('click', function () {
    // @ts-ignore
    window.gtag('event', 'click', {
      event_category: 'ads',
      event_label: 'chrome-extension',
    });
  });
});
