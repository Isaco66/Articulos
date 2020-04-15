using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace ArticulosDeportivos.Tablas
{
    public class T_Ropa
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(255)]
        public string tipo { get; set; }
        [MaxLength(255)]
        public string talla { get; set; }
        [MaxLength(255)]
        public string color { get; set; }
        [MaxLength(255)]
        public string codigo { get; set; }
        [MaxLength(255)]
        public double precio { get; set; }

    }
}
