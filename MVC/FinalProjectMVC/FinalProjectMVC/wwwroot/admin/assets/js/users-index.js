document.addEventListener("DOMContentLoaded", function () {
        const tableBody = document.getElementById("usersTableBody");
        const paginationNumbers = document.getElementById("paginationNumbers");
        const prevPageBtn = document.getElementById("prevPageBtn");
        const nextPageBtn = document.getElementById("nextPageBtn");
        const paginationContainer = document.getElementById("paginationContainer");
        const isSuperAdmin = "@callerRole" === "SuperAdmin";

        const pageSize = @Model.PageSize;
        let currentPage = @Model.Page;

        const antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]')?.value ?? "";

        function buildRows(items, pageOffset) {
            if (items.length === 0) {
                tableBody.innerHTML = `<tr id="noUsersRow"><td colspan="6" class="admin-table__empty">No users found</td></tr>`;
                document.getElementById("totalUsersCount").textContent = "0";
                return;
            }

            tableBody.innerHTML = "";
            items.forEach(function (item, index) {
                const badgeClass = item.role ? `admin-badge--${item.role.toLowerCase()}` : "";

                let actionsTd = "";
                if (isSuperAdmin) {
                    if (item.canChangeRole) {
                        const adminSel = item.role === "Admin" ? "selected" : "";
                        const memberSel = item.role === "Member" ? "selected" : "";
                        actionsTd = `<td><div class="admin-actions">
                            <form action="/Admin/Users/AssignRole" method="post">
                                <input type="hidden" name="__RequestVerificationToken" value="${antiForgeryToken}" />
                                <input type="hidden" name="UserId" value="${item.id}" />
                                <input type="hidden" name="UserName" value="${item.userName ?? ""}" />
                                <select name="Role" class="admin-select" style="padding:5px 8px;font-size:12px;width:auto;" onchange="this.form.submit()">
                                    <option value="Admin" ${adminSel}>Admin</option>
                                    <option value="Member" ${memberSel}>Member</option>
                                </select>
                            </form>
                        </div></td>`;
                    } else {
                        actionsTd = `<td><span class="admin-text-muted">â€”</span></td>`;
                    }
                }

                tableBody.innerHTML += `
                    <tr class="user-row">
                        <td>${pageOffset + index + 1}</td>
                        <td>${item.fullName ?? ""}</td>
                        <td>${item.userName ?? ""}</td>
                        <td>${item.email ?? ""}</td>
                        <td><span class="admin-badge ${badgeClass}">${item.role ?? ""}</span></td>
                        ${actionsTd}
                    </tr>`;
            });
        }

        // PAGINATION
        async function loadPage(page) {
            try {
                const response = await fetch(`/Admin/Users/GetPage?page=${page}&pageSize=${pageSize}`);
                if (!response.ok) return;
                const result = await response.json();
                const items = result.items ?? [];

                buildRows(items, (page - 1) * pageSize);
                document.getElementById("totalUsersCount").textContent = result.totalCount ?? 0;

                currentPage = result.page;
                const totalPages = result.totalPages;

                prevPageBtn.disabled = currentPage <= 1;
                prevPageBtn.dataset.page = currentPage - 1;
                nextPageBtn.disabled = currentPage >= totalPages;
                nextPageBtn.dataset.page = currentPage + 1;

                paginationNumbers.innerHTML = "";
                for (let i = 1; i <= totalPages; i++) {
                    const btn = document.createElement("button");
                    btn.className = "admin-pagination__number" + (i === currentPage ? " active" : "");
                    btn.textContent = i;
                    btn.dataset.page = i;
                    btn.addEventListener("click", () => loadPage(i));
                    paginationNumbers.appendChild(btn);
                }

                paginationContainer.style.display = totalPages > 1 ? "flex" : "none";
            } catch (e) {
                console.error("Pagination error:", e);
            }
        }

        prevPageBtn?.addEventListener("click", function () {
            if (!this.disabled) loadPage(parseInt(this.dataset.page));
        });
        nextPageBtn?.addEventListener("click", function () {
            if (!this.disabled) loadPage(parseInt(this.dataset.page));
        });
        document.querySelectorAll("#paginationNumbers .admin-pagination__number").forEach(btn => {
            btn.addEventListener("click", function () { loadPage(parseInt(this.dataset.page)); });
        });

        // SEARCH
        const emailSearchInput = document.getElementById("emailSearchInput");
        let searchTimeout = null;
        emailSearchInput?.addEventListener("input", function () {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(async function () {
                const email = emailSearchInput.value.trim();

                if (!email) {
                    paginationContainer.style.display = "";
                    loadPage(currentPage);
                    return;
                }

                paginationContainer.style.display = "none";

                try {
                    const response = await fetch(`/Admin/Users/SearchJson?fullName=${encodeURIComponent(email)}`);
                    if (!response.ok) return;
                    const users = await response.json();

                    buildRows(users, 0);
                    document.getElementById("totalUsersCount").textContent = users.length;
                } catch (e) {
                    console.error("Search error:", e);
                }
            }, 350);
        });
    });