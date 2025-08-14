const cacheName = "DefaultCompany-GhostRace-0.1.0";
const contentToCache = [
    "Build/0f7bc57e1610b726776b1f1df4222f26.loader.js",
    "Build/e4b99207156f2b0a7dfbc33ca590fd7f.framework.js.unityweb",
    "Build/7e526e65a076cca89fa017eb2997d23a.data.unityweb",
    "Build/9a2f4a5086397d6dd7161fcefccb9f7b.wasm.unityweb",
    "TemplateData/style.css"

];

self.addEventListener('install', function (e) {
    console.log('[Service Worker] Install');
    
    e.waitUntil((async function () {
      const cache = await caches.open(cacheName);
      console.log('[Service Worker] Caching all: app shell and content');
      await cache.addAll(contentToCache);
    })());
});

self.addEventListener('fetch', function (e) {
    e.respondWith((async function () {
      let response = await caches.match(e.request);
      console.log(`[Service Worker] Fetching resource: ${e.request.url}`);
      if (response) { return response; }

      response = await fetch(e.request);
      const cache = await caches.open(cacheName);
      console.log(`[Service Worker] Caching new resource: ${e.request.url}`);
      cache.put(e.request, response.clone());
      return response;
    })());
});
