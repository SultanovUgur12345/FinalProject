document.querySelectorAll('.open-delete-modal').forEach(btn => {
        btn.addEventListener('click', () => {
            document.getElementById('deleteVideoId').value = btn.dataset.id;
            document.getElementById('deleteVideoTitle').textContent = btn.dataset.title;
            document.getElementById('deleteModal').classList.add('active');
        });
    });
    document.getElementById('deleteModalClose').addEventListener('click', () => document.getElementById('deleteModal').classList.remove('active'));
    document.getElementById('deleteModalOverlay').addEventListener('click', () => document.getElementById('deleteModal').classList.remove('active'));
    document.getElementById('cancelDeleteBtn').addEventListener('click', () => document.getElementById('deleteModal').classList.remove('active'));