// Header interactions
(function () {
    const header = document.getElementById('qodef-page-header');
    const topArea = document.getElementById('qodef-top-area');

    const updateHeaderState = () => {
        if (!header) return;
        const isSticky = window.scrollY > 10;
        header.classList.toggle('qodef-header--sticky', isSticky);
        if (topArea) topArea.style.display = isSticky ? 'none' : '';
    };

    window.addEventListener('scroll', updateHeaderState, { passive: true });
    updateHeaderState();
})();

document.addEventListener('DOMContentLoaded', function () {
    const teamSection = document.getElementById('teamSection');
    const modalOverlay = document.getElementById('modalOverlay');

    const modalImage = document.getElementById('modalImage');
    const modalName = document.getElementById('modalName');
    const modalRole = document.getElementById('modalRole');
    const modalBio = document.getElementById('modalBio');
    const modalExperience = document.getElementById('modalExperience');
    const modalLanguages = document.getElementById('modalLanguages');
    const modalCertificates = document.getElementById('modalCertificates');

    function openModal(card) {
        if (!modalOverlay || !card) return;

        if (modalImage) {
            modalImage.src = card.getAttribute('data-image') || '';
            modalImage.alt = card.getAttribute('data-name') || 'Worker';
        }

        if (modalName) modalName.textContent = card.getAttribute('data-name') || '';
        if (modalRole) modalRole.textContent = card.getAttribute('data-position') || '';
        if (modalBio) modalBio.textContent = card.getAttribute('data-description') || '';
        if (modalExperience) modalExperience.textContent = card.getAttribute('data-experience') || '0';
        if (modalLanguages) modalLanguages.textContent = card.getAttribute('data-languages') || '-';
        if (modalCertificates) modalCertificates.textContent = card.getAttribute('data-certificates') || '-';

        modalOverlay.style.display = 'flex';
        document.body.style.overflow = 'hidden';
    }

    function closeModal() {
        if (!modalOverlay) return;
        modalOverlay.style.display = 'none';
        document.body.style.overflow = 'auto';
    }

    if (teamSection) {
        teamSection.addEventListener('click', function (event) {
            const socialLink = event.target.closest('.team-social a');
            if (socialLink) {
                event.preventDefault();
            }

            const card = event.target.closest('.team-card');
            if (card) {
                openModal(card);
            }
        });
    }

    if (modalOverlay) {
        modalOverlay.addEventListener('click', function (event) {
            if (event.target === modalOverlay) {
                closeModal();
            }
        });
    }

    window.closeModal = closeModal;
});
