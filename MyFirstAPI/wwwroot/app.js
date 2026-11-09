const API = 'https://localhost:7241/api/Product';

async function loadProducts() {
    const response = await fetch(API);
    const products = await response.json();

    const list = document.getElementById('product-list');
    list.innerHTML = '';

    products.forEach(p => {
        list.innerHTML += `
            <li>
                ${p.name} - ${p.price}đ (còn ${p.stock})
                <button onclick="deleteProduct(${p.id})">Xóa</button>
            </li>
        `;
    });
}

async function deleteProduct(id) {
    await fetch(`${API}/${id}`, { method: 'DELETE' });
    loadProducts();
}

document.getElementById('create-form').addEventListener('submit', async (e) => {
    e.preventDefault();

    const product = {
        name: document.getElementById('name').value,
        description: document.getElementById('description').value,
        price: Number(document.getElementById('price').value),
        stock: Number(document.getElementById('stock').value)
    };

    await fetch(API, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(product)
    });

    loadProducts();
});

loadProducts();