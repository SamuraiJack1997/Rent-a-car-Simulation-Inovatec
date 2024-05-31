using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class Rezervacija
    {
        public int VoziloId {  get; set; }
        public int KupacId {  get; set; }
        public DateOnly PocetakRezervacije { get; set; }
        public DateOnly KrajRezervacije { get; set; }

        public override string? ToString()
        {
            return $"VoziloID: {VoziloId}, KupacID:{KupacId}, Pocetak Rezervacije:{PocetakRezervacije}, Kraj Rezervacije:{KrajRezervacije}";
        }
    }
}
