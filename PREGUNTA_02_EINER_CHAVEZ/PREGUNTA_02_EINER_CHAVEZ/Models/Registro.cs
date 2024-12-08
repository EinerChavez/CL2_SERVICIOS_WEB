using System.ComponentModel.DataAnnotations.Schema;

namespace PREGUNTA_02_EINER_CHAVEZ.Models
{

    [Table("Usuarios")]
    public class Registro
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }

}
