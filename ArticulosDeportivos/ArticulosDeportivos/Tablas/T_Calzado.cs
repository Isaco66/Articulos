using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace ArticulosDeportivos.Tablas
{
    public class T_Calzado
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(255)]
        public string marca { get; set; }
        [MaxLength(255)]
        public string modelo { get; set; }
        [MaxLength(255)]
        public string color { get; set; }
        [MaxLength(255)]
        public string codigo { get; set; }
        [MaxLength(255)]
        public double precio { get; set; }

    }
}
