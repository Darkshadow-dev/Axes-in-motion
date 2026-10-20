const healthButton = document.querySelector("#health-button");
const healthLabel = document.querySelector("#health-label");
const healthIndicator = document.querySelector("#health-indicator");
const menuToggle = document.querySelector("#menu-toggle");
const mainNavigation = document.querySelector("#main-navigation");
const pageStorageKey = "axes-in-motion-page";
const pages = Array.from(document.querySelectorAll(".page[id]"));
const pagesById = new Map(pages.map(page => [page.id, page]));
const pageLinks = Array.from(document.querySelectorAll("[data-page-link]"));
let activePageId = "";

function setMenuOpen(open) {
  menuToggle?.setAttribute("aria-expanded", String(open));
  mainNavigation?.classList.toggle("open", open);
}

function getSavedPage() {
  try {
    return localStorage.getItem(pageStorageKey) || "";
  } catch {
    return "";
  }
}

function savePage(id) {
  try {
    localStorage.setItem(pageStorageKey, id);
  } catch {
    // Page navigation still works when browser storage is unavailable.
  }
}

function setActivePage(id, { scroll = true, persist = true, focus = false } = {}) {
  const pageId = pagesById.has(id) ? id : "home";
  const page = pagesById.get(pageId);
  if (!page) return "";

  for (const candidate of pages) {
    const isActive = candidate === page;
    candidate.hidden = !isActive;
    candidate.classList.toggle("active", isActive);
    candidate.setAttribute("aria-hidden", String(!isActive));
    candidate.tabIndex = -1;
  }

  for (const link of pageLinks) {
    if (link.getAttribute("href") === `#${pageId}`) {
      link.setAttribute("aria-current", "page");
    } else {
      link.removeAttribute("aria-current");
    }
  }

  activePageId = pageId;
  document.title = `${page.dataset.pageTitle || pageId} — Axes in Motion`;
  if (persist) savePage(pageId);

  if (scroll) window.scrollTo({ top: 0, left: 0, behavior: "instant" });
  if (focus) page.focus({ preventScroll: true });
  return pageId;
}

function pageIdFromHash() {
  try {
    return decodeURIComponent(window.location.hash.slice(1));
  } catch {
    return "";
  }
}

function navigateToPage(id, { replace = false, scroll = true, persist = true, focus = false } = {}) {
  const pageId = pagesById.has(id) ? id : "home";
  const nextHash = `#${encodeURIComponent(pageId)}`;

  if (window.location.hash !== nextHash) {
    if (replace) history.replaceState(null, "", nextHash);
    else history.pushState(null, "", nextHash);
  }

  setActivePage(pageId, { scroll, persist, focus });
}

document.addEventListener("click", event => {
  const link = event.target instanceof Element ? event.target.closest('a[href^="#"]') : null;
  if (!link) return;

  const pageId = pageIdFromLink(link);
  if (!pageId || !pagesById.has(pageId)) return;

  event.preventDefault();
  navigateToPage(pageId, { focus: true });
  setMenuOpen(false);
});

function pageIdFromLink(link) {
  try {
    return decodeURIComponent(new URL(link.href).hash.slice(1));
  } catch {
    return "";
  }
}

function syncPageFromHistory() {
  const requestedPage = pageIdFromHash();
  const pageId = pagesById.has(requestedPage) ? requestedPage : "home";
  if (pageId !== requestedPage) {
    navigateToPage(pageId, { replace: true });
    return;
  }
  if (pageId !== activePageId) setActivePage(pageId, { focus: true });
  setMenuOpen(false);
}

window.addEventListener("popstate", syncPageFromHistory);
window.addEventListener("hashchange", syncPageFromHistory);

menuToggle?.addEventListener("click", () => {
  const isOpen = menuToggle.getAttribute("aria-expanded") === "true";
  setMenuOpen(!isOpen);
});

document.addEventListener("keydown", event => {
  if (event.key === "Escape") setMenuOpen(false);
});

function initializePageNavigation() {
  const requestedPage = pageIdFromHash();
  const savedPage = getSavedPage();
  const initialPage = pagesById.has(requestedPage)
    ? requestedPage
    : pagesById.has(savedPage)
      ? savedPage
      : "home";

  const pageId = setActivePage(initialPage, { scroll: true, persist: true });
  if (requestedPage !== pageId) history.replaceState(null, "", `#${pageId}`);
}

if (document.readyState === "loading") {
  document.addEventListener("DOMContentLoaded", initializePageNavigation, { once: true });
} else {
  initializePageNavigation();
}

healthButton?.addEventListener("click", async () => {
  healthButton.disabled = true;
  healthLabel.textContent = "Checking…";
  healthIndicator.className = "health-indicator";

  try {
    const response = await fetch("/health", { headers: { Accept: "application/json" } });
    const result = await response.json();
    if (!response.ok || result.status !== "ok") {
      healthLabel.textContent = result.status === "database_not_ready" ? "Database schema is missing" : "SQL Server unavailable";
      healthIndicator.classList.add("offline");
      return;
    }
    healthLabel.textContent = "SQL + API connected";
    healthIndicator.classList.add("online");
  } catch {
    healthLabel.textContent = "API server is offline";
    healthIndicator.classList.add("offline");
  } finally {
    healthButton.disabled = false;
  }
});

document.querySelector("#copy-query")?.addEventListener("click", async event => {
  const button = event.currentTarget;
  const query = document.querySelector("#sql-query")?.textContent?.trim();
  if (!query) return;

  try {
    await navigator.clipboard.writeText(query);
    button.textContent = "COPIED";
  } catch {
    button.textContent = "SELECT QUERY";
  }
  window.setTimeout(() => { button.textContent = "COPY"; }, 1600);
});

const yearLabel = document.querySelector("#year");
if (yearLabel) yearLabel.textContent = new Date().getFullYear();
