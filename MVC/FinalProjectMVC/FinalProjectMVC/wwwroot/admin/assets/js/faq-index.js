// faq-index.js
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".open-delete-modal").forEach(function (btn) {
        btn.addEventListener("click", function () {
            var id = this.dataset.id;
            var question = this.dataset.question;

            document.getElementById("modalFaqQuestion").textContent = question;
            document.getElementById("deleteForm").action = "/Admin/Faq/Delete/" + id;
            document.getElementById("deleteModal").classList.add("active");
        });
    });

    var cancelBtn = document.getElementById("cancelDeleteBtn");
    var overlay = document.getElementById("deleteModalOverlay");

    if (cancelBtn) {
        cancelBtn.addEventListener("click", function () {
            document.getElementById("deleteModal").classList.remove("active");
        });
    }

    if (overlay) {
        overlay.addEventListener("click", function () {
            document.getElementById("deleteModal").classList.remove("active");
        });
    }
});
