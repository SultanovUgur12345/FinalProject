/**
 * admin-delete-modal.js
 * Generic delete-modal helper for admin panel pages.
 *
 * Usage — call initDeleteModal(options) with page-specific selectors/callbacks:
 *
 *   initDeleteModal({
 *     modalId: 'deleteModal',
 *     overlayId: 'deleteModalOverlay',
 *     closeId: 'deleteModalClose',
 *     cancelId: 'cancelDelete',
 *     triggerClass: 'open-delete-modal',
 *     onOpen: function(button) { ... }   // populate modal fields here
 *   });
 */
function initDeleteModal(options) {
    var modal   = document.getElementById(options.modalId);
    var overlay = document.getElementById(options.overlayId);
    var closeBtn = document.getElementById(options.closeId);
    var cancelBtn = document.getElementById(options.cancelId);
    var activeClass = options.activeClass || 'show';

    if (!modal) return;

    function openModal(button) {
        if (options.onOpen) options.onOpen(button);
        modal.classList.add(activeClass);
        document.body.style.overflow = 'hidden';
    }

    function closeModal() {
        modal.classList.remove(activeClass);
        document.body.style.overflow = '';
    }

    document.addEventListener('click', function (e) {
        var btn = e.target.closest('.' + options.triggerClass);
        if (btn) openModal(btn);
    });

    if (closeBtn)  closeBtn.addEventListener('click', closeModal);
    if (cancelBtn) cancelBtn.addEventListener('click', closeModal);
    if (overlay)   overlay.addEventListener('click', closeModal);

    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') closeModal();
    });
}
