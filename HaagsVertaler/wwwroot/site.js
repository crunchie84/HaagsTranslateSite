!(function () {
  var e;
  null === (e = document.querySelector('.js-wis-tekst')) ||
    void 0 === e ||
    e.addEventListener('click', () => {
      const e = document.querySelector('.js-nederlands');
      e && ((e.value = ''), e.focus());
    });
  const t = document.querySelector('.js-kopieer-tekst');
  t &&
    t.addEventListener('click', () => {
      const e = document.querySelector('.js-haags');
      null == e || e.select(), document.execCommand('copy');
    });
})();
//# sourceMappingURL=site.js.map
