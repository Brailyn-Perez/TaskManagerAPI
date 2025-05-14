# 📋 API de Manejo de Tareas en .NET 8

Esta es una API RESTful construida con **.NET 8** para gestionar tareas básicas (To-Do list). Permite crear, obtener, actualizar y eliminar tareas de forma sencilla.

## 🚀 Propósito

El propósito de esta API es proporcionar una estructura básica para gestionar tareas, ideal para proyectos de práctica o como base para aplicaciones más complejas.

## ⚙️ Configuración del Proyecto

1. **Clonar el repositorio**

   ```bash
   git clone https://github.com/tuusuario/TareasApi.git
   cd TareasApi
   ```

2. **Ejecutar el proyecto**

   ```bash
   dotnet run
   ```

3. **Acceder a Swagger**
   Navega a:

   ```
   https://localhost:<puerto>/swagger
   ```

## 🧪 Cómo Probar la API

Puedes probar los endpoints usando **Swagger**, **Postman** o cualquier cliente REST.

### Endpoints disponibles

* `GET /api/tasks` - Lista todas las tareas
* `GET /api/tasks/{id}` - Obtiene una tarea por ID
* `POST /api/tasks` - Crea una nueva tarea

  ```json
  {
     "description": "string",
     "status": "Pending",
     "dueDate": "2025-05-14T01:05:24.042Z",
     "aditionalData": "string"
  }

  ```
* `PUT /api/tasks/{id}` - Actualiza una tarea existente
* `DELETE /api/tasks/{id}` - Elimina una tarea

