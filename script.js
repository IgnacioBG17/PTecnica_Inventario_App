const API_URL = "https://localhost:7132/api"; 

async function cargarProductos() {
    
    const res = await fetch(`${API_URL}/Products`);
    const data = await res.json();
    mostrarProductos(data.result);
}

async function cargarCategorias() {
    
    const res = await fetch(`${API_URL}/Categories`);
    const categorias = await res.json();
    const select = document.getElementById("categoria");
    select.innerHTML = categorias.result.map(c => `<option value="${c.id}">${c.name}</option>`).join("");
}

function mostrarProductos(productos) {
    
    const tbody = document.querySelector("#tablaProductos tbody");
    tbody.innerHTML = productos.map(p => `
        <tr>
          <td>${p.id}</td>
          <td>${p.code}</td>
          <td>${p.description}</td>
          <td>${p.price}</td>
          <td>${p.categoryName}</td>
          <td>
            <button class='btn btn-sm btn-warning' onclick='editarProducto(${JSON.stringify(p)})'>✏️</button>
            <button class='btn btn-sm btn-danger' onclick='eliminarProducto(${p.id})'>🗑️</button>
          </td>
        </tr>
      `).join("");
}

async function guardarProducto(event) {
    
    event.preventDefault();
    const producto = {
        id: document.getElementById("productoId").value || 0,
        Description: document.getElementById("nombre").value,
        Code: document.getElementById("codigo").value,
        Price: parseFloat(document.getElementById("precio").value),
        CategoryId: parseInt(document.getElementById("categoria").value)
    };

    const metodo = producto.id ? "PUT" : "POST";
    const url = `${API_URL}/Products${producto.id ? "/" + producto.id : ""}`;

    await fetch(url, {
        method: metodo,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(producto)
    });

    limpiarFormulario();
    cargarProductos();
}

function editarProducto(p) {
    document.getElementById("productoId").value = p.id;
    document.getElementById("codigo").value = p.code;
    document.getElementById("nombre").value = p.description;
    document.getElementById("precio").value = p.price;
    document.getElementById("categoria").value = p.categoryid;
}

async function eliminarProducto(id) {
    if (confirm("¿Eliminar producto?")) {
        await fetch(`${API_URL}/Products/${id}`, { method: "DELETE" });
        cargarProductos();
        limpiarFormulario();
    }
}

function limpiarFormulario() {
    document.getElementById("productoId").value = "";
    document.getElementById("nombre").value = "";
    document.getElementById("codigo").value = "";
    document.getElementById("precio").value = "";
    document.getElementById("categoria").value = "";
}

function filtrarProductos() {
    const nombre = document.getElementById("filtroNombre").value.toLowerCase();
    const codigo = document.getElementById("filtroCodigo").value.toLowerCase();

    fetch(`${API_URL}/Products`)
        .then(r => r.json())
        .then(data => {
            const filtrados = data.result.filter(p =>
                (!nombre || p.description?.toLowerCase().includes(nombre)) &&
                (!codigo || p.code?.toLowerCase().includes(codigo))
            );
            mostrarProductos(filtrados);
            limpiarFormulario();
        });
}


cargarCategorias();
cargarProductos();