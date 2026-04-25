// feature-index.js
document.addEventListener("DOMContentLoaded", function () {
    initDeleteModal({
        modalId: 'deleteModal',
        overlayId: 'deleteModalOverlay',
        closeId: 'deleteModalClose',
        cancelId: 'cancelDelete',
        triggerClass: 'open-delete-modal',
        activeClass: 'show',
        onOpen: function (btn) {
            document.getElementById('deleteFeatureId').value = btn.getAttribute('data-id') || '';
            document.getElementById('deleteFeatureTitle').textContent = btn.getAttribute('data-title') || '';
            document.getElementById('deleteFeatureImage').src = btn.getAttribute('data-image') || '';
        }
    });
});
