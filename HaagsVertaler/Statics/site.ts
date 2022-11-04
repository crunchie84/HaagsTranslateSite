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
