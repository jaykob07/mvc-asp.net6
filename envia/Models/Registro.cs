using System;
namespace envia.Models
{
	public class Registro
	{
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cargo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}

