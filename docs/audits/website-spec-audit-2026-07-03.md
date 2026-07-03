# Website Specification audit — 2026-07-03

Audit of **haags.nu** against [The Website Specification](https://specification.website): all **156 items** (36 `required`, 80 `recommended`, 35 `optional`, 5 `avoid`) across the spec's 10 categories.

- **Method:** one audit agent per category, working from the spec's MCP checklist; evidence gathered with curl (HTML, headers, assets, conditional requests, encoding negotiation), dig, and openssl against the live deployment. Items that need a real browser are marked 🔍.
- **Environment:** production — no staging caveats apply.
- **Base URL:** `https://haags.nu`

## Audited URLs

| Path | Page |
| --- | --- |
| `/` | Home — the translator |
| `/over` | About |

Additional paths probed where items called for them: `robots.txt`, `sitemap.xml`, `/llms.txt`, `/.well-known/*`, `/api`, a 404 page, and host/scheme redirect variants.

Legend: ✅ pass · ⚠️ partial/caveat · ❌ fail · 🔍 manual check needed · — not applicable

## Changes since previous report

First audit — no previous report to compare against.

## 1. Foundations (20 items)

_HTML, head, document basics._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [The HTML doctype](https://specification.website/spec/foundations/doctype/) | required | ✅ | `<!DOCTYPE html>` is line 1 on `/` and `/over` |
| [The lang attribute on `<html>`](https://specification.website/spec/foundations/html-lang/) | required | ✅ | `<html lang="nl">` on both pages; valid BCP 47, matches content |
| [`<meta charset>`](https://specification.website/spec/foundations/meta-charset/) | required | ✅ | `<meta charset="UTF-8">` at byte offset 96; header also sends `charset=utf-8` |
| [`<meta viewport>`](https://specification.website/spec/foundations/meta-viewport/) | required | ✅ | `width=device-width, initial-scale=1`; user scaling not disabled |
| [The `<title>` element](https://specification.website/spec/foundations/title/) | required | ⚠️ | Exactly one non-empty title per page, but `/over` reuses the home title verbatim (spec: unique per page) |
| [`<meta name="description">`](https://specification.website/spec/foundations/meta-description/) | recommended | ⚠️ | Present but identical on `/` and `/over`; not unique per page |
| [Canonical URL (rel="canonical")](https://specification.website/spec/foundations/canonical-url/) | recommended | ❌ | No `rel="canonical"` on either page; duplicate URLs serve 200 (`/over`, `/over/`, `/OVER`) |
| [Favicons and app icons](https://specification.website/spec/foundations/favicons/) | recommended | ⚠️ | `/favicon.ico` 200, PNG 16/32, apple-touch-icon OK; no SVG favicon, manifest has only one 192×192 icon — no 512 and no maskable variant |
| [`<meta name="theme-color">`](https://specification.website/spec/foundations/theme-color/) | recommended | ✅ | Present (`#ffffff`); single value is fine for a light-only site, though it mismatches manifest `theme_color` `#c7c49f` and brand bg `#fed67a` |
| [`<meta name="color-scheme">`](https://specification.website/spec/foundations/color-scheme/) | recommended | ❌ | No `<meta name="color-scheme">` and no `color-scheme` in shipped CSS |
| [Open Graph protocol](https://specification.website/spec/foundations/open-graph/) | recommended | ⚠️ | og:title/description/image/url/site_name present; missing `og:type`, `og:image:width/height`, `twitter:card`; `og:url` on `/over` points at the homepage |
| [Feed discovery with rel="alternate"](https://specification.website/spec/foundations/feed-discovery/) | recommended | — | Site publishes no feed; nothing to announce |
| [Feed content hygiene](https://specification.website/spec/foundations/feed-hygiene/) | recommended | — | No feed published |
| [Popover API](https://specification.website/spec/foundations/popover-api/) | recommended | — | No modals, menus, or tooltips anywhere on the site |
| [CSS anchor positioning](https://specification.website/spec/foundations/anchor-positioning/) | recommended | — | No tethered UI (tooltips/menus/popovers) to position |
| [Balanced text wrapping](https://specification.website/spec/foundations/text-wrap/) | recommended | ❌ | No `text-wrap: balance`/`pretty` in the inlined CSS; multi-line h1/h2 headings would benefit |
| [CSS container queries](https://specification.website/spec/foundations/container-queries/) | recommended | — | No `@container`; single page-level layout with viewport media queries only, no components rendered in varying containers |
| [Invoker commands](https://specification.website/spec/foundations/invoker-commands/) | recommended | — | No popovers/dialogs to wire; "Wis tekst"/"Kopieer" buttons use JS listeners (custom `command` would be optional polish) |
| [Entry and exit animations](https://specification.website/spec/foundations/entry-exit-animations/) | optional | — | No elements enter/exit the DOM or top layer; not adopted, not needed |
| [Content-based field sizing](https://specification.website/spec/foundations/field-sizing/) | optional | — | Not adopted; textareas use fixed `min-height:12.5rem` + `resize:vertical`, no JS auto-grow hack present either |

### Findings

- **Canonical URL ❌** — no `<link rel="canonical">` in either page's `<head>`. Duplicate-content URLs all return 200 with identical HTML: `https://haags.nu/over`, `https://haags.nu/over/`, `https://haags.nu/OVER`. Host variants: `http://haags.nu/` 301→`https://haags.nu/` (good), but `https://www.haags.nu/` serves an **expired certificate** (`notAfter=Mar 18 2024`, CN=haags.nu incl. www SAN) and, past the cert error, 301-redirects to plain `http://haags.nu/` (HTTPS→HTTP downgrade).
- **color-scheme ❌** — no `name="color-scheme"` meta and no `color-scheme` property in the inline CSS of either page.
- **text-wrap ❌** — shipped CSS contains no `text-wrap` declarations; headings like "Hoe is deze Haagse Vertaalmachine tot stand gekomen?" wrap unbalanced.
- **title ⚠️** — `/` and `/over` both ship `<title>Vertaal Nederlands naar het Haags</title>`; the About page title should be page-specific (also a duplicate-content quality signal).
- **meta description ⚠️** — identical `content="Voor als je Nederlands naar het Haags wil vertalen…"` on both pages.
- **Open Graph ⚠️** — missing `og:type` on both pages; `og:url` is `https://haags.nu` even on `/over`; no `og:image:width`/`og:image:height`; only `twitter:image:src` is set — no `twitter:card=summary_large_image`. `og:image` (`/share.png`, 8.8 KB PNG, 200) is otherwise fine.
- **Favicons ⚠️** — no SVG favicon (`/favicon.svg` 404 and none referenced); `manifest.json` declares a single 192×192 icon — no 512×512 and no `"purpose": "maskable"` entry. Minor: `<link rel="manifest" href="manifest.json">` is a relative URL — resolves correctly for these root-level routes but would break under nested paths.

## 2. SEO (14 items)

_Search visibility._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Redirects (301/302/308)](https://specification.website/spec/seo/redirects/) | required | ⚠️ | Apex http→https is a clean 301, but www chains 3 hops (http://www → https://www → **http://**haags.nu → https://haags.nu) incl. an HTTPS→HTTP downgrade, and the www cert is expired |
| [Meta robots and X-Robots-Tag](https://specification.website/spec/seo/meta-robots/) | required | ✅ | No meta robots / X-Robots-Tag on either public page → implicit index,follow; nothing accidentally noindexed |
| [Heading hierarchy](https://specification.website/spec/seo/heading-hierarchy/) | required | ✅ | `/`: h1 → h2,h2; `/over`: h1 → h2,h2; one h1 per page, no skipped levels |
| [robots.txt](https://specification.website/spec/seo/robots-txt/) | recommended | ❌ | `GET /robots.txt` → 404, content-length 0 |
| [XML sitemaps](https://specification.website/spec/seo/xml-sitemaps/) | recommended | ❌ | `/sitemap.xml` and `/sitemap_index.xml` both 404; no sitemap referenced anywhere |
| [Sitemap index files](https://specification.website/spec/seo/sitemap-index/) | recommended | — | Site has ~2 URLs; an index file is not applicable at this scale |
| [URL structure](https://specification.website/spec/seo/url-structure/) | recommended | ⚠️ | URLs are lowercase, short, descriptive (`/`, `/over`), but `/over/`, `/OVER`, and `?utm_source=x` all return 200 with no `rel=canonical` on any page → uncontrolled duplicates |
| [Server-side rendering](https://specification.website/spec/seo/server-side-rendering/) | recommended | ✅ | Full content, title, and meta in initial HTML (ASP.NET Core SSR); translation works via form POST, JS only progressive enhancement |
| [Internal linking](https://specification.website/spec/seo/internal-linking/) | recommended | ✅ | Header nav cross-links `/` ↔ `/over` with descriptive anchors ("Vertaler", "Over"); both pages reachable via plain `<a href>` |
| [Structured data (JSON-LD)](https://specification.website/spec/seo/structured-data/) | recommended | ❌ | No `application/ld+json` on either page; WebSite/WebApplication schema would fit the translator |
| [Breadcrumbs](https://specification.website/spec/seo/breadcrumbs/) | recommended | — | Flat two-page site with no hierarchy; breadcrumbs not applicable |
| [Image and video sitemap extensions](https://specification.website/spec/seo/image-sitemaps/) | optional | — | No sitemap exists and no crawl-hidden media; not applicable |
| [IndexNow](https://specification.website/spec/seo/indexnow/) | optional | — | No evidence of adoption (no discoverable key file); optional and pings are not statically verifiable |
| [Soft 404s](https://specification.website/spec/seo/soft-404/) | avoid | ✅ | `/nonexistent-page-xyz`, `/robots.txt`, `/index.html` all return a genuine HTTP 404 status — anti-pattern absent (body is empty, a UX rather than SEO issue) |

### Findings

- **robots.txt missing (❌)**: `https://haags.nu/robots.txt` → `HTTP/2 404`, `content-length: 0`. Even a permissive `User-agent: * / Allow: /` file with a Sitemap line is recommended.
- **No XML sitemap (❌)**: `https://haags.nu/sitemap.xml` → `HTTP/2 404`; `/sitemap_index.xml` also 404. A two-URL sitemap would be trivial to add.
- **No structured data (❌)**: zero `<script type="application/ld+json">` blocks on `/` and `/over`. Only OG meta tags present.
- **Redirect chain + downgrade on www (⚠️)**: `http://www.haags.nu/` 301 → `https://www.haags.nu/` 301 → `http://haags.nu/` 301 → `https://haags.nu/` (3 hops). The middle hop downgrades HTTPS→HTTP, and the cert served on `www.haags.nu:443` is expired. `https://www.haags.nu` should 301 directly to `https://haags.nu/` in one hop.
- **Duplicate URL variants without canonical (⚠️)**: `/over/` (trailing slash), `/OVER` (uppercase, IIS case-insensitive) and `/over?utm_source=x` all return 200 with identical content; no `rel=canonical` link on any page. `og:url` is hardcoded to `https://haags.nu` even on `/over`. Adding per-page canonicals (or 301ing variants) would consolidate signals.

## 3. Accessibility (25 items)

_WCAG-aligned rules._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Colour contrast](https://specification.website/spec/accessibility/color-contrast/) | required | ⚠️ | All text passes (body 11.9:1, buttons 5.05–6.38:1, links 7.7:1, large green h2 3.63:1) but textarea boundary is ~1.2:1 vs page bg (<3:1 non-text) |
| [Image alt text](https://specification.website/spec/accessibility/image-alt-text/) | required | ✅ | Both imgs have descriptive alt; decorative Chrome SVG is aria-hidden |
| [Form labels](https://specification.website/spec/accessibility/form-labels/) | required | ✅ | Both textareas carry aria-label ("Nederlandse tekst"/"Haagse tekst") plus visible h2 headings |
| [Keyboard navigation](https://specification.website/spec/accessibility/keyboard-navigation/) | required | 🔍 | Static evidence good: only native a/button/textarea, no tabindex, no focus-trap JS; needs live check |
| [Visible focus indicators](https://specification.website/spec/accessibility/focus-indicators/) | required | ❌ | Global `textarea:focus{outline:0}`; readonly Haags textarea gets no replacement; Dutch input's replacement border #c1a361 on #fff is 2.42:1 (<3:1) |
| [Skip links](https://specification.website/spec/accessibility/skip-links/) | required | ❌ | No skip link on either page; first focusable is the nav "Vertaler" link (autofocus on the input partially mitigates on `/` only) |
| [Semantic HTML and landmarks](https://specification.website/spec/accessibility/semantic-html/) | required | ✅ | main/nav/footer/h1/h2/sections used; minor: `<header>` nested inside `<main>` so no banner landmark |
| [Descriptive link text](https://specification.website/spec/accessibility/link-text/) | required | ✅ | No "click here"; all links name their destination |
| [Accessible form errors](https://specification.website/spec/accessibility/form-errors/) | required | ❌ | Empty submit re-renders an identical page with no error text; data-val-* attrs present but no validation script loaded; no aria-invalid/aria-describedby |
| [Document and parts language](https://specification.website/spec/accessibility/document-language/) | required | ✅ | `<html lang="nl">` on both pages; Haags is a Dutch dialect, no separate lang tag exists |
| [Reduced motion](https://specification.website/spec/accessibility/reduced-motion/) | required | ⚠️ | No prefers-reduced-motion query; only motion is 0.2s hover transitions incl. `scale(1.05)` — low risk but unguarded |
| [Captions and transcripts](https://specification.website/spec/accessibility/captions-and-transcripts/) | required | — | No audio or video content |
| [Accessible data tables](https://specification.website/spec/accessibility/data-tables/) | required | — | No tabular data on either page |
| [Touch target size](https://specification.website/spec/accessibility/touch-target-size/) | required | ⚠️ | "Wis tekst" button is a 12px-font, zero-padding control (~14–16px tall, <24px); likely saved only by the WCAG spacing exception; "Kopieer" ok (~39px via padding) |
| [Forced colours mode](https://specification.website/spec/accessibility/forced-colors/) | recommended | ⚠️ | No `forced-colors` CSS; native elements mostly repair themselves, but `outline:0` on textarea focus also kills focus visibility in forced-colors mode |
| [The inert attribute](https://specification.website/spec/accessibility/inert-attribute/) | recommended | — | No overlays, dialogs, or off-canvas UI |
| [ARIA — first rule of ARIA](https://specification.website/spec/accessibility/aria-usage/) | recommended | ✅ | Minimal, correct ARIA: aria-label on textareas, aria-hidden on decorative SVG; native elements everywhere else |
| [Accessible authentication](https://specification.website/spec/accessibility/accessible-authentication/) | recommended | — | No authentication on the site |
| [Redundant entry](https://specification.website/spec/accessibility/redundant-entry/) | recommended | — | Single-step tool; no multi-step processes |
| [Hidden until found](https://specification.website/spec/accessibility/hidden-until-found/) | recommended | — | No collapsible/accordion content |
| [Mobile-friendly form inputs](https://specification.website/spec/accessibility/mobile-form-inputs/) | recommended | ✅ | Textarea text 18px (≥16px, no iOS zoom); free-text field so default keyboard is correct; proper viewport meta |
| [Native interactive elements](https://specification.website/spec/accessibility/native-interactive-elements/) | recommended | ✅ | Real `<button>`, `<a>`, `<form>`, `<textarea>` throughout; no div-with-click-handler patterns |
| [CSS state and relational selectors](https://specification.website/spec/accessibility/css-state-selectors/) | recommended | ⚠️ | No `:has()`/`:user-invalid`/`:focus-within` in shipped CSS despite min/maxlength constraints on the input; low applicability overall |
| [Empty links and buttons](https://specification.website/spec/accessibility/empty-links-buttons/) | avoid | ✅ | Every link and button has a text accessible name; anti-pattern absent |
| [Accessibility overlays](https://specification.website/spec/accessibility/accessibility-overlays/) | avoid | ✅ | No overlay widget; only third-party script is Google gtag |

### Findings

- **Visible focus indicators ❌**: shipped CSS contains `textarea:focus{outline:0}` globally. The readonly result textarea (`.js-haags`) is keyboard-focusable but receives no replacement style at all on focus. The Dutch input's replacement (`.nederlands-input textarea:focus{background:#fff;border-color:#c1a361;...}`) yields a border of only 2.42:1 against the white focus background, below the 3:1 indicator contrast guideline.
- **Skip links ❌**: neither `/` nor `/over` contains a skip-to-main link; the first focusable element is the header nav link `<a class="active" href="/">Vertaler</a>`. The `autofocus` on `#Source` mitigates only the home page and only on load.
- **Accessible form errors ❌**: `POST /` with `Source=` (empty, violating the declared `data-val-minlength-min="1"`) returned HTTP 200 with a byte-identical page to GET `/` (diff after normalizing the antiforgery token: zero lines). No error text, no `aria-invalid`, no `aria-describedby`, no validation summary; the ASP.NET `data-val-*` attributes are inert because no jQuery/unobtrusive-validation script is loaded.
- **Colour contrast ⚠️**: all computed text ratios pass (body #02241a/#fed67a = 11.90, submit #fff/#0d7e5d = 5.05, "Wis tekst" #4a4a4a/#fed67a = 6.38, "Kopieer" #4a4a4a/#e5c272 = 5.19, links #3e3e3e = 7.70, h2 #0d7e5d = 3.63 at large/bold size). Non-text fails statically: textarea fill #ffedc1 vs page bg #fed67a = 1.20:1 and its border #e5c272 vs bg = 1.23:1, so the input boundary is under the 3:1 non-text minimum. Rendered check still advised.
- **Touch target size ⚠️**: `.form-header button` ("Wis tekst") has `font-size:12px`, no padding → rendered box ≈14–16px tall, below the 24×24 CSS px minimum; the ~10px margin to the textarea below means the WCAG 2.5.8 spacing exception probably applies, but it is marginal. "Kopieer" passes via padding.
- **Reduced motion ⚠️**: 0 occurrences of `prefers-reduced-motion` in shipped CSS; motion is limited to 0.15–0.2s hover transitions including `.button:hover{transform:scale(1.05)}` — minor, but unguarded.
- **Forced colours ⚠️**: 0 occurrences of `forced-colors`; the global `outline:0` on textarea focus would also suppress the focus indicator under Windows High Contrast.
- **CSS state selectors ⚠️**: no `:has()`, `:user-invalid`, `:user-valid`, `:placeholder-shown`, or `:focus-within` anywhere in the inlined stylesheet, even though the form declares min/maxlength constraints that could be surfaced this way.

## 4. Security (17 items)

_Headers, transport, policies._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [HTTPS and TLS](https://specification.website/spec/security/https-tls/) | required | ⚠️ | Apex OK: TLS 1.2+1.3, TLS 1.0/1.1 rejected, http→https 301. But https://www.haags.nu serves a cert that expired 2024-03-18, and http://www 301s into it. |
| [HSTS (Strict-Transport-Security)](https://specification.website/spec/security/hsts/) | required | ⚠️ | `max-age=2592000` (30 days) on all HTTPS responses; missing `includeSubDomains`, max-age far below recommended 63072000. |
| [X-Content-Type-Options: nosniff](https://specification.website/spec/security/x-content-type-options/) | required | ✅ | `nosniff` present on pages, 404, manifest.json and image assets. |
| [Clickjacking protection (frame-ancestors / X-Frame-Options)](https://specification.website/spec/security/frame-ancestors/) | required | ⚠️ | No CSP `frame-ancestors`; legacy XFO only, and `/` sends two conflicting values (`SAMEORIGIN` + `DENY`). `/over`, 404 and assets send `DENY` once. |
| [Cookie attributes — Secure, HttpOnly, SameSite](https://specification.website/spec/security/cookie-attributes/) | required | ❌ | `.AspNetCore.Antiforgery.*` cookie set on `/` with `httponly; samesite=strict` but no `Secure` flag and no `__Host-` prefix. |
| [Mixed content and upgrade-insecure-requests](https://specification.website/spec/security/mixed-content/) | recommended | ⚠️ | No http:// subresources found on `/` or `/over`, but no CSP so no `upgrade-insecure-requests` safety net. |
| [Content Security Policy (CSP)](https://specification.website/spec/security/content-security-policy/) | recommended | ❌ | No CSP header and no `<meta http-equiv>` CSP on either page. |
| [Reporting API (Reporting-Endpoints)](https://specification.website/spec/security/reporting-endpoints/) | recommended | ❌ | No `Reporting-Endpoints` (or `Report-To`) header on any response. |
| [/.well-known/security.txt](https://specification.website/spec/security/security-txt/) | recommended | ❌ | `/.well-known/security.txt` and `/security.txt` both return 404. |
| [Cross-origin isolation (COOP / COEP / CORP)](https://specification.website/spec/security/cross-origin-isolation/) | recommended | ❌ | No Cross-Origin-Opener-Policy, -Embedder-Policy, or -Resource-Policy on any response. |
| [Referrer-Policy](https://specification.website/spec/security/referrer-policy/) | recommended | ❌ | No `Referrer-Policy` header or meta on either page. |
| [Permissions-Policy](https://specification.website/spec/security/permissions-policy/) | recommended | ❌ | No `Permissions-Policy` header on any response. |
| [Subresource Integrity (SRI)](https://specification.website/spec/security/subresource-integrity/) | recommended | ❌ | External gtag.js (googletagmanager.com) and Google Fonts CSS load without `integrity`; both are SRI-incompatible dynamic resources — self-hosting would be needed to comply. |
| [Trusted Types](https://specification.website/spec/security/trusted-types/) | recommended | ❌ | No CSP at all, so no `require-trusted-types-for` / `trusted-types` directives. |
| [DNS CAA records](https://specification.website/spec/security/caa-records/) | recommended | ❌ | `dig CAA haags.nu` returns no records. |
| [Clear-Site-Data](https://specification.website/spec/security/clear-site-data/) | optional | — | No login/logout or session flows on the site; nothing to clear. |
| [DNSSEC](https://specification.website/spec/security/dnssec/) | optional | ✅ | Zone signed (DS + DNSKEY + RRSIG, alg 13); validating resolver returns `ad` flag. |

### Findings

- **Cookie attributes (❌, required):** `set-cookie: .AspNetCore.Antiforgery.cdV5uW_Ejgc=…; path=/; samesite=strict; httponly` on `https://haags.nu/` — missing `Secure` and no `__Host-` prefix. Configure ASP.NET Core antiforgery with `Cookie.SecurePolicy = CookieSecurePolicy.Always`.
- **HTTPS/TLS (⚠️):** `https://www.haags.nu` serves a Let's Encrypt cert `notAfter=Mar 18 01:30:26 2024 GMT` (expired >2 years); `http://www.haags.nu` 301s to it, so the www variant is fully broken. Apex `haags.nu` uses a valid DigiCert cert (notAfter Dec 12 2026), TLS 1.2/1.3 only, and `http://haags.nu` 301s to `https://haags.nu/`.
- **HSTS (⚠️):** `strict-transport-security: max-age=2592000` — 30 days instead of the recommended `max-age=63072000; includeSubDomains` (though `includeSubDomains` must wait until the expired www cert is fixed).
- **Clickjacking (⚠️):** `/` sends both `x-frame-options: SAMEORIGIN` and `x-frame-options: DENY` (conflicting duplicates); no CSP `frame-ancestors` anywhere. Other responses send a single `DENY`.
- **CSP / Trusted Types / Reporting / Referrer-Policy / Permissions-Policy / COOP-COEP-CORP (❌):** none of these headers appear on `/`, `/over`, the 404, or assets; only HSTS, nosniff, XFO and the deprecated `x-xss-protection: 1` are sent.
- **security.txt (❌):** both `/.well-known/security.txt` and `/security.txt` return HTTP 404.
- **SRI (❌):** the gtag.js script and Google Fonts stylesheet have no `integrity` attribute on either page.
- **CAA (❌):** no CAA record on `haags.nu`; adding one for DigiCert (and Let's Encrypt if www is re-issued there) would block mis-issuance.
- **Mixed content (⚠️):** zero insecure subresources observed, but with no CSP there is no `upgrade-insecure-requests` fallback directive.

## 5. Well-Known URIs (10 items)

_Standard /.well-known/ paths._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Well-known URIs](https://specification.website/spec/well-known/well-known-overview/) | recommended | ⚠️ | No well-known URIs published at all; mechanically sound though — all `/.well-known/*` probes return real 404 (empty body, no soft-404), served over HTTPS, no auth wall |
| [/.well-known/api-catalog](https://specification.website/spec/well-known/api-catalog/) | recommended | ❌ | 404; no `Link: rel="api-catalog"` header on responses either — even a minimal Linkset pointing at site resources is absent |
| [/.well-known/change-password](https://specification.website/spec/well-known/change-password/) | optional | — | Site has no user accounts (no login/registration anywhere); nothing to point at |
| [/.well-known/webauthn](https://specification.website/spec/well-known/webauthn/) | optional | — | No passkeys/WebAuthn in use, single origin; not applicable |
| [/.well-known/openid-configuration](https://specification.website/spec/well-known/openid-configuration/) | optional | — | Site is not an OIDC identity provider; 404 is correct |
| [/.well-known/webfinger](https://specification.website/spec/well-known/webfinger/) | optional | — | No Fediverse/ActivityPub presence; `?resource=acct:...` probe returns 404, which is fine |
| [/.well-known/apple-app-site-association](https://specification.website/spec/well-known/apple-app-site-association/) | optional | — | No iOS app (no App Store links in HTML, only apple-touch-icon); Universal Links not needed |
| [/.well-known/assetlinks.json](https://specification.website/spec/well-known/assetlinks-json/) | optional | — | No Android app (no Play Store links); App Links not needed |
| [/.well-known/nodeinfo](https://specification.website/spec/well-known/nodeinfo/) | optional | — | Not a federated platform; not applicable |
| [/.well-known/traffic-advice](https://specification.website/spec/well-known/traffic-advice/) | optional | — | Opt-out/throttle mechanism not implemented; 404 means default prefetch behavior, which is acceptable for this site |

### Findings

- **api-catalog (❌)**: `/.well-known/api-catalog` → 404, and no `Link` header advertising `rel="api-catalog"` on `/` or `/over`. A small static `application/linkset+json` file listing the site's resources (notably the `/api` translate endpoint) would satisfy RFC 9727. Caveat: the resources it would typically point at (`/llms.txt`, `/sitemap.xml`, `/robots.txt`) don't exist either.
- **Well-known URIs overview (⚠️)**: the site publishes zero well-known URIs — all probed paths (including `security.txt`) return 404 with empty bodies. The handling itself is correct per RFC 8615 (no soft-404, HTTPS enforced), but nothing under `/.well-known/` is actually served.

## 6. Agent Readiness (20 items)

_Discoverability by AI agents._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Stable URLs](https://specification.website/spec/agent-readiness/stable-urls/) | required | ⚠️ | Core URLs stable, http→https 301 OK; but `https://www.haags.nu` serves an expired cert (with -k it 301s to insecure `http://haags.nu/`), and `/over` + `/over/` both return 200 with no canonical |
| [Agent readiness](https://specification.website/spec/agent-readiness/agent-readiness-overview/) | recommended | ⚠️ | Good base: fully server-rendered HTML (no JS needed), stable URLs, JSON API at `/api?text=`; but no robots.txt, no sitemap.xml, no structured data, no discovery files of any kind |
| [/llms.txt](https://specification.website/spec/agent-readiness/llms-txt/) | recommended | ❌ | `GET /llms.txt` → 404 |
| [Per-page Markdown source endpoints](https://specification.website/spec/agent-readiness/markdown-source-endpoints/) | recommended | — | Not a documentation site (2 pages, a tool); `/over.md` → 404 and `Accept: text/markdown` still returns text/html |
| [robots.txt for AI crawlers](https://specification.website/spec/agent-readiness/robots-for-ai-crawlers/) | recommended | ❌ | `GET /robots.txt` → 404 (empty body); no AI-crawler policy exists at all |
| [Structured data for agents](https://specification.website/spec/agent-readiness/structured-data-for-agents/) | recommended | ❌ | Zero `application/ld+json` blocks on `/` and `/over`; only OG meta tags. `WebApplication`/`WebSite` JSON-LD would fit the translator |
| [Machine-readable formats](https://specification.website/spec/agent-readiness/machine-readable-formats/) | recommended | ✅ | `/api?text=hallo%20allemaal` → 200 `application/json; charset=utf-8`, `{"source":"hallo allemaal","result":"hallau allemaal"}`; caveat: the API is not advertised anywhere machine-discoverable |
| [HTTP Link headers for discovery](https://specification.website/spec/agent-readiness/link-headers/) | recommended | ❌ | No `Link:` header on `/`, `/over`, or `/api` responses |
| [Agent Skills discovery](https://specification.website/spec/agent-readiness/agent-skills-discovery/) | recommended | ❌ | `/.well-known/skills` and `/.well-known/skills/index.json` → 404 |
| [/llms-full.txt](https://specification.website/spec/agent-readiness/llms-full-txt/) | optional | ❌ | `GET /llms-full.txt` → 404; would be trivially cheap for a 2-page site |
| [Content Signals in robots.txt](https://specification.website/spec/agent-readiness/content-signals/) | optional | ❌ | No robots.txt exists, so no `Content-Signal` directives possible |
| [Web Bot Auth — verifiable bot identity](https://specification.website/spec/agent-readiness/web-bot-auth/) | optional | — | Site applies no bot gating (no robots.txt, no challenges), so signature-based bot verification has nothing to hook into |
| [MCP and tool discovery](https://specification.website/spec/agent-readiness/mcp-and-tool-discovery/) | optional | ⚠️ | A "haags" MCP connector exists externally (claude.ai org marketplace), but the site itself exposes/advertises nothing: `/mcp`, `/api/mcp`, `/.well-known/mcp.json` all 404 and no DNS for mcp.haags.nu |
| [A2A agent cards](https://specification.website/spec/agent-readiness/a2a-agent-cards/) | optional | ❌ | `/.well-known/agent-card.json` → 404, despite the translation API being an obviously delegable capability |
| [DNS for AI Discovery (DNS-AID)](https://specification.website/spec/agent-readiness/dns-aid/) | optional | ❌ | `dig _agents.haags.nu HTTPS/SVCB` returns no records |
| [Agentic Resource Discovery (ARD)](https://specification.website/spec/agent-readiness/agentic-resource-discovery/) | optional | ❌ | `/.well-known/ai-catalog.json` → 404 |
| [NLWeb — conversational interface discovery](https://specification.website/spec/agent-readiness/nlweb/) | optional | ❌ | No `rel="nlweb"` link in HTML on either page; `/ask` → 404 |
| [WebMCP — browser-native tools for agents](https://specification.website/spec/agent-readiness/webmcp/) | optional | ❌ | No `navigator.modelContext` usage; only external script is gtag.js. The translate form would be a natural in-page tool |
| [Open Knowledge Format (OKF) bundle](https://specification.website/spec/agent-readiness/okf-bundle/) | optional | — | Site is a translation tool with a single about page, not a knowledge corpus; nothing to bundle |
| [Schemamap — discoverable JSON-LD endpoints per resource](https://specification.website/spec/agent-readiness/schemamap/) | optional | ❌ | `/schemamap.xml` → 404; no JSON-LD exists to index anyway |

### Findings

- **Stable URLs (⚠️)**: `https://www.haags.nu/` fails TLS verification (expired cert); with verification disabled it responds `301 → http://haags.nu/` (an insecure downgrade hop). `/over` and `/over/` both return 200 with identical content and no `rel="canonical"`.
- **robots.txt (❌)**: 404 with empty body — no crawler policy whatsoever, which also blocks Content Signals.
- **/llms.txt & /llms-full.txt (❌)**: both 404.
- **Structured data (❌)**: 0 occurrences of `application/ld+json` in `/` and `/over` HTML.
- **Link headers (❌)**: response header sets on `/`, `/over`, `/api` contain only IIS/security headers — no `Link:` advertising the JSON API, which is otherwise undiscoverable (`/api` with no params returns `{"source":null,"result":null}` rather than usage docs).
- **MCP (⚠️)**: an MCP connector named "haags" is available via the claude.ai org marketplace, but nothing on haags.nu points to it — `/mcp`, `/api/mcp`, `/.well-known/mcp.json` all 404; `mcp.haags.nu` has no DNS record. Agents landing on the site cannot find it.
- **Discovery well-knowns (❌)**: `/.well-known/agent-card.json`, `/.well-known/ai-catalog.json`, `/.well-known/skills`, `/.well-known/skills/index.json`, `/schemamap.xml` — all 404; `_agents.haags.nu` has no SVCB/HTTPS records.
- **Overview (⚠️)**: the strongest agent-readiness asset is that the site is fully SSR (complete HTML without JS) plus a clean JSON API; everything discovery-related is missing.

## 7. Performance (25 items)

_Core Web Vitals, caching, fonts._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Core Web Vitals (LCP, INP, CLS)](https://specification.website/spec/performance/core-web-vitals/) | required | 🔍 | Needs field/browser data; static signals good (SSR, ~11 KB HTML, inline CSS, `fetchpriority=high` on logo, aspect-ratio reserved) — only risk is render-blocking Google Fonts CSS |
| [Image optimisation](https://specification.website/spec/performance/image-optimization/) | required | ⚠️ | All images PNG — no WebP/AVIF, no srcset; logo.png 500×226 shown at ≤180px, icon 512×512 shown at 56px; payloads tiny (6.9 KB + 2.2 KB) and layout space reserved, so impact is minor |
| [Cache-Control headers](https://specification.website/spec/performance/cache-control/) | required | ⚠️ | Home HTML `no-cache, no-store` ✓; but `/over` has NO Cache-Control at all, and assets get `max-age=31536000` on non-fingerprinted URLs without `immutable` |
| [Compression (gzip, brotli, zstd)](https://specification.website/spec/performance/compression/) | required | ⚠️ | gzip works (11 460 → 4 693 B); `Accept-Encoding: br` or `zstd` alone returns uncompressed — no brotli/zstd support; images correctly uncompressed |
| [Lazy loading images, iframes, and video](https://specification.website/spec/performance/lazy-loading/) | recommended | ✅ | No iframes/video; only 2 small images, LCP logo not lazy-loaded (correct); nothing meaningful to defer |
| [Preload, prefetch, preconnect](https://specification.website/spec/performance/preload-prefetch-preconnect/) | recommended | ✅ | `preconnect` to fonts.googleapis.com and fonts.gstatic.com (with `crossorigin`); LCP image prioritised via `fetchpriority=high` |
| [Conditional requests (ETag, Last-Modified, 304)](https://specification.website/spec/performance/conditional-requests/) | recommended | ✅ | Assets send ETag + Last-Modified; both If-None-Match and If-Modified-Since return 304 with 0-byte body; HTML is no-store so needs no validator |
| [No-Vary-Search response header](https://specification.website/spec/performance/no-vary-search/) | recommended | ⚠️ | Header absent; low impact since no prefetch/query-parameter variants in use |
| [Web font loading](https://specification.website/spec/performance/font-loading/) | recommended | ⚠️ | Google Fonts third-party (not self-hosted WOFF2), 5 faces loaded; `display=swap` and preconnect present |
| [Critical CSS and render-blocking resources](https://specification.website/spec/performance/critical-css/) | recommended | ⚠️ | All site CSS inlined in `<head>` (good); but Google Fonts stylesheet `<link>` is a render-blocking third-party request in `<head>` |
| [Script loading — defer, async, module](https://specification.website/spec/performance/script-loading/) | recommended | ✅ | Inline script at end of `<body>`, gtag.js loaded `async`; no bare `<script src>` in `<head>` |
| [HTTP/2 and HTTP/3](https://specification.website/spec/performance/http3/) | recommended | ⚠️ | Served over HTTP/2; no HTTP/3 — no `alt-svc` header advertised (IIS 10) |
| [Speculation Rules](https://specification.website/spec/performance/speculation-rules/) | recommended | ⚠️ | No speculation rules; a prefetch/prerender of `/over` ↔ `/` would be a cheap win on this 2-page site |
| [Resource hints overview](https://specification.website/spec/performance/resource-hints/) | recommended | ✅ | Correct hint choice: preconnect for third-party font origins; no misused/duplicate hints |
| [View Transitions](https://specification.website/spec/performance/view-transitions/) | recommended | ⚠️ | No `@view-transition` opt-in in CSS; cross-document transitions unused |
| [Back/forward cache (BFCache)](https://specification.website/spec/performance/bfcache/) | recommended | ⚠️ | Home sends `Cache-Control: no-cache, no-store` (antiforgery cookie), which blocks BFCache in most browsers; `/over` has no blockers; no unload handlers |
| [Visibility-aware rendering](https://specification.website/spec/performance/visibility-aware-rendering/) | recommended | — | Pages are tiny with no long off-screen content; no scroll/resize listeners in shipped JS either |
| [Scrollbar gutter](https://specification.website/spec/performance/scrollbar-gutter/) | recommended | ⚠️ | No `scrollbar-gutter: stable` in CSS; centered `.wrapper` (max-width 81.25rem) can shift horizontally between overflowing and non-overflowing states |
| [Dynamic viewport units (dvh, svh, lvh)](https://specification.website/spec/performance/dynamic-viewport-units/) | recommended | ✅ | No `100vh` usage; layout uses `html,body{height:100%}` — mobile toolbar bug absent |
| [103 Early Hints](https://specification.website/spec/performance/early-hints/) | optional | — | No 103 interim response observed; optional and low value with CSS already inlined |
| [CSS containment](https://specification.website/spec/performance/css-containment/) | optional | — | Not used; trivial DOM makes containment unnecessary |
| [Scroll-driven animations](https://specification.website/spec/performance/scroll-driven-animations/) | optional | — | No scroll-linked effects at all (no JS scroll listeners to replace) |
| [Compression Dictionary Transport](https://specification.website/spec/performance/compression-dictionary-transport/) | optional | — | Not implemented; no external JS/CSS bundles that would benefit |
| [Server-Timing header](https://specification.website/spec/performance/server-timing/) | optional | — | Header not sent |
| [HTTP/1.1 workarounds: sharding, sprites, and bundling](https://specification.website/spec/performance/http1-workarounds/) | avoid | ✅ | No domain sharding, no sprites, no request-count bundling; single origin plus fonts/analytics |

### Findings

- **Compression ⚠️**: server only supports gzip. `Accept-Encoding: br` alone and `zstd` alone both return responses with no `content-encoding` (full 11 460 B body); gzip yields 4 693 B. Brotli would cut roughly another 15–20%.
- **Cache-Control ⚠️**: `/` sends `cache-control: no-cache, no-store` + `pragma: no-cache` (appropriate — page embeds an antiforgery token), but `/over` sends **no Cache-Control header at all**, leaving it to heuristic caching. Static assets (`/logo.png`, `/share.png`, `/manifest.json`, favicons) get `cache-control: public, max-age=31536000` on **non-fingerprinted URLs** without `immutable` — a changed logo.png would be stale for up to a year (mitigated only by revalidation-capable clients via ETag).
- **Image optimisation ⚠️**: all raster images are PNG; no WebP/AVIF, no `srcset`/`sizes`. `logo.png` is 500×226 displayed at max 180 px CSS width; `haags-vertaler-icon.png` is 512×512 displayed at 56 px. Absolute waste is small (6 890 B + 2 232 B). Logo `<img>` lacks width/height attributes but the `.logo-wrap{height:0;padding-bottom:45.2%}` wrapper reserves space, so no CLS.
- **Web font loading ⚠️**: fonts served from Google Fonts (fonts.googleapis.com CSS + fonts.gstatic.com WOFF2) rather than self-hosted; 4 Grandstander weights + Rubik requested. `display=swap` and preconnects are in place.
- **Critical CSS ⚠️**: all first-party CSS is inlined (~7 KB in `<style>`), but the Google Fonts `<link rel="stylesheet">` in `<head>` is render-blocking third-party CSS on both pages.
- **HTTP/2 and HTTP/3 ⚠️**: HTTP/2 confirmed; no `alt-svc` header on any response, so no HTTP/3 upgrade path.
- **BFCache ⚠️**: home's `no-store` makes it BFCache-ineligible in Chrome/Firefox; back-navigation to the translator (the page users most likely return to, with typed text) triggers a full reload. `/over` has no blockers.
- **Scrollbar gutter ⚠️**: `scrollbar-gutter` absent from shipped CSS; centered max-width layout can shift when navigating between pages that do/don't overflow vertically.
- **Speculation Rules / View Transitions / No-Vary-Search ⚠️**: none present — all recommended-tier, low effort on a 2-page site, low real-world impact.
- **Core Web Vitals 🔍**: requires field/lab measurement; static profile is favorable (SSR HTML 11.5 KB, inline CSS, `fetchpriority=high` on the logo, async-only analytics), with the fonts stylesheet as the main LCP risk.

## 8. Privacy (6 items)

_Consent and visitor choice._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Privacy policy](https://specification.website/spec/privacy/privacy-policy/) | required | ❌ | No privacy policy exists: no link on `/` or `/over`; `/privacy`, `/privacy-policy`, `/privacybeleid`, `/cookies` all 404. One is needed since GA4 collects personal data. |
| [Cookie consent](https://specification.website/spec/privacy/cookie-consent/) | required | ❌ | Google Analytics 4 (`gtag/js?id=G-4L4JPR01EL`) loads unconditionally on both pages with plain `gtag('config', ...)` — no consent banner, no Consent Mode; GA sets `_ga*` cookies without opt-in on a Dutch (EU) site. Only the essential antiforgery cookie is exempt. |
| [Global Privacy Control (GPC)](https://specification.website/spec/privacy/global-privacy-control/) | recommended | ❌ | Signal ignored: with `Sec-GPC: 1` the GA script is still served identically; no client-side `navigator.globalPrivacyControl` check in shipped JS; `/.well-known/gpc.json` → 404. |
| [Third-party scripts and privacy](https://specification.website/spec/privacy/third-party-scripts/) | recommended | ⚠️ | One third-party script: `www.googletagmanager.com/gtag/js`, loaded without consent gating on both pages. Additionally Google Fonts CSS/fonts load from Google, exposing visitor IPs (not self-hosted). |
| [Privacy-respecting analytics](https://specification.website/spec/privacy/analytics-privacy/) | recommended | ❌ | Analytics is GA4 (ad-tech, US-hosted, cookie-based), configured with no Consent Mode or IP anonymisation, plus custom `gtag('event','click',...)` tracking on outbound "ads" links; no cookieless/EU-hosted alternative. |
| [Data minimisation](https://specification.website/spec/privacy/data-minimization/) | recommended | ⚠️ | First-party collection is minimal (single `Source` textarea max 5000 chars, no accounts, contact via mailto, only an antiforgery cookie) — but GA4 adds device/behavioral data collection with no stated purpose or retention policy. |

### Findings

- **Privacy policy (❌):** Zero occurrences of "privacy", "cookie", "AVG", or "GDPR" in the HTML of `/` and `/over`; the only footer links are Vertaler, Over, Q42, Twitter handles, and a mailto. `/privacy`, `/privacy-policy`, `/privacybeleid`, `/cookies` all return 404.
- **Cookie consent (❌):** Both pages ship `<script async src="https://www.googletagmanager.com/gtag/js?id=G-4L4JPR01EL">` followed by inline `gtag('js', new Date()); gtag('config', 'G-4L4JPR01EL');` with no `gtag('consent', 'default', ...)` call. Grep for consent/banner/toestemming: 0 hits on both pages.
- **GPC (❌):** `curl -H 'Sec-GPC: 1' https://haags.nu/` still contains the googletagmanager script; `/.well-known/gpc.json` → HTTP 404; no `globalPrivacyControl` reference in the inline JS.
- **Third-party scripts (⚠️):** googletagmanager.com script plus Google Fonts stylesheet with preconnects to `fonts.gstatic.com` — both fire for every visitor before any consent; fonts could be self-hosted to avoid IP disclosure to Google.
- **Privacy-respecting analytics (❌):** GA4 measurement ID `G-4L4JPR01EL` on both pages; inline JS also sends click events (`event_category: "ads"`, labels `haagsnl-link` / `chrome-extension`) to GA. No privacy-focused alternative (Plausible/Matomo/etc.) detected.
- **Data minimisation (⚠️):** The translator form itself is exemplary (one text field, POST to `/`, essential antiforgery token only), but GA4's default collection (IP, device, page behavior) exceeds what the product needs and is undocumented anywhere on the site.

## 9. Resilience (6 items)

_Graceful failure._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [Custom error pages (404, 500)](https://specification.website/spec/resilience/error-pages/) | required | ❌ | 404 returns correct status but an empty body (`content-length: 0`) — no custom error page, no explanation or way forward; `server: Microsoft-IIS/10.0` header leaks platform info |
| [Maintenance pages and 503](https://specification.website/spec/resilience/maintenance-pages/) | recommended | 🔍 | Site is live; 503/Retry-After behaviour during intentional downtime cannot be verified externally — no static evidence of a maintenance page either way |
| [Graceful degradation when JavaScript fails](https://specification.website/spec/resilience/graceful-degradation/) | recommended | ✅ | Fully SSR; translator is a real `<form method="post" action="/">` and a no-JS POST returns the translation ("hallo" → "hallau"); nav is plain links; only JS-dependent "Wis tekst"/"Kopieer" buttons render as no-ops without JS |
| [Web app manifest](https://specification.website/spec/resilience/pwa-manifest/) | recommended | ⚠️ | `/manifest.json` linked on both pages with name, short_name, display, theme/background colours; but missing `start_url` and only one 192x192 icon (no 512px, no maskable); theme_color (#c7c49f) mismatches the `<meta name="theme-color">` (#ffffff) |
| [Monitoring and uptime](https://specification.website/spec/resilience/monitoring-uptime/) | recommended | 🔍 | External synthetic monitoring/RUM/status page not verifiable from outside; no status page discovered |
| [Offline support and service workers](https://specification.website/spec/resilience/offline-support/) | optional | — | No service worker: no `serviceWorker` registration in HTML; /sw.js, /service-worker.js, /serviceworker.js all 404; optional item, not implemented |

### Findings

- **Custom error pages ❌**: `GET https://haags.nu/deze-pagina-bestaat-niet-xyz` → `HTTP/2 404` with `content-length: 0` and an entirely empty body (same with `Accept: text/html`). Status code is correct, but there is no page at all — no plain-language explanation, no link back to the homepage. Also `server: Microsoft-IIS/10.0` exposes the platform. Could not trigger a 500 to inspect that variant.
- **Web app manifest ⚠️**: `/manifest.json` (257 bytes) contains `name`, `short_name`, `display: standalone`, one icon (192×192), `background_color`/`theme_color: #c7c49f`. Missing `start_url`, no 512×512 icon, no `purpose: maskable` icon, and manifest `theme_color` contradicts the HTML `<meta name="theme-color" content="#ffffff">`.
- **Maintenance pages / 503 🔍 and Monitoring 🔍**: both depend on infrastructure/process not observable via HTTP while the site is healthy; no status-page URL or monitoring artefacts were discoverable from the deployed site.

## 10. Internationalisation (13 items)

_Language, locale, direction._

| Item | Status | Result | Notes |
| --- | --- | --- | --- |
| [lang attribute on inline content](https://specification.website/spec/i18n/lang-attribute/) | required | ✅ | Document is `lang="nl"`; Haags passages are a Dutch dialect with no BCP 47 subtag, so `nl` covers them — no foreign-language inline content to mark |
| [International URL structure](https://specification.website/spec/i18n/international-url-structure/) | recommended | — | Single-language Dutch site on one host (haags.nu); no locale variants, so no URL pattern needed |
| [hreflang for language and regional URLs](https://specification.website/spec/i18n/hreflang/) | recommended | — | No language/regional alternates exist; hreflang correctly absent on both pages |
| [Localised page metadata](https://specification.website/spec/i18n/localised-metadata/) | recommended | ✅ | Title, meta description, and all OG fields are in Dutch/Haags; image alt texts Dutch; no English leakage in head (only unused ASP.NET `data-val-*` validation messages are English) |
| [Language switcher](https://specification.website/spec/i18n/language-switcher/) | recommended | — | Single-locale site; no switcher needed, and no flags misused |
| [RTL and bidirectional text](https://specification.website/spec/i18n/rtl-support/) | recommended | — | No RTL locales served; CSS uses physical properties but nothing to mirror |
| [Locale-aware content](https://specification.website/spec/i18n/locale-content/) | recommended | ✅ | Only formatted value is the footer date "07-06-2026" in Dutch DD-MM-YYYY convention, matching the `nl` locale |
| [Plural rules and grammatical number](https://specification.website/spec/i18n/plural-rules/) | recommended | — | No dynamic counts or pluralised strings anywhere in the UI; all copy is static Dutch |
| [hreflang in XML sitemaps](https://specification.website/spec/i18n/sitemap-hreflang/) | optional | — | /sitemap.xml returns 404 and there are no locale alternates to declare |
| [translate attribute for untranslatable content](https://specification.website/spec/i18n/translate-attribute/) | optional | ⚠️ | No `translate="no"` anywhere; the Haags dialect output (`.js-haags` textarea), Haags taglines, and brand "Haags.nu" would be mangled by Chrome/Google auto-translate — the one site type where this attribute really pays off |
| [Writing modes and CJK line breaking](https://specification.website/spec/i18n/writing-modes/) | optional | — | No CJK/vertical-script content served |
| [Internationalised Domain Names (IDN)](https://specification.website/spec/i18n/idn-support/) | optional | — | ASCII-only domain (haags.nu); no IDN in use or needed |
| [Avoid automatic IP-based language redirects](https://specification.website/spec/i18n/avoid-auto-geo-redirects/) | avoid | ✅ | Requests with `Accept-Language: en-US` and `ar` both return HTTP 200 with the same `lang="nl"` page — no geo/language redirect |

### Findings

- **translate attribute ⚠️**: zero `translate=` attributes on either page. The site's core output is Haags dialect text (the readonly `.js-haags` textarea, `<span>Vetaal Neidâhlans naah ut Haags</span>`, extension-promo copy "Leis élke website in ut Haags" / "Nâh as Chraume-ekstensie!"). Since the page is `lang="nl"`, Chrome offers auto-translation to non-Dutch visitors and would rewrite the Haags output — defeating the site's purpose. Adding `translate="no"` on the Haags result textarea, the Haags taglines, and the "Haags.nu" brand name would fix this.

## Prioritised fix list

1. **Fix or retire `www.haags.nu`** — the certificate expired 2024-03-18 and the redirect chain downgrades HTTPS→HTTP. Renew the cert and 301 `https://www.haags.nu` → `https://haags.nu/` in one hop. (security/seo/agent-readiness, required)
2. **Add cookie consent or drop GA4** — GA4 loads without consent on an EU site; either add a consent banner + Consent Mode, or switch to a cookieless privacy-friendly analytics tool. Add a privacy policy page either way. (privacy, 2× required ❌)
3. **Set `Secure` on the antiforgery cookie** — `Cookie.SecurePolicy = CookieSecurePolicy.Always` (ideally with a `__Host-` name prefix). (security, required ❌)
4. **Fix focus visibility** — remove `textarea:focus{outline:0}` or give both textareas a ≥3:1 focus indicator; also add a skip link and visible form error messages. (accessibility, 3× required ❌)
5. **Add a custom 404 page** — the current 404 body is empty. (resilience, required ❌)
6. **Serve the missing discovery files** — `robots.txt` (with AI-crawler policy + Sitemap line), `sitemap.xml`, `/.well-known/security.txt`, `/llms.txt`, and JSON-LD (`WebApplication`) on `/`. All trivial static wins for a 2-page site. (seo/security/agent-readiness, recommended)
7. **Add per-page canonicals and unique titles/descriptions** — `/over` currently duplicates the home title, description, and og:url, and duplicate URL variants return 200. (foundations/seo)
8. **Add baseline security headers** — CSP (start report-only), `Referrer-Policy`, `Permissions-Policy`, dedupe the conflicting `X-Frame-Options` on `/`, raise HSTS max-age. (security, recommended)
9. **Add `translate="no"` to Haags content** — prevents browser auto-translate from mangling the dialect output. (i18n, cheap and high-value for this site)
10. **Performance polish** — enable brotli, add Cache-Control to `/over`, self-host the two font families (also a privacy win), fix the manifest (`start_url`, 512px + maskable icons, consistent theme_color).

## Remaining manual checks

- **Core Web Vitals** (performance, 🔍): measure LCP/INP/CLS in the field or with Lighthouse; static profile is favourable.
- **Keyboard navigation** (accessibility, 🔍): tab through both pages in a browser; static evidence suggests it works (native elements only), but focus visibility is already known-broken.
- **Maintenance pages / 503** (resilience, 🔍): verify the deployment procedure serves a 503 + `Retry-After` during planned downtime.
- **Monitoring and uptime** (resilience, 🔍): confirm synthetic monitoring/alerting exists for the site (not externally observable).
- **Colour contrast rendered check** (accessibility, ⚠️): computed ratios pass for text; verify in-browser rendering, especially the textarea boundary (<3:1 non-text contrast).
