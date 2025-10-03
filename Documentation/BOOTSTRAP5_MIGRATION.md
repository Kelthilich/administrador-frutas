# ?? Guía de Migración a Bootstrap 5.3.8 y jQuery 3.7.1

## ? Cambios Realizados Automáticamente

### 1. **Site.Master**
- ? Bootstrap CSS actualizado: `4.6.0` ? `5.3.8`
- ? jQuery actualizado: `3.6.0` ? `3.7.1`
- ? Bootstrap JS actualizado: `4.6.0` ? `5.3.8`
- ? Font Awesome actualizado: `5.15.4` ? `6.5.1`

### 2. **Global.asax.cs**
- ? jQuery CDN actualizado a `3.7.1`

### 3. **README.md**
- ? Documentación actualizada con versiones correctas

---

## ?? Cambios Importantes de Bootstrap 5

### **Cambios de Atributos Data:**
```html
<!-- Bootstrap 4 (Antiguo) -->
data-toggle="collapse"
data-toggle="dropdown"
data-toggle="modal"
data-dismiss="alert"

<!-- Bootstrap 5 (Nuevo) -->
data-bs-toggle="collapse"
data-bs-toggle="dropdown"
data-bs-toggle="modal"
data-bs-dismiss="alert"
```

### **Cambios de Clases CSS:**
```css
/* Bootstrap 4 (Antiguo) */
.mr-auto    /* Margin right auto */
.ml-2       /* Margin left 2 */
.pr-3       /* Padding right 3 */
.text-right /* Align text right */

/* Bootstrap 5 (Nuevo) */
.me-auto    /* Margin end auto */
.ms-2       /* Margin start 2 */
.pe-3       /* Padding end 3 */
.text-end   /* Align text end */
```

### **Cambios en Botones de Cierre:**
```html
<!-- Bootstrap 4 (Antiguo) -->
<button type="button" class="close" data-dismiss="alert">
    <span>&times;</span>
</button>

<!-- Bootstrap 5 (Nuevo) -->
<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
```

### **Cambios en Dropdowns:**
```html
<!-- Bootstrap 4 (Antiguo) -->
<div class="dropdown-menu">
    <a class="dropdown-item">Item</a>
</div>

<!-- Bootstrap 5 (Nuevo) -->
<ul class="dropdown-menu">
    <li><a class="dropdown-item">Item</a></li>
</ul>
```

---

## ?? Archivos que Necesitan Revisión Manual

Revisa estos archivos y actualiza según sea necesario:

### **1. Account/Login.aspx**
- Buscar: `data-toggle`, `data-dismiss`
- Reemplazar: `data-bs-toggle`, `data-bs-dismiss`
- Buscar: `.text-right`, `.mr-`, `.ml-`
- Reemplazar: `.text-end`, `.me-`, `.ms-`

### **2. Account/Register.aspx**
- Mismo proceso que Login.aspx

### **3. Frutas/ListaFrutas.aspx**
- Actualizar clases de utilidades
- Revisar dropdowns y modals

### **4. Frutas/AgregarFruta.aspx**
- Actualizar formularios
- Revisar validaciones

### **5. Frutas/EditarFruta.aspx**
- Mismo proceso que AgregarFruta.aspx

### **6. Admin/Usuarios.aspx**
- Actualizar tablas y cards
- Revisar modals

### **7. Admin/Logs.aspx**
- Actualizar filtros
- Revisar paginación

---

## ?? Comando de Búsqueda y Reemplazo

### **Para Visual Studio:**

1. **Abrir Find and Replace** (`Ctrl+Shift+H`)

2. **Buscar y reemplazar estos patrones:**

| Buscar | Reemplazar |
|--------|-----------|
| `data-toggle="` | `data-bs-toggle="` |
| `data-dismiss="` | `data-bs-dismiss="` |
| `data-target="` | `data-bs-target="` |
| `.mr-auto` | `.me-auto` |
| `.ml-auto` | `.ms-auto` |
| `.text-right` | `.text-end` |
| `.text-left` | `.text-start` |
| `dropdown-menu-right` | `dropdown-menu-end` |
| `class="close"` | `class="btn-close"` |

---

## ?? Testing Checklist

Después de actualizar, verifica estas funcionalidades:

- [ ] **Navegación:** Dropdowns funcionan correctamente
- [ ] **Alertas:** Se pueden cerrar con el botón X
- [ ] **Modals:** Se abren y cierran correctamente
- [ ] **Forms:** Validaciones funcionan
- [ ] **Botones:** Todos los estilos se ven bien
- [ ] **Tablas:** Responsive funcionando
- [ ] **Cards:** Layout correcto
- [ ] **Tooltips:** Si los hay, verificar que funcionen

---

## ?? Notas Adicionales

### **jQuery ya no es requerido por Bootstrap 5:**
Bootstrap 5 usa JavaScript vanilla, pero como ASP.NET Web Forms necesita jQuery para algunas validaciones, lo mantenemos.

### **Compatibilidad:**
- ? IE 11: Ya no es soportado por Bootstrap 5
- ? Navegadores modernos: Completamente soportados
- ? Mobile: Mejorado en Bootstrap 5

### **Performance:**
Bootstrap 5 es más ligero y rápido que Bootstrap 4:
- Bootstrap 4: ~143 KB
- Bootstrap 5: ~120 KB (17% más pequeño)

---

## ?? Próximos Pasos

1. **Revisar cada página** manualmente
2. **Ejecutar la aplicación** y probar cada funcionalidad
3. **Verificar responsive** en diferentes tamaños de pantalla
4. **Commit de cambios** cuando todo funcione

---

## ?? Referencias

- [Bootstrap 5 Migration Guide](https://getbootstrap.com/docs/5.3/migration/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.3/)
- [jQuery 3.7 Release Notes](https://blog.jquery.com/2023/05/11/jquery-3-7-0-released/)

---

## ? Resumen de Versiones Actualizadas

| Librería | Versión Anterior | Versión Nueva | Estado |
|----------|-----------------|---------------|---------|
| Bootstrap | 4.6.0 | 5.3.8 | ? Actualizado |
| jQuery | 3.6.0 | 3.7.1 | ? Actualizado |
| Font Awesome | 5.15.4 | 6.5.1 | ? Actualizado |

**¡Felicidades! Tu proyecto ahora usa las últimas versiones estables.** ??