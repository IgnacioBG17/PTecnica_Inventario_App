# Prueba Técnica - Programador

Este proyecto consiste en una aplicación desarrollada como parte de una prueba técnica para la posición de Analista Programador. El sistema permite gestionar productos mediante una API REST y una aplicación cliente.

---

## 🧩 Tecnologías utilizadas

- **Backend:** ASP.NET Core Web API (.NET 6)
- **ORM:** Entity Framework Core
- **Base de datos:** SQL Server
- **Frontend:** HTML + Bootstrap 5 + Fetch API
- **Estilo:** Bootstrap 5
- **Cliente asincrónico:** `fetch`
- **Repositorio:** GitHub público

---

## 📁 Estructura del proyecto

```
InventarioApp/
│
├── InventarioApi/           # Proyecto API REST
│   ├── Controllers/         # Categorías y Productos
│   ├── Models/              # Entidades EF
│   └── Data/                # DbContext
│
├── InventarioCliente/       # Frontend (HTML, Bootstrap, JS)
│
└── README.md
```

---

## 🔌 Funcionalidades implementadas

- CRUD de Categorías
- CRUD de Productos
- Relación 1:N entre Categoría y Producto
- Filtros por nombre y código
- Operaciones asíncronas con Fetch API
- Estilos adaptados con Bootstrap 5
- Pruebas con Swagger
- Pruebas con el FrontEnd
---

## ▶️ Instrucciones para correr el proyecto

### Backend
1. Ingresar a la ruta
 ```bash
   cd inventarioApi
   ```
2. Restaurar paquetes:

   ```bash
   dotnet restore
   ```

3. Crear base de datos:

   ```bash
   dotnet ef database update
   ```

4. Ejecutar API:

   ```bash
   dotnet run
   ```

5. Acceder a Swagger:

   ```
   https://localhost:<puerto>/swagger
   ```

---

### Frontend

1. Abrir `InventarioCliente/index.html` en un navegador moderno.
2. Interactuar con la interfaz para gestionar productos.

---

## 🌐 Repositorio

Este proyecto fue subido a GitHub como parte de la prueba técnica.  
🔗 [https://github.com/tu-IgnacioBG17/](https://github.com/IgnacioBG17/)

---

## 📞 Contacto

Desarrollado por: **[Angel Bustamante]**  
Correo: [bustamanteangel532@gmail.com]  

---

## ✅ Resultado esperado

> Se entrega una solución funcional, bien estructurada, que cumple con todos los requerimientos técnicos solicitados.
