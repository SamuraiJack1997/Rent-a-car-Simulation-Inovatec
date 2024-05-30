using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class Vozilo
    {
        public int Id { get; set; }
        public TipVozila tipVozila { get; set; }
        public MarkaAuta markaAuta { get; set; }
        public MarkaMotora markaMotora { get; set; }
        public string Model { get; set; }
        public double Potrosnja { get; set; }
        public int Kubikaza { get; set; }
        public int Kilometraza { get; set; }
        public int Snaga { get; set; }
        public TipAuta? tipAuta { get; set; }
        public TipMotora? tipMotora { get; set; }
        public List<Oprema>? oprema { get; set; }

        public double IzracunajCenu(DateTime PocetakRezervacije, int brojDana)
        {
            //TODO implementacija strategije
            return 0;
        }
    }
}
