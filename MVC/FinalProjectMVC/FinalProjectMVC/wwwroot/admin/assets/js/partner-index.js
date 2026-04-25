// partner-index.js
document.addEventListener("DOMContentLoaded", function () {
    initDeleteModal({
        modalId: 'deleteModal',
        overlayId: 'deleteModalOverlay',
        closeId: null,
        cancelId: 'cancelDeleteBtn',
        triggerClass: 'open-delete-modal',
        activeClass: 'active',
        onOpen: function (btn) {
            document.getElementById('modalPartnerImage').src = btn.getAttribute('data-image') || '';
            document.getElementById('deleteForm').action = '/Admin/Partner/Delete/' + (btn.getAttribute('data-id') || '');
        }
    });
});
