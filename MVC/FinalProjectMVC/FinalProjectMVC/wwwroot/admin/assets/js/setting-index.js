function enableEdit(id) {
        document.getElementById('display-' + id).style.display = 'none';
        document.getElementById('input-' + id).style.display = '';
        document.getElementById('input-' + id).focus();
        document.getElementById('editBtn-' + id).style.display = 'none';
        document.getElementById('saveBtn-' + id).style.display = '';
        document.getElementById('cancelBtn-' + id).style.display = '';
    }

    function cancelEdit(id, original) {
        document.getElementById('input-' + id).value = original;
        document.getElementById('display-' + id).style.display = '';
        document.getElementById('input-' + id).style.display = 'none';
        document.getElementById('editBtn-' + id).style.display = '';
        document.getElementById('saveBtn-' + id).style.display = 'none';
        document.getElementById('cancelBtn-' + id).style.display = 'none';
    }

    async function saveEdit(id) {
        const value = document.getElementById('input-' + id).value.trim();
        if (!value) return;
        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        const res = await fetch(`/Admin/Setting/InlineEdit/${id}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json', 'RequestVerificationToken': token ?? '' },
            body: JSON.stringify({ value })
        });
        if (res.ok) {
            document.getElementById('display-' + id).textContent = value;
            cancelEdit(id, value);
        } else {
            alert('Yadda saxlamaq mumkun olmadi.');
        }
    }