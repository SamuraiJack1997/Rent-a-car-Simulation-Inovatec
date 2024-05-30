using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class Rezervacije
    {
        public int VoziloId {  get; set; }
        public int KupacId {  get; set; }
        public DateTime PocetakRezervacije { get; set; }
        public DateTime KrajRezervacije { get; set; }
        
    }
}
