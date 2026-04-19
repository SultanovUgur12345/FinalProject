// Cart state
    let cart = {};
    
    // Get all menu items
    const menuItems = document.querySelectorAll('.menu-item');
    
    // Update menu item display (qty on button)
    function updateItemDisplay(itemElement) {
        const name = itemElement.getAttribute('data-name');
        const qtySpan = itemElement.querySelector('.qty-value');
        const qty = cart[name] || 0;
        qtySpan.textContent = qty;
    }
    
    // Update order summary panel
    function updateOrderSummary() {
        const orderContainer = document.getElementById('orderList');
        const totalSpan = document.getElementById('totalAmount');
        let total = 0;
        
        orderContainer.innerHTML = '';
        const items = Object.keys(cart);
        
        if (items.length === 0) {
            orderContainer.innerHTML = '<div class="empty-order">âœ¨ Select items from the menu</div>';
            totalSpan.textContent = 'â‚º0';
            return;
        }
        
        items.forEach(name => {
            const qty = cart[name];
            if (qty > 0) {
                let price = 0;
                menuItems.forEach(item => {
                    if (item.getAttribute('data-name') === name) {
                        price = parseInt(item.getAttribute('data-price'));
                    }
                });
                const itemTotal = qty * price;
                total += itemTotal;
                
                const orderDiv = document.createElement('div');
                orderDiv.className = 'order-item-card';
                orderDiv.innerHTML = `
                    <div class="order-item-info">
                        <div class="order-item-name">${name}</div>
                        <div class="order-item-meta">${qty} x â‚º${price}</div>
                    </div>
                    <div class="order-item-price">â‚º${itemTotal}</div>
                    <div class="order-item-remove" data-name="${name}">âœ•</div>
                `;
                orderContainer.appendChild(orderDiv);
            }
        });
        
        totalSpan.textContent = `â‚º${total}`;
        
        // Add remove event listeners
        document.querySelectorAll('.order-item-remove').forEach(btn => {
            btn.addEventListener('click', function(e) {
                const name = this.getAttribute('data-name');
                delete cart[name];
                menuItems.forEach(item => {
                    if (item.getAttribute('data-name') === name) {
                        const qtySpan = item.querySelector('.qty-value');
                        qtySpan.textContent = '0';
                    }
                });
                updateOrderSummary();
            });
        });
    }
    
    // Add event listeners to all plus/minus buttons
    menuItems.forEach(item => {
        const name = item.getAttribute('data-name');
        const minusBtn = item.querySelector('.minus');
        const plusBtn = item.querySelector('.plus');
        
        if (!cart[name]) cart[name] = 0;
        
        plusBtn.addEventListener('click', () => {
            cart[name] = (cart[name] || 0) + 1;
            updateItemDisplay(item);
            updateOrderSummary();
        });
        
        minusBtn.addEventListener('click', () => {
            if (cart[name] > 0) {
                cart[name] -= 1;
                updateItemDisplay(item);
                updateOrderSummary();
            }
        });
    });
    
    // Clear order button
    document.getElementById('clearOrder').addEventListener('click', () => {
        cart = {};
        menuItems.forEach(item => {
            const qtySpan = item.querySelector('.qty-value');
            qtySpan.textContent = '0';
        });
        updateOrderSummary();
    });
    
    // ========== KÄ°TAB SÆHÄ°FÆLÆRÄ°NÄ° Ã‡EVÄ°RMÆ ==========
    let currentPage = 1;
    const totalPages = 4;
    const pageNames = {
        1: "Mezeler (Starters)",
        2: "Ana Yemekler (Main Courses)",
        3: "Pide & Lahmacun",
        4: "TatlÄ±lar & Ä°Ã§ecekler"
    };
    
    function showPage(page) {
        for (let i = 1; i <= totalPages; i++) {
            const pageDiv = document.getElementById(`page${i}`);
            if (pageDiv) {
                pageDiv.classList.remove('active-page');
            }
        }
        const activePageDiv = document.getElementById(`page${page}`);
        if (activePageDiv) {
            activePageDiv.classList.add('active-page');
        }
        document.getElementById('pageIndicator').innerHTML = `Page ${page} of ${totalPages} â€¢ ${pageNames[page]}`;
        currentPage = page;
    }
    
    document.getElementById('prevPage').addEventListener('click', () => {
        let newPage = currentPage - 1;
        if (newPage < 1) newPage = totalPages;
        showPage(newPage);
    });
    
    document.getElementById('nextPage').addEventListener('click', () => {
        let newPage = currentPage + 1;
        if (newPage > totalPages) newPage = 1;
        showPage(newPage);
    });
    
    // Initial update
    updateOrderSummary();
