# Website Specification status

Living status of haags.nu against [The Website Specification](https://specification.website). This file is updated on every audit run so that git shows exactly which items changed between audits. Detailed findings, evidence, and prioritised fix lists live in the dated reports next to this file (latest: [2026-07-03](website-spec-audit-2026-07-03.md)).

- **Last audited:** 2026-07-03
- **Environment:** production — `https://haags.nu`
- **Audited URLs:** `/` · `/over`

## Scoreboard

Score = (✅ + 🚫) / applicable items.
Legend: ✅ pass · ⚠️ partial · ❌ fail · 🔍 manual check needed · — not applicable · 🚫 intentionally skipped (team decision, `👤 date` in the Intentionally-ignored column)

| Category | ✅ | 🚫 | ⚠️ | ❌ | 🔍 | — | Score |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Foundations | 5 | 0 | 4 | 3 | 0 | 8 | 5/12 |
| SEO | 5 | 0 | 2 | 3 | 0 | 4 | 5/10 |
| Accessibility | 10 | 0 | 5 | 3 | 1 | 6 | 10/19 |
| Security | 2 | 0 | 4 | 10 | 0 | 1 | 2/16 |
| Well-Known URIs | 0 | 0 | 1 | 1 | 0 | 8 | 0/2 |
| Agent Readiness | 1 | 0 | 3 | 13 | 0 | 3 | 1/17 |
| Performance | 7 | 0 | 11 | 0 | 1 | 6 | 7/19 |
| Privacy | 0 | 0 | 2 | 4 | 0 | 0 | 0/6 |
| Resilience | 1 | 0 | 1 | 1 | 2 | 1 | 1/5 |
| Internationalisation | 4 | 0 | 1 | 0 | 0 | 8 | 4/5 |
| **Total** | **35** | **0** | **34** | **38** | **4** | **45** | **35/111** |

## 1. Foundations (20 items)

_HTML, head, document basics._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [The HTML doctype](https://specification.website/spec/foundations/doctype/) | required | ✅ |  |  |
| [The lang attribute on `<html>`](https://specification.website/spec/foundations/html-lang/) | required | ✅ |  |  |
| [`<meta charset>`](https://specification.website/spec/foundations/meta-charset/) | required | ✅ |  |  |
| [`<meta viewport>`](https://specification.website/spec/foundations/meta-viewport/) | required | ✅ |  |  |
| [The `<title>` element](https://specification.website/spec/foundations/title/) | required | ⚠️ |  | `/over` reuses the home title verbatim; should be unique per page |
| [`<meta name="description">`](https://specification.website/spec/foundations/meta-description/) | recommended | ⚠️ |  | Present but identical on `/` and `/over` |
| [Canonical URL (rel="canonical")](https://specification.website/spec/foundations/canonical-url/) | recommended | ❌ |  | No `rel="canonical"` anywhere; `/over`, `/over/`, `/OVER` all 200 |
| [Favicons and app icons](https://specification.website/spec/foundations/favicons/) | recommended | ⚠️ |  | favicon.ico + PNGs + apple-touch-icon OK; no SVG favicon, manifest lacks 512px/maskable icon |
| [`<meta name="theme-color">`](https://specification.website/spec/foundations/theme-color/) | recommended | ✅ |  | `#ffffff`; mismatches manifest `theme_color` `#c7c49f` |
| [`<meta name="color-scheme">`](https://specification.website/spec/foundations/color-scheme/) | recommended | ❌ |  | No color-scheme meta or CSS property |
| [Open Graph protocol](https://specification.website/spec/foundations/open-graph/) | recommended | ⚠️ |  | Missing `og:type`, image dimensions, `twitter:card`; `og:url` on `/over` points at homepage |
| [Feed discovery with rel="alternate"](https://specification.website/spec/foundations/feed-discovery/) | recommended | — |  | No feed published |
| [Feed content hygiene](https://specification.website/spec/foundations/feed-hygiene/) | recommended | — |  | No feed published |
| [Popover API](https://specification.website/spec/foundations/popover-api/) | recommended | — |  | No modals, menus, or tooltips |
| [CSS anchor positioning](https://specification.website/spec/foundations/anchor-positioning/) | recommended | — |  | No tethered UI to position |
| [Balanced text wrapping](https://specification.website/spec/foundations/text-wrap/) | recommended | ❌ |  | No `text-wrap: balance`/`pretty`; multi-line headings would benefit |
| [CSS container queries](https://specification.website/spec/foundations/container-queries/) | recommended | — |  | Single page-level layout; no components in varying containers |
| [Invoker commands](https://specification.website/spec/foundations/invoker-commands/) | recommended | — |  | No popovers/dialogs to wire |
| [Entry and exit animations](https://specification.website/spec/foundations/entry-exit-animations/) | optional | — |  | No elements enter/exit the DOM or top layer |
| [Content-based field sizing](https://specification.website/spec/foundations/field-sizing/) | optional | — |  | Fixed min-height textareas; no auto-grow hack either |

## 2. SEO (14 items)

_Search visibility._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Redirects (301/302/308)](https://specification.website/spec/seo/redirects/) | required | ⚠️ |  | www chains 3 hops incl. HTTPS→HTTP downgrade; www cert expired |
| [Meta robots and X-Robots-Tag](https://specification.website/spec/seo/meta-robots/) | required | ✅ |  |  |
| [Heading hierarchy](https://specification.website/spec/seo/heading-hierarchy/) | required | ✅ |  |  |
| [robots.txt](https://specification.website/spec/seo/robots-txt/) | recommended | ❌ |  | `/robots.txt` → 404 |
| [XML sitemaps](https://specification.website/spec/seo/xml-sitemaps/) | recommended | ❌ |  | `/sitemap.xml` → 404 |
| [Sitemap index files](https://specification.website/spec/seo/sitemap-index/) | recommended | — |  | ~2 URLs; index file not applicable |
| [URL structure](https://specification.website/spec/seo/url-structure/) | recommended | ⚠️ |  | Clean URLs, but slash/case/query variants all 200 with no canonical |
| [Server-side rendering](https://specification.website/spec/seo/server-side-rendering/) | recommended | ✅ |  |  |
| [Internal linking](https://specification.website/spec/seo/internal-linking/) | recommended | ✅ |  |  |
| [Structured data (JSON-LD)](https://specification.website/spec/seo/structured-data/) | recommended | ❌ |  | No JSON-LD on either page; WebSite/WebApplication schema would fit |
| [Breadcrumbs](https://specification.website/spec/seo/breadcrumbs/) | recommended | — |  | Flat two-page site |
| [Image and video sitemap extensions](https://specification.website/spec/seo/image-sitemaps/) | optional | — |  | No sitemap and no crawl-hidden media |
| [IndexNow](https://specification.website/spec/seo/indexnow/) | optional | — |  | No evidence of adoption; not statically verifiable |
| [Soft 404s](https://specification.website/spec/seo/soft-404/) | avoid | ✅ |  | Missing pages return genuine 404 status (body empty — see resilience) |

## 3. Accessibility (25 items)

_WCAG-aligned rules._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Colour contrast](https://specification.website/spec/accessibility/color-contrast/) | required | ⚠️ |  | Text passes; textarea boundary ~1.2:1 vs page bg (<3:1 non-text) |
| [Image alt text](https://specification.website/spec/accessibility/image-alt-text/) | required | ✅ |  |  |
| [Form labels](https://specification.website/spec/accessibility/form-labels/) | required | ✅ |  |  |
| [Keyboard navigation](https://specification.website/spec/accessibility/keyboard-navigation/) | required | 🔍 |  | Static evidence good (native elements, no tabindex); needs live check |
| [Visible focus indicators](https://specification.website/spec/accessibility/focus-indicators/) | required | ❌ |  | `textarea:focus{outline:0}`; readonly textarea gets no replacement; input's replacement border 2.42:1 |
| [Skip links](https://specification.website/spec/accessibility/skip-links/) | required | ❌ |  | No skip link on either page |
| [Semantic HTML and landmarks](https://specification.website/spec/accessibility/semantic-html/) | required | ✅ |  | Minor: `<header>` nested inside `<main>`, no banner landmark |
| [Descriptive link text](https://specification.website/spec/accessibility/link-text/) | required | ✅ |  |  |
| [Accessible form errors](https://specification.website/spec/accessibility/form-errors/) | required | ❌ |  | Empty submit re-renders identical page, no error text; data-val-* attrs inert (no validation script) |
| [Document and parts language](https://specification.website/spec/accessibility/document-language/) | required | ✅ |  |  |
| [Reduced motion](https://specification.website/spec/accessibility/reduced-motion/) | required | ⚠️ |  | No prefers-reduced-motion query; only 0.2s hover transitions incl. scale |
| [Captions and transcripts](https://specification.website/spec/accessibility/captions-and-transcripts/) | required | — |  | No audio or video content |
| [Accessible data tables](https://specification.website/spec/accessibility/data-tables/) | required | — |  | No tabular data |
| [Touch target size](https://specification.website/spec/accessibility/touch-target-size/) | required | ⚠️ |  | "Wis tekst" ~14–16px tall (<24px); marginal spacing exception |
| [Forced colours mode](https://specification.website/spec/accessibility/forced-colors/) | recommended | ⚠️ |  | No forced-colors CSS; `outline:0` also kills focus in high-contrast mode |
| [The inert attribute](https://specification.website/spec/accessibility/inert-attribute/) | recommended | — |  | No overlays, dialogs, or off-canvas UI |
| [ARIA — first rule of ARIA](https://specification.website/spec/accessibility/aria-usage/) | recommended | ✅ |  |  |
| [Accessible authentication](https://specification.website/spec/accessibility/accessible-authentication/) | recommended | — |  | No authentication |
| [Redundant entry](https://specification.website/spec/accessibility/redundant-entry/) | recommended | — |  | No multi-step processes |
| [Hidden until found](https://specification.website/spec/accessibility/hidden-until-found/) | recommended | — |  | No collapsible content |
| [Mobile-friendly form inputs](https://specification.website/spec/accessibility/mobile-form-inputs/) | recommended | ✅ |  |  |
| [Native interactive elements](https://specification.website/spec/accessibility/native-interactive-elements/) | recommended | ✅ |  |  |
| [CSS state and relational selectors](https://specification.website/spec/accessibility/css-state-selectors/) | recommended | ⚠️ |  | No `:has()`/`:user-invalid`/`:focus-within` despite form constraints; low applicability |
| [Empty links and buttons](https://specification.website/spec/accessibility/empty-links-buttons/) | avoid | ✅ |  |  |
| [Accessibility overlays](https://specification.website/spec/accessibility/accessibility-overlays/) | avoid | ✅ |  |  |

## 4. Security (17 items)

_Headers, transport, policies._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [HTTPS and TLS](https://specification.website/spec/security/https-tls/) | required | ⚠️ |  | Apex OK (TLS 1.2/1.3); www cert expired 2024-03-18 and http://www 301s into it |
| [HSTS (Strict-Transport-Security)](https://specification.website/spec/security/hsts/) | required | ⚠️ |  | max-age=2592000 (30 days); no includeSubDomains |
| [X-Content-Type-Options: nosniff](https://specification.website/spec/security/x-content-type-options/) | required | ✅ |  |  |
| [Clickjacking protection (frame-ancestors / X-Frame-Options)](https://specification.website/spec/security/frame-ancestors/) | required | ⚠️ |  | No CSP frame-ancestors; `/` sends conflicting XFO `SAMEORIGIN` + `DENY` |
| [Cookie attributes — Secure, HttpOnly, SameSite](https://specification.website/spec/security/cookie-attributes/) | required | ❌ |  | Antiforgery cookie missing `Secure` flag and `__Host-` prefix |
| [Mixed content and upgrade-insecure-requests](https://specification.website/spec/security/mixed-content/) | recommended | ⚠️ |  | No insecure subresources found, but no CSP safety net |
| [Content Security Policy (CSP)](https://specification.website/spec/security/content-security-policy/) | recommended | ❌ |  | No CSP header or meta |
| [Reporting API (Reporting-Endpoints)](https://specification.website/spec/security/reporting-endpoints/) | recommended | ❌ |  | No Reporting-Endpoints/Report-To header |
| [/.well-known/security.txt](https://specification.website/spec/security/security-txt/) | recommended | ❌ |  | Both locations 404 |
| [Cross-origin isolation (COOP / COEP / CORP)](https://specification.website/spec/security/cross-origin-isolation/) | recommended | ❌ |  | No COOP/COEP/CORP headers |
| [Referrer-Policy](https://specification.website/spec/security/referrer-policy/) | recommended | ❌ |  | Header absent |
| [Permissions-Policy](https://specification.website/spec/security/permissions-policy/) | recommended | ❌ |  | Header absent |
| [Subresource Integrity (SRI)](https://specification.website/spec/security/subresource-integrity/) | recommended | ❌ |  | gtag.js and Google Fonts CSS load without integrity (dynamic — would need self-hosting) |
| [Trusted Types](https://specification.website/spec/security/trusted-types/) | recommended | ❌ |  | No CSP, so no trusted-types directives |
| [DNS CAA records](https://specification.website/spec/security/caa-records/) | recommended | ❌ |  | No CAA records on haags.nu |
| [Clear-Site-Data](https://specification.website/spec/security/clear-site-data/) | optional | — |  | No login/session flows |
| [DNSSEC](https://specification.website/spec/security/dnssec/) | optional | ✅ |  |  |

## 5. Well-Known URIs (10 items)

_Standard /.well-known/ paths._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Well-known URIs](https://specification.website/spec/well-known/well-known-overview/) | recommended | ⚠️ |  | Zero well-known URIs published; 404 handling itself is correct (no soft-404) |
| [/.well-known/api-catalog](https://specification.website/spec/well-known/api-catalog/) | recommended | ❌ |  | 404 and no `Link: rel="api-catalog"` header; `/api` endpoint undocumented |
| [/.well-known/change-password](https://specification.website/spec/well-known/change-password/) | optional | — |  | No user accounts |
| [/.well-known/webauthn](https://specification.website/spec/well-known/webauthn/) | optional | — |  | No passkeys/WebAuthn |
| [/.well-known/openid-configuration](https://specification.website/spec/well-known/openid-configuration/) | optional | — |  | Not an OIDC provider |
| [/.well-known/webfinger](https://specification.website/spec/well-known/webfinger/) | optional | — |  | No Fediverse presence |
| [/.well-known/apple-app-site-association](https://specification.website/spec/well-known/apple-app-site-association/) | optional | — |  | No iOS app |
| [/.well-known/assetlinks.json](https://specification.website/spec/well-known/assetlinks-json/) | optional | — |  | No Android app |
| [/.well-known/nodeinfo](https://specification.website/spec/well-known/nodeinfo/) | optional | — |  | Not a federated platform |
| [/.well-known/traffic-advice](https://specification.website/spec/well-known/traffic-advice/) | optional | — |  | Default prefetch behaviour acceptable |

## 6. Agent Readiness (20 items)

_Discoverability by AI agents._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Stable URLs](https://specification.website/spec/agent-readiness/stable-urls/) | required | ⚠️ |  | www variant broken (expired cert, HTTP downgrade); duplicate `/over` variants without canonical |
| [Agent readiness](https://specification.website/spec/agent-readiness/agent-readiness-overview/) | recommended | ⚠️ |  | Strong base (full SSR, JSON API); all discovery files missing |
| [/llms.txt](https://specification.website/spec/agent-readiness/llms-txt/) | recommended | ❌ |  | 404 |
| [Per-page Markdown source endpoints](https://specification.website/spec/agent-readiness/markdown-source-endpoints/) | recommended | — |  | Not a documentation site |
| [robots.txt for AI crawlers](https://specification.website/spec/agent-readiness/robots-for-ai-crawlers/) | recommended | ❌ |  | No robots.txt at all |
| [Structured data for agents](https://specification.website/spec/agent-readiness/structured-data-for-agents/) | recommended | ❌ |  | Zero JSON-LD blocks |
| [Machine-readable formats](https://specification.website/spec/agent-readiness/machine-readable-formats/) | recommended | ✅ |  | `/api?text=` returns clean JSON; API not advertised anywhere discoverable |
| [HTTP Link headers for discovery](https://specification.website/spec/agent-readiness/link-headers/) | recommended | ❌ |  | No `Link:` header on any response |
| [Agent Skills discovery](https://specification.website/spec/agent-readiness/agent-skills-discovery/) | recommended | ❌ |  | `/.well-known/skills` → 404 |
| [/llms-full.txt](https://specification.website/spec/agent-readiness/llms-full-txt/) | optional | ❌ |  | 404 |
| [Content Signals in robots.txt](https://specification.website/spec/agent-readiness/content-signals/) | optional | ❌ |  | No robots.txt to carry directives |
| [Web Bot Auth — verifiable bot identity](https://specification.website/spec/agent-readiness/web-bot-auth/) | optional | — |  | No bot gating to hook into |
| [MCP and tool discovery](https://specification.website/spec/agent-readiness/mcp-and-tool-discovery/) | optional | ⚠️ |  | "haags" MCP connector exists externally (claude.ai marketplace) but nothing on the site points to it |
| [A2A agent cards](https://specification.website/spec/agent-readiness/a2a-agent-cards/) | optional | ❌ |  | `/.well-known/agent-card.json` → 404 |
| [DNS for AI Discovery (DNS-AID)](https://specification.website/spec/agent-readiness/dns-aid/) | optional | ❌ |  | No `_agents.haags.nu` records |
| [Agentic Resource Discovery (ARD)](https://specification.website/spec/agent-readiness/agentic-resource-discovery/) | optional | ❌ |  | `/.well-known/ai-catalog.json` → 404 |
| [NLWeb — conversational interface discovery](https://specification.website/spec/agent-readiness/nlweb/) | optional | ❌ |  | No `rel="nlweb"` link; `/ask` → 404 |
| [WebMCP — browser-native tools for agents](https://specification.website/spec/agent-readiness/webmcp/) | optional | ❌ |  | No `navigator.modelContext` usage |
| [Open Knowledge Format (OKF) bundle](https://specification.website/spec/agent-readiness/okf-bundle/) | optional | — |  | Not a knowledge corpus |
| [Schemamap — discoverable JSON-LD endpoints per resource](https://specification.website/spec/agent-readiness/schemamap/) | optional | ❌ |  | `/schemamap.xml` → 404; no JSON-LD to index |

## 7. Performance (25 items)

_Core Web Vitals, caching, fonts._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Core Web Vitals (LCP, INP, CLS)](https://specification.website/spec/performance/core-web-vitals/) | required | 🔍 |  | Needs field data; static profile favourable, fonts stylesheet main LCP risk |
| [Image optimisation](https://specification.website/spec/performance/image-optimization/) | required | ⚠️ |  | PNG only, no WebP/AVIF or srcset; payloads tiny so impact minor |
| [Cache-Control headers](https://specification.website/spec/performance/cache-control/) | required | ⚠️ |  | `/over` has no Cache-Control; 1-year max-age on non-fingerprinted assets |
| [Compression (gzip, brotli, zstd)](https://specification.website/spec/performance/compression/) | required | ⚠️ |  | gzip only; no brotli/zstd |
| [Lazy loading images, iframes, and video](https://specification.website/spec/performance/lazy-loading/) | recommended | ✅ |  |  |
| [Preload, prefetch, preconnect](https://specification.website/spec/performance/preload-prefetch-preconnect/) | recommended | ✅ |  |  |
| [Conditional requests (ETag, Last-Modified, 304)](https://specification.website/spec/performance/conditional-requests/) | recommended | ✅ |  |  |
| [No-Vary-Search response header](https://specification.website/spec/performance/no-vary-search/) | recommended | ⚠️ |  | Absent; low impact without prefetch/query variants |
| [Web font loading](https://specification.website/spec/performance/font-loading/) | recommended | ⚠️ |  | Google Fonts third-party, 5 faces; display=swap and preconnect present |
| [Critical CSS and render-blocking resources](https://specification.website/spec/performance/critical-css/) | recommended | ⚠️ |  | First-party CSS inlined; Google Fonts stylesheet render-blocking |
| [Script loading — defer, async, module](https://specification.website/spec/performance/script-loading/) | recommended | ✅ |  |  |
| [HTTP/2 and HTTP/3](https://specification.website/spec/performance/http3/) | recommended | ⚠️ |  | HTTP/2 only; no alt-svc for HTTP/3 |
| [Speculation Rules](https://specification.website/spec/performance/speculation-rules/) | recommended | ⚠️ |  | None; prefetch of `/over` ↔ `/` would be cheap |
| [Resource hints overview](https://specification.website/spec/performance/resource-hints/) | recommended | ✅ |  |  |
| [View Transitions](https://specification.website/spec/performance/view-transitions/) | recommended | ⚠️ |  | No `@view-transition` opt-in |
| [Back/forward cache (BFCache)](https://specification.website/spec/performance/bfcache/) | recommended | ⚠️ |  | Home's `no-store` blocks BFCache; `/over` fine |
| [Visibility-aware rendering](https://specification.website/spec/performance/visibility-aware-rendering/) | recommended | — |  | Tiny pages, no long off-screen content |
| [Scrollbar gutter](https://specification.website/spec/performance/scrollbar-gutter/) | recommended | ⚠️ |  | No `scrollbar-gutter: stable`; centered layout can shift |
| [Dynamic viewport units (dvh, svh, lvh)](https://specification.website/spec/performance/dynamic-viewport-units/) | recommended | ✅ |  |  |
| [103 Early Hints](https://specification.website/spec/performance/early-hints/) | optional | — |  | Low value with CSS already inlined |
| [CSS containment](https://specification.website/spec/performance/css-containment/) | optional | — |  | Trivial DOM |
| [Scroll-driven animations](https://specification.website/spec/performance/scroll-driven-animations/) | optional | — |  | No scroll-linked effects |
| [Compression Dictionary Transport](https://specification.website/spec/performance/compression-dictionary-transport/) | optional | — |  | No external bundles that would benefit |
| [Server-Timing header](https://specification.website/spec/performance/server-timing/) | optional | — |  | Not sent |
| [HTTP/1.1 workarounds: sharding, sprites, and bundling](https://specification.website/spec/performance/http1-workarounds/) | avoid | ✅ |  |  |

## 8. Privacy (6 items)

_Consent and visitor choice._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Privacy policy](https://specification.website/spec/privacy/privacy-policy/) | required | ❌ |  | No policy page or link anywhere; needed since GA4 collects personal data |
| [Cookie consent](https://specification.website/spec/privacy/cookie-consent/) | required | ❌ |  | GA4 loads unconditionally, no consent banner or Consent Mode, on an EU site |
| [Global Privacy Control (GPC)](https://specification.website/spec/privacy/global-privacy-control/) | recommended | ❌ |  | Sec-GPC ignored; no gpc.json |
| [Third-party scripts and privacy](https://specification.website/spec/privacy/third-party-scripts/) | recommended | ⚠️ |  | gtag.js + Google Fonts fire pre-consent; fonts could be self-hosted |
| [Privacy-respecting analytics](https://specification.website/spec/privacy/analytics-privacy/) | recommended | ❌ |  | GA4 without Consent Mode; no privacy-friendly alternative |
| [Data minimisation](https://specification.website/spec/privacy/data-minimization/) | recommended | ⚠️ |  | First-party collection minimal; GA4 adds undocumented device/behavioural collection |

## 9. Resilience (6 items)

_Graceful failure._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [Custom error pages (404, 500)](https://specification.website/spec/resilience/error-pages/) | required | ❌ |  | 404 status correct but body completely empty; `server: Microsoft-IIS/10.0` leaks platform |
| [Maintenance pages and 503](https://specification.website/spec/resilience/maintenance-pages/) | recommended | 🔍 |  | Not verifiable externally while site is healthy |
| [Graceful degradation when JavaScript fails](https://specification.website/spec/resilience/graceful-degradation/) | recommended | ✅ |  |  |
| [Web app manifest](https://specification.website/spec/resilience/pwa-manifest/) | recommended | ⚠️ |  | Missing `start_url`, 512px/maskable icons; theme_color mismatches meta |
| [Monitoring and uptime](https://specification.website/spec/resilience/monitoring-uptime/) | recommended | 🔍 |  | Not verifiable externally; no status page discovered |
| [Offline support and service workers](https://specification.website/spec/resilience/offline-support/) | optional | — |  | No service worker; optional, not implemented |

## 10. Internationalisation (13 items)

_Language, locale, direction._

| Item | Status | Result | Intentionally ignored | Notes |
| --- | --- | --- | --- | --- |
| [lang attribute on inline content](https://specification.website/spec/i18n/lang-attribute/) | required | ✅ |  |  |
| [International URL structure](https://specification.website/spec/i18n/international-url-structure/) | recommended | — |  | Single-language Dutch site |
| [hreflang for language and regional URLs](https://specification.website/spec/i18n/hreflang/) | recommended | — |  | No locale alternates exist |
| [Localised page metadata](https://specification.website/spec/i18n/localised-metadata/) | recommended | ✅ |  |  |
| [Language switcher](https://specification.website/spec/i18n/language-switcher/) | recommended | — |  | Single-locale site |
| [RTL and bidirectional text](https://specification.website/spec/i18n/rtl-support/) | recommended | — |  | No RTL locales served |
| [Locale-aware content](https://specification.website/spec/i18n/locale-content/) | recommended | ✅ |  |  |
| [Plural rules and grammatical number](https://specification.website/spec/i18n/plural-rules/) | recommended | — |  | No dynamic counts or pluralised strings |
| [hreflang in XML sitemaps](https://specification.website/spec/i18n/sitemap-hreflang/) | optional | — |  | No sitemap and no alternates |
| [translate attribute for untranslatable content](https://specification.website/spec/i18n/translate-attribute/) | optional | ⚠️ |  | No `translate="no"` on Haags output; browser auto-translate would mangle it |
| [Writing modes and CJK line breaking](https://specification.website/spec/i18n/writing-modes/) | optional | — |  | No CJK/vertical-script content |
| [Internationalised Domain Names (IDN)](https://specification.website/spec/i18n/idn-support/) | optional | — |  | ASCII-only domain |
| [Avoid automatic IP-based language redirects](https://specification.website/spec/i18n/avoid-auto-geo-redirects/) | avoid | ✅ |  |  |
