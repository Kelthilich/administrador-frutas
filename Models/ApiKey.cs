using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace frutas.Models
{
    /// <summary>
    /// Entidad para manejar las API Keys de autenticaci�n
    /// Permite acceso seguro a los endpoints de la API
    /// </summary>
    [Table("ApiKeys")]
    public class ApiKey : BaseEntity
    {
        /// <summary>
        /// Valor �nico de la API Key
        /// </summary>
        [Required]
        [StringLength(255)]
        public string KeyValue { get; set; }

        /// <summary>
        /// ID del usuario propietario de la API Key
        /// </summary>
        [Required]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nombre descriptivo de la API Key
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre de la API Key")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripci�n del prop�sito de la API Key
        /// </summary>
        [StringLength(500)]
        [Display(Name = "Descripci�n")]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha de expiraci�n de la API Key
        /// </summary>
        [Display(Name = "Fecha de Expiraci�n")]
        [DataType(DataType.Date)]
        public DateTime? FechaExpiracion { get; set; }

        /// <summary>
        /// Fecha del �ltimo uso de la API Key
        /// </summary>
        [Display(Name = "�ltimo Uso")]
        public DateTime? UltimoUso { get; set; }

        /// <summary>
        /// Contador de veces que se ha usado la API Key
        /// </summary>
        [Display(Name = "Usos Totales")]
        public int ContadorUsos { get; set; } = 0;

        /// <summary>
        /// L�mite de requests por hora (0 = sin l�mite)
        /// </summary>
        [Display(Name = "L�mite por Hora")]
        public int LimitePorHora { get; set; } = 1000;

        /// <summary>
        /// Permisos de la API Key (READ, WRITE, DELETE, ALL)
        /// </summary>
        [StringLength(50)]
        [Display(Name = "Permisos")]
        public string Permisos { get; set; } = "READ";

        /// <summary>
        /// IP addresses permitidas (separadas por coma, vac�o = todas)
        /// </summary>
        [StringLength(500)]
        [Display(Name = "IPs Permitidas")]
        public string IPsPermitidas { get; set; }

        /// <summary>
        /// Referencia al usuario propietario
        /// </summary>
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        /// <summary>
        /// Verifica si la API Key es v�lida
        /// </summary>
        /// <returns>True si es v�lida</returns>
        public bool EsValida()
        {
            if (!Activo) return false;
            if (FechaExpiracion.HasValue && FechaExpiracion.Value <= DateTime.Now) return false;
            return true;
        }

        /// <summary>
        /// Verifica si la IP est� permitida para esta API Key
        /// </summary>
        /// <param name="ip">IP a verificar</param>
        /// <returns>True si est� permitida</returns>
        public bool IPEstaPermitida(string ip)
        {
            if (string.IsNullOrEmpty(IPsPermitidas)) return true;
            return IPsPermitidas.Contains(ip);
        }

        /// <summary>
        /// Verifica si el usuario tiene el permiso especificado
        /// </summary>
        /// <param name="permiso">Permiso a verificar</param>
        /// <returns>True si tiene el permiso</returns>
        public bool TienePermiso(string permiso)
        {
            if (Permisos == "ALL") return true;
            return Permisos.Contains(permiso);
        }

        /// <summary>
        /// Registra el uso de la API Key
        /// </summary>
        public void RegistrarUso()
        {
            UltimoUso = DateTime.Now;
            ContadorUsos++;
        }

        /// <summary>
        /// Genera una nueva API Key aleatoria
        /// </summary>
        /// <returns>String con la nueva API Key</returns>
        public static string GenerarNuevaKey()
        {
            return Guid.NewGuid().ToString("N") + DateTime.Now.Ticks.ToString("X");
        }
    }
}