using System;
using System.ComponentModel.DataAnnotations;

namespace frutas.Models
{
    /// <summary>
    /// Clase base para todas las entidades del sistema
    /// Implementa propiedades comunes como auditor�a
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Identificador �nico de la entidad
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Fecha de creaci�n del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        /// <summary>
        /// Fecha de �ltima modificaci�n del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Indica si el registro est� activo (soft delete)
        /// </summary>
        public bool Activo { get; set; } = true;

        /// <summary>
        /// Usuario que cre� el registro
        /// </summary>
        public int? UsuarioCreacion { get; set; }

        /// <summary>
        /// Usuario que modific� el registro por �ltima vez
        /// </summary>
        public int? UsuarioModificacion { get; set; }
    }
}