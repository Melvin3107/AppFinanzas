using System;
using System.ComponentModel.DataAnnotations;

namespace Usuarios.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}
//Hola mundo