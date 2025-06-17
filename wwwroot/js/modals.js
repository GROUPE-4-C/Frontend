// Gestion globale des modals
class ModalManager {
  constructor() {
    this.setupEventListeners();
    this.hideAllModalsOnLoad();
  }

  hideAllModalsOnLoad() {
    // S'assurer que tous les modals sont cachés au chargement
    document.addEventListener("DOMContentLoaded", () => {
      const modals = document.querySelectorAll(".create-modal-overlay");
      modals.forEach((modal) => {
        modal.classList.remove("show");
        modal.style.display = "none";
      });
    });
  }

  setupEventListeners() {
    // Fermer les modals en cliquant en dehors
    document.addEventListener("click", (event) => {
      if (event.target.classList.contains("create-modal-overlay")) {
        this.closeAllModals();
      }
    });

    // Fermer avec la touche Escape
    document.addEventListener("keydown", (event) => {
      if (event.key === "Escape") {
        this.closeAllModals();
      }
    });
  }

  openModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
      modal.style.display = "flex";
      // Force reflow pour que l'animation fonctionne
      modal.offsetHeight;
      modal.classList.add("show");
      document.body.style.overflow = "hidden";
      return true;
    }
    return false;
  }

  closeModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
      modal.classList.remove("show");
      // Attendre la fin de l'animation avant de cacher
      setTimeout(() => {
        modal.style.display = "none";
      }, 300);
      document.body.style.overflow = "auto";
    }
  }

  closeAllModals() {
    const modals = document.querySelectorAll(".create-modal-overlay");
    modals.forEach((modal) => {
      modal.classList.remove("show");
      setTimeout(() => {
        modal.style.display = "none";
      }, 300);
    });
    document.body.style.overflow = "auto";
  }
}

// Instance globale
const modalManager = new ModalManager();

// Fonctions globales
function openModal(modalId) {
  return modalManager.openModal(modalId);
}
function closeModal(modalId) {
  modalManager.closeModal(modalId);
}
