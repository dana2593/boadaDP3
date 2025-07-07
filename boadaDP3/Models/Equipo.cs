using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace boadaDP3.Models
{
    public class Equipo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Dispositivo { get; set; } = string.Empty;

        [NotNull]
        public string Marca { get; set; } = string.Empty;

        public bool GarantiaActiva { get; set; }

        public int VidaUtilMeses { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}