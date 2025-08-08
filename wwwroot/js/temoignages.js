// Fonction pour confirmer la suppression d'un témoignage
function confirmDeleteTemoignage(message) {
    return new Promise((resolve) => {
        if (confirm(message)) {
            resolve(true);
        } else {
            resolve(false);
        }
    });
}

// Fonction pour fermer le menu déroulant lors d'un clic à l'extérieur
document.addEventListener('click', function(event) {
    if (!event.target.closest('.dropdown')) {
        const dropdowns = document.querySelectorAll('.dropdown-menu.show');
        dropdowns.forEach(dropdown => {
            dropdown.classList.remove('show');
        });
    }
});

// Fonction pour empêcher la propagation des clics dans le menu
document.addEventListener('click', function(event) {
    if (event.target.closest('.dropdown-menu')) {
        event.stopPropagation();
    }
});

// Fonction pour ouvrir le menu d'options
function toggleOptionsMenu(menuId) {
    const menu = document.querySelector(`.dropdown-menu[data-id="${menuId}"]`);
    if (menu) {
        const isVisible = menu.classList.contains('show');
        // Fermer tous les menus
        document.querySelectorAll('.dropdown-menu.show').forEach(m => {
            m.classList.remove('show');
        });
        // Basculer le menu actuel
        if (!isVisible) {
            menu.classList.add('show');
        }
    }
}
