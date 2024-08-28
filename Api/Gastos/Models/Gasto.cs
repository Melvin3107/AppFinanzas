using System;
using System.ComponentModel.DataAnnotations;

namespace Gastos.Models
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
