using System.ComponentModel.DataAnnotations.Schema;

namespace PREGUNTA_01_EINER_CHAVEZ.Models
{
    public class Videoteca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        [Column("veces_vista")]
        public int? VecesVista { get; set; }
        public decimal? Rating { get; set; }
        public DateTime? Fecha { get; set; }
    }

}
