document.addEventListener("DOMContentLoaded", function () {

        // SEARCH
        const searchInput = document.getElementById("workerSearchInput");
        const tableBody = document.getElementById("workersTableBody");
        const apiBaseUrl = "@apiBaseUrl";

        let searchTimeout = null;

        searchInput.addEventListener("input", function () {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(async function () {
                const name = searchInput.value.trim();

                if (!name) {
                    // Bos olsa normal pagination-i goster
                    loadPage(currentPage);
                    return;
                }

                // Axtaris varsa pagination-i gizlet
                document.getElementById("paginationContainer").style.display = "none";

                try {
                    const response = await fetch(`/Admin/Workers/Search?name=${encodeURIComponent(name)}`);
                    if (!response.ok) return;
                    const workers = await response.json();

                    tableBody.innerHTML = "";

                    if (workers.length === 0) {
                        tableBody.innerHTML = `<tr id="noWorkersRow"><td colspan="6" class="admin-table__empty">No workers found</td></tr>`;
                        document.getElementById("totalWorkersCount").textContent = "0";
                        return;
                    }

                    document.getElementById("totalWorkersCount").textContent = workers.length;

                    workers.forEach(function (item) {
                        const imgSrc = item.image ? `${apiBaseUrl}${item.image}` : "";
                        tableBody.innerHTML += `
                            <tr class="worker-row">
                                <td>${item.id}</td>
                                <td><img class="admin-avatar" src="${imgSrc}" alt="${item.fullName}" /></td>
                                <td>${item.fullName}</td>
                                <td>${item.position ?? ""}</td>
                                <td>${item.experienceYears} year</td>
                                <td>
                                    <div class="admin-actions">
                                        <a href="/Admin/Workers/Detail/${item.id}" class="admin-btn admin-btn--sm">Get</a>
                                        <a href="/Admin/Workers/Edit/${item.id}" class="admin-btn admin-btn--sm">Edit</a>
                                        <button type="button"
                                                class="admin-btn admin-btn--sm admin-btn--danger open-delete-modal"
                                                data-id="${item.id}"
                                                data-name="${item.fullName}"
                                                data-position="${item.position ?? ""}"
                                                data-experience="${item.experienceYears}"
                                                data-image="${imgSrc}">
                                            Delete
                                        </button>
                                    </div>
                                </td>
                            </tr>`;
                    });

                    document.querySelectorAll(".open-delete-modal").forEach(function (btn) {
                        btn.addEventListener("click", function () { openDeleteModal(btn); });
                    });

                } catch (e) {
                    console.error("Search error:", e);
                }
            }, 350);
        });

        // DELETE MODAL
        const deleteModal = document.getElementById("deleteModal");
        const deleteModalOverlay = document.getElementById("deleteModalOverlay");
        const deleteModalClose = document.getElementById("deleteModalClose");
        const cancelDelete = document.getElementById("cancelDelete");

        const deleteWorkerId = document.getElementById("deleteWorkerId");
        const deleteWorkerName = document.getElementById("deleteWorkerName");
        const deleteWorkerPosition = document.getElementById("deleteWorkerPosition");
        const deleteWorkerExperience = document.getElementById("deleteWorkerExperience");
        const deleteWorkerImage = document.getElementById("deleteWorkerImage");

        function openDeleteModal(button) {
            const id = button.getAttribute("data-id") || "";
            const name = button.getAttribute("data-name") || "";
            const position = button.getAttribute("data-position") || "";
            const experience = button.getAttribute("data-experience") || "";
            const image = button.getAttribute("data-image") || "";

            deleteWorkerId.value = id;
            deleteWorkerName.textContent = name;
            deleteWorkerPosition.textContent = position;
            deleteWorkerExperience.textContent = experience;
            deleteWorkerImage.src = image;
            deleteWorkerImage.style.display = image ? "block" : "none";

            deleteModal.classList.add("show");
            document.body.style.overflow = "hidden";
        }

        function closeDeleteModal() {
            deleteModal.classList.remove("show");
            document.body.style.overflow = "";
        }

        document.addEventListener("click", function (e) {
            const deleteBtn = e.target.closest(".open-delete-modal");
            if (deleteBtn) {
                openDeleteModal(deleteBtn);
            }
        });

        if (deleteModalClose) deleteModalClose.addEventListener("click", closeDeleteModal);
        if (cancelDelete) cancelDelete.addEventListener("click", closeDeleteModal);
        if (deleteModalOverlay) deleteModalOverlay.addEventListener("click", closeDeleteModal);

        // PAGINATION (AJAX - refreshsiz)
        const pageSize = @Model.PageSize;
        let currentPage = @Model.Page;

        async function loadPage(page) {
            try {
                const response = await fetch(`/Admin/Workers/GetPage?page=${page}&pageSize=${pageSize}`);
                if (!response.ok) return;
                const result = await response.json();

                // Table
                tableBody.innerHTML = "";
                if (!result.items || result.items.length === 0) {
                    tableBody.innerHTML = `<tr id="noWorkersRow"><td colspan="6" class="admin-table__empty">No workers found</td></tr>`;
                    document.getElementById("totalWorkersCount").textContent = "0";
                    document.getElementById("paginationContainer").style.display = "none";
                    return;
                }

                document.getElementById("totalWorkersCount").textContent = result.totalCount;
                result.items.forEach(function (item) {
                    const imgSrc = item.image ? `${apiBaseUrl}${item.image}` : "";
                    tableBody.innerHTML += `
                        <tr class="worker-row">
                            <td>${item.id}</td>
                            <td><img class="admin-avatar" src="${imgSrc}" alt="${item.fullName}" /></td>
                            <td>${item.fullName}</td>
                            <td>${item.position ?? ""}</td>
                            <td>${item.experienceYears} year</td>
                            <td>
                                <div class="admin-actions">
                                    <a href="/Admin/Workers/Detail/${item.id}" class="admin-btn admin-btn--sm">Get</a>
                                    <a href="/Admin/Workers/Edit/${item.id}" class="admin-btn admin-btn--sm">Edit</a>
                                    <button type="button"
                                            class="admin-btn admin-btn--sm admin-btn--danger open-delete-modal"
                                            data-id="${item.id}"
                                            data-name="${item.fullName}"
                                            data-position="${item.position ?? ""}"
                                            data-experience="${item.experienceYears}"
                                            data-image="${imgSrc}">
                                        Delete
                                    </button>
                                </div>
                            </td>
                        </tr>`;
                });

                // Pagination controls
                currentPage = result.page;
                const totalPages = result.totalPages;   

                document.getElementById("prevPageBtn").disabled = currentPage <= 1;
                document.getElementById("prevPageBtn").dataset.page = currentPage - 1;
                document.getElementById("nextPageBtn").disabled = currentPage >= totalPages;
                document.getElementById("nextPageBtn").dataset.page = currentPage + 1;

                const paginationNumbers = document.getElementById("paginationNumbers");
                paginationNumbers.innerHTML = "";
                for (let i = 1; i <= totalPages; i++) {
                    const btn = document.createElement("button");
                    btn.className = "admin-pagination__number" + (i === currentPage ? " active" : "");
                    btn.textContent = i;
                    btn.dataset.page = i;
                    btn.addEventListener("click", () => loadPage(i));
                    paginationNumbers.appendChild(btn);
                }

                document.getElementById("paginationContainer").style.display = totalPages > 1 ? "flex" : "none";

            } catch (e) {
                console.error("Pagination error:", e);
            }
        }

        document.getElementById("prevPageBtn").addEventListener("click", function () {
            if (!this.disabled) loadPage(parseInt(this.dataset.page));
        });
        document.getElementById("nextPageBtn").addEventListener("click", function () {
            if (!this.disabled) loadPage(parseInt(this.dataset.page));
        });
        document.querySelectorAll("#paginationNumbers .admin-pagination__number").forEach(function (btn) {
            btn.addEventListener("click", function () { loadPage(parseInt(this.dataset.page)); });
        });
    });