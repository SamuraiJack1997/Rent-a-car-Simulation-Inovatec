using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class ZahteviRezervacije
    {
        public int VoziloID { get; set; }
        public int KupacID { get; set; }
        public DateTime DatumDolaska { get; set; }
        public DateTime PocetakRezervacije { get; set; }
        public int BrojDana { get; set; }
    }
}
