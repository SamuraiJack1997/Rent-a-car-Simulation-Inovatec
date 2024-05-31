using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class ZahtevRezervacija
    {
        public int VoziloID { get; set; }
        public int KupacID { get; set; }
        public DateOnly DatumDolaska { get; set; }
        public DateOnly PocetakRezervacije { get; set; }
        public int BrojDana { get; set; }
    }
}
