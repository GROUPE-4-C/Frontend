// Fonction pour faire défiler la conversation vers le bas
function scrollToBottom(element) {
    if (element) {
        element.scrollTop = element.scrollHeight;
    }
}

// Fonction pour vérifier si l'utilisateur est au bas de la conversation
function isScrolledToBottom(element) {
    if (element) {
        return Math.abs(element.scrollHeight - element.clientHeight - element.scrollTop) < 1;
    }
    return false;
}

// Fonction pour jouer un son de notification
function playMessageSound() {
    const audio = new Audio('/sounds/message.mp3');
    audio.play().catch(err => console.log('Unable to play notification sound'));
}

// Fonction pour demander la permission des notifications
async function requestNotificationPermission() {
    if ('Notification' in window) {
        const permission = await Notification.requestPermission();
        return permission === 'granted';
    }
    return false;
}

// Fonction pour afficher une notification de bureau
function showNotification(title, body) {
    if ('Notification' in window && Notification.permission === 'granted') {
        new Notification(title, {
            body: body,
            icon: '/images/logo.png'
        });
    }
}

// Fonction pour gérer le focus de la fenêtre
let windowFocused = true;
window.addEventListener('focus', () => windowFocused = true);
window.addEventListener('blur', () => windowFocused = false);

// Fonction pour gérer les notifications de nouveaux messages
function handleNewMessage(senderName, message) {
    if (!windowFocused) {
        playMessageSound();
        showNotification(senderName, message);
    }
}
