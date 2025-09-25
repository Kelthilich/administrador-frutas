using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frutas.Models
{
    /// <summary>
    /// Entidad que representa una fruta en el sistema
    /// Implementa auditoría completa y soft delete
    /// </summary>
    [Table("Frutas")]
    public class Fruta : BaseEntity
    {
        /// <summary>
        /// Nombre de la fruta
        /// </summary>
        [Required(ErrorMessage = "El nombre de la fruta es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción detallada de la fruta
        /// </summary>
        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Precio unitario de la fruta
        /// </summary>
        [Required(ErrorMessage = "El precio es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe estar entre 0.01 y 999,999.99")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        /// <summary>
        /// Cantidad disponible en stock
        /// </summary>
        [Required(ErrorMessage = "El stock es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        /// <summary>
        /// Categoría de la fruta (Ej: Cítrica, Tropical, etc.)
        /// </summary>
        [StringLength(50, ErrorMessage = "La categoría no puede exceder 50 caracteres")]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; }

        /// <summary>
        /// País de origen de la fruta
        /// </summary>
        [StringLength(50, ErrorMessage = "El país de origen no puede exceder 50 caracteres")]
        [Display(Name = "País de Origen")]
        public string PaisOrigen { get; set; }

        /// <summary>
        /// Temporada de la fruta (Ej: Primavera, Verano, etc.)
        /// </summary>
        [StringLength(30, ErrorMessage = "La temporada no puede exceder 30 caracteres")]
        [Display(Name = "Temporada")]
        public string Temporada { get; set; }

        /// <summary>
        /// Indica si la fruta es orgánica
        /// </summary>
        [Display(Name = "¿Es Orgánica?")]
        public bool EsOrganica { get; set; } = false;

        /// <summary>
        /// Fecha de vencimiento (si aplica)
        /// </summary>
        [Display(Name = "Fecha de Vencimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }

        /// <summary>
        /// Referencia al usuario que creó el registro
        /// </summary>
        [ForeignKey("UsuarioCreacion")]
        public virtual Usuario UsuarioCreador { get; set; }

        /// <summary>
        /// Referencia al usuario que modificó el registro
        /// </summary>
        [ForeignKey("UsuarioModificacion")]
        public virtual Usuario UsuarioModificador { get; set; }

        /// <summary>
        /// Método para obtener información resumida de la fruta
        /// </summary>
        /// <returns>String con información básica</returns>
        public string ObtenerResumen()
        {
            return $"{Nombre} - ${Precio:F2} (Stock: {Stock})";
        }

        /// <summary>
        /// Verifica si la fruta está disponible para venta
        /// </summary>
        /// <returns>True si está disponible</returns>
        public bool EstaDisponible()
        {
            return Activo && Stock > 0 && 
                   (FechaVencimiento == null || FechaVencimiento > DateTime.Now);
        }

        /// <summary>
        /// Calcula el valor total del stock
        /// </summary>
        /// <returns>Valor monetario total</returns>
        public decimal CalcularValorStock()
        {
            return Precio * Stock;
        }
    }
}