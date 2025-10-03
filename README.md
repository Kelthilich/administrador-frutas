# Sistema de Gesti�n de Frutas ??

Un sistema web completo para la gesti�n de inventario de frutas desarrollado en ASP.NET Web Forms con .NET Framework 4.8.

## ?? Caracter�sticas

### ?? Autenticaci�n y Seguridad
- Sistema de login/registro con hash de contrase�as
- Control de roles (Usuario/Administrador)
- Bloqueo autom�tico de cuentas por intentos fallidos
- Reseteo de contrase�as por administradores

### ?? Gesti�n de Frutas
- CRUD completo de frutas
- Filtros avanzados (categor�a, pa�s, temporada, etc.)
- Paginaci�n y ordenamiento
- Validaciones del lado cliente y servidor
- C�lculo autom�tico de valores de inventario

### ?? Gesti�n de Usuarios (Solo Administradores)
- Lista de todos los usuarios
- Activar/Desactivar cuentas
- Generar contrase�as temporales
- Estad�sticas de usuarios

### ?? Sistema de Auditor�a (Solo Administradores)
- Logs de todas las acciones del sistema
- Filtros por fecha, severidad, usuario y acci�n
- Paginaci�n y b�squeda avanzada
- C�digos de color por severidad

## ??? Tecnolog�as Utilizadas

- **Frontend:** ASP.NET Web Forms, Bootstrap 4, Font Awesome, jQuery
- **Backend:** C# .NET Framework 4.8
- **Base de Datos:** SQL Server LocalDB
- **Arquitectura:** Patr�n en capas (Models, DTOs, Repositories, Services)
- **Seguridad:** BCrypt para hash de contrase�as

## ?? Requisitos del Sistema

- Visual Studio 2019 o superior
- .NET Framework 4.8
- SQL Server LocalDB
- IIS Express

## ?? Instalaci�n y Configuraci�n

### 1. Clonar el repositorio
```bash
git clone https://github.com/Kelthilich/administrador-frutas.git
cd administrador-frutas
```

### 2. Configurar la Base de Datos
1. Abrir el proyecto en Visual Studio
2. Ejecutar el script SQL en `Database/CreateDatabase.sql`
3. Verificar la cadena de conexi�n en `Web.config`

### 3. Ejecutar el proyecto
1. Compilar la soluci�n (Ctrl+Shift+B)
2. Ejecutar con F5 o Ctrl+F5

## ?? Usuarios de Prueba

### Administrador
- **Usuario:** `admin2`
- **Contrase�a:** `Test123!`

### Usuario Normal
- **Usuario:** `asd2`
- **Contrase�a:** `(tu contrase�a de registro)`

## ?? Estructura del Proyecto

```
frutas/
??? Account/           # P�ginas de autenticaci�n
??? Admin/             # P�ginas de administraci�n
??? Frutas/            # Gesti�n de frutas
??? DTOs/              # Objetos de transferencia de datos
??? Models/            # Modelos de datos
??? Repositories/      # Acceso a datos
??? Security/          # Helpers de seguridad
??? Services/          # L�gica de negocio
??? Database/          # Scripts SQL
??? Content/           # Estilos CSS
```

## ?? Funcionalidades Principales

### Para Usuarios Normales:
- ? Gesti�n completa de frutas (crear, editar, eliminar, ver)
- ? Filtros y b�squeda avanzada
- ? Cambio de contrase�a
- ? Dashboard con estad�sticas personales

### Para Administradores:
- ? Todo lo anterior +
- ? Gesti�n de usuarios del sistema
- ? Resetear contrase�as de usuarios
- ? Activar/Desactivar cuentas
- ? Ver logs del sistema con filtros avanzados
- ? Estad�sticas completas del sistema

## ?? Seguridad Implementada

- Hash de contrase�as con salt usando BCrypt
- Validaci�n de entrada en cliente y servidor
- Control de sesiones y permisos por rol
- Logging de todas las acciones para auditor�a
- Protecci�n contra inyecci�n SQL con par�metros
- Bloqueo temporal de cuentas por intentos fallidos

## ?? Pr�ximas Mejoras

- [ ] API REST para integraci�n m�vil
- [ ] Dashboard con gr�ficos interactivos
- [ ] Sistema de notificaciones por email
- [ ] Subida de im�genes para frutas
- [ ] Reportes en PDF/Excel
- [ ] B�squeda por c�digo de barras

## ?? Contribuciones

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## ?? Licencia

Este proyecto est� bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para m�s detalles.

## ????? Autor

**Tu Nombre** - [Kelthilich](https://github.com/Kelthilich)

## ?? Soporte

Si tienes alguna pregunta o problema, por favor abre un [issue](https://github.com/Kelthilich/administrador-frutas/issues) en GitHub.

---

? **�No olvides dar una estrella al proyecto si te result� �til!** ?