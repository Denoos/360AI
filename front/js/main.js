// Функция для подключения HTML-фрагментов
document.addEventListener("DOMContentLoaded", () => {
    const includes = document.querySelectorAll("[data-include]");
    includes.forEach(el => {
      const file = el.getAttribute("data-include");
      fetch(file)
        .then(response => {
          if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
          }
          return response.text();
        })
        .then(data => {
          // Вставляем полученный HTML внутрь элемента
          el.innerHTML = data;
        })
        .catch(err => {
          console.warn(`Не удалось загрузить: ${file}`, err);
          el.innerHTML = `<p class="text-red-500">Ошибка загрузки: ${file}</p>`;
        });
    });
  });