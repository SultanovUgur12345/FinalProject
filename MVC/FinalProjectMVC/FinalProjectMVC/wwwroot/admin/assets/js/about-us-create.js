let achIndex = @Model.Achievements.Count;

    document.getElementById('addAchBtn').addEventListener('click', function () {
        const container = document.getElementById('achievementsContainer');
        const row = document.createElement('div');
        row.className = 'ach-row';
        row.dataset.index = achIndex;
        row.innerHTML = `
            <div class="ach-fields">
                <input name="Achievements[${achIndex}].Title" class="admin-input" placeholder="Achievement title..." />
                <input name="Achievements[${achIndex}].Description" class="admin-input" placeholder="Achievement description..." />
            </div>
            <button type="button" class="admin-btn admin-btn--sm admin-btn--danger remove-ach-btn">&#10005;</button>`;
        container.appendChild(row);
        achIndex++;
        bindRemove(row.querySelector('.remove-ach-btn'));
    });

    function bindRemove(btn) {
        btn.addEventListener('click', function () {
            btn.closest('.ach-row').remove();
            reindexAchievements();
        });
    }

    function reindexAchievements() {
        document.querySelectorAll('#achievementsContainer .ach-row').forEach(function (row, idx) {
            row.querySelectorAll('input').forEach(function (input) {
                input.name = input.name.replace(/\[\d+\]/, '[' + idx + ']');
            });
        });
        achIndex = document.querySelectorAll('#achievementsContainer .ach-row').length;
    }

    document.querySelectorAll('.remove-ach-btn').forEach(bindRemove);