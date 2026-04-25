// ship-slider-index.js
document.addEventListener("DOMContentLoaded", function () {
    var deleteModal = document.getElementById("deleteModal");
    var deleteModalOverlay = document.getElementById("deleteModalOverlay");
    var deleteModalClose = document.getElementById("deleteModalClose");
    var cancelDelete = document.getElementById("cancelDelete");

    function openDeleteModal(button) {
        document.getElementById("deleteSliderId").value = button.getAttribute("data-id") || "";
        document.getElementById("deleteSliderTitle").textContent = button.getAttribute("data-title") || "";
        document.getElementById("deleteSliderPreTitle").textContent = button.getAttribute("data-pretitle") || "";
        var img = button.getAttribute("data-image") || "";
        var imgEl = document.getElementById("deleteSliderImage");
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
