document.addEventListener('click', function(event) {
    // Ferme tous les menus déroulants si on clique en dehors
    if (!event.target.closest('.dropdown')) {
        const dropdowns = document.querySelectorAll('.dropdown-menu');
        dropdowns.forEach(dropdown => {
            dropdown.classList.remove('show');
        });
    }
});

function toggleDropdown(id) {
    // Ferme tous les autres menus déroulants
    const dropdowns = document.querySelectorAll('.dropdown-menu');
    dropdowns.forEach(dropdown => {
        if (dropdown.getAttribute('data-id') !== id) {
            dropdown.classList.remove('show');
        }
    });

    // Bascule le menu actuel
    const currentDropdown = document.querySelector(`.dropdown-menu[data-id="${id}"]`);
    if (currentDropdown) {
        currentDropdown.classList.toggle('show');
    }
}

// Empêche la propagation des clics dans le menu
document.addEventListener('click', function(event) {
    if (event.target.closest('.dropdown-menu')) {
        event.stopPropagation();
    }
});
