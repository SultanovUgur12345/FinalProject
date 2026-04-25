// about-us-index.js
document.addEventListener("DOMContentLoaded", function () {
    var deleteModal = document.getElementById("deleteModal");
    var deleteModalOverlay = document.getElementById("deleteModalOverlay");
    var deleteModalClose = document.getElementById("deleteModalClose");
    var cancelDelete = document.getElementById("cancelDelete");

    function openDeleteModal(button) {
        document.getElementById("deleteItemId").value = button.getAttribute("data-id");
        document.getElementById("deleteItemName").textContent = button.getAttribute("data-name") || "";
        document.getElementById("deleteItemSubtitle").textContent = button.getAttribute("data-subtitle") || "";
        var img = button.getAttribute("data-image") || "";
        var imgEl = document.getElementById("deleteItemImage");
        imgEl.src = img;
        imgEl.style.display = img ? "block" : "none";
        deleteModal.classList.add("show");
        document.body.style.overflow = "hidden";
    }

    function closeDeleteModal() {
        deleteModal.classList.remove("show");
        document.body.style.overflow = "";
    }

    document.addEventListener("click", function (e) {
        var btn = e.target.closest(".open-delete-modal");
        if (btn) openDeleteModal(btn);
    });

    if (deleteModalClose) deleteModalClose.addEventListener("click", closeDeleteModal);
    if (cancelDelete) cancelDelete.addEventListener("click", closeDeleteModal);
    if (deleteModalOverlay) deleteModalOverlay.addEventListener("click", closeDeleteModal);

    document.addEventListener("keydown", function (e) {
        if (e.key === "Escape") closeDeleteModal();
    });
});
