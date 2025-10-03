# Sistema de Gestión de Frutas ??

Un sistema web completo para la gestión de inventario de frutas desarrollado en ASP.NET Web Forms con .NET Framework 4.8.

## ?? Características

### ?? Autenticación y Seguridad
- Sistema de login/registro con hash de contraseñas
- Control de roles (Usuario/Administrador)
- Bloqueo automático de cuentas por intentos fallidos
- Reseteo de contraseñas por administradores

### ?? Gestión de Frutas
- CRUD completo de frutas
- Filtros avanzados (categoría, país, temporada, etc.)
- Paginación y ordenamiento
- Validaciones del lado cliente y servidor
- Cálculo automático de valores de inventario

### ?? Gestión de Usuarios (Solo Administradores)
- Lista de todos los usuarios
- Activar/Desactivar cuentas
- Generar contraseñas temporales
- Estadísticas de usuarios

### ?? Sistema de Auditoría (Solo Administradores)
- Logs de todas las acciones del sistema
- Filtros por fecha, severidad, usuario y acción
- Paginación y búsqueda avanzada
- Códigos de color por severidad

## ??? Tecnologías Utilizadas

- **Frontend:** ASP.NET Web Forms, Bootstrap 4, Font Awesome, jQuery
- **Backend:** C# .NET Framework 4.8
- **Base de Datos:** SQL Server LocalDB
- **Arquitectura:** Patrón en capas (Models, DTOs, Repositories, Services)
- **Seguridad:** BCrypt para hash de contraseñas

## ?? Requisitos del Sistema

- Visual Studio 2019 o superior
- .NET Framework 4.8
- SQL Server LocalDB
- IIS Express

## ?? Instalación y Configuración

### 1. Clonar el repositorio
```bash
git clone https://github.com/Kelthilich/administrador-frutas.git
cd administrador-frutas
```

### 2. Configurar la Base de Datos
1. Abrir el proyecto en Visual Studio
2. Ejecutar el script SQL en `Database/CreateDatabase.sql`
3. Verificar la cadena de conexión en `Web.config`

### 3. Ejecutar el proyecto
1. Compilar la solución (Ctrl+Shift+B)
2. Ejecutar con F5 o Ctrl+F5

## ?? Usuarios de Prueba

### Administrador
- **Usuario:** `admin2`
- **Contraseña:** `Test123!`

### Usuario Normal
- **Usuario:** `asd2`
- **Contraseña:** `(tu contraseña de registro)`

## ?? Estructura del Proyecto

```
frutas/
??? Account/           # Páginas de autenticación
??? Admin/             # Páginas de administración
??? Frutas/            # Gestión de frutas
??? DTOs/              # Objetos de transferencia de datos
??? Models/            # Modelos de datos
??? Repositories/      # Acceso a datos
??? Security/          # Helpers de seguridad
??? Services/          # Lógica de negocio
??? Database/          # Scripts SQL
??? Content/           # Estilos CSS
```

## ?? Funcionalidades Principales

### Para Usuarios Normales:
- ? Gestión completa de frutas (crear, editar, eliminar, ver)
- ? Filtros y búsqueda avanzada
- ? Cambio de contraseña
- ? Dashboard con estadísticas personales

### Para Administradores:
- ? Todo lo anterior +
- ? Gestión de usuarios del sistema
- ? Resetear contraseñas de usuarios
- ? Activar/Desactivar cuentas
- ? Ver logs del sistema con filtros avanzados
- ? Estadísticas completas del sistema

## ?? Seguridad Implementada

- Hash de contraseñas con salt usando BCrypt
- Validación de entrada en cliente y servidor
- Control de sesiones y permisos por rol
- Logging de todas las acciones para auditoría
- Protección contra inyección SQL con parámetros
- Bloqueo temporal de cuentas por intentos fallidos

## ?? Próximas Mejoras

- [ ] API REST para integración móvil
- [ ] Dashboard con gráficos interactivos
- [ ] Sistema de notificaciones por email
- [ ] Subida de imágenes para frutas
- [ ] Reportes en PDF/Excel
- [ ] Búsqueda por código de barras

## ?? Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## ????? Autor

**Tu Nombre** - [Kelthilich](https://github.com/Kelthilich)

## ?? Soporte

Si tienes alguna pregunta o problema, por favor abre un [issue](https://github.com/Kelthilich/administrador-frutas/issues) en GitHub.

---

? **¡No olvides dar una estrella al proyecto si te resultó útil!** ?