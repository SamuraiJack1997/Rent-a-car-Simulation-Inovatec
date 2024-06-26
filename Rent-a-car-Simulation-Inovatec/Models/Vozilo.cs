﻿using Rent_a_car_Simulation_Inovatec.PriceStrategy;
using Rent_a_car_Simulation_Inovatec.StrategijaCena;
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
        public Marka marka { get; set; }
        public string Model { get; set; }
        public double Potrosnja { get; set; }
        public int? Kubikaza { get; set; }
        public int? Kilometraza { get; set; }
        public int? Snaga { get; set; }
        public Tip tip { get; set; }
        public List<Oprema>? oprema { get; set; }

        public double IzracunajCenu(Kupac kupac, int brojDana)
        {
            IStrategijaCena strategijaCena = (tipVozila == TipVozila.Automobil) ? new AutoStrategijaCena() : new MotorStrategijaCena();
            return strategijaCena.IzracunajCenu(this, kupac, brojDana);
        }

        public override string? ToString()
        {
            string vozilo;
            vozilo = "[ID:" + Id + ", Tip vozila:" + tipVozila + ", Marka:" + marka + ", Model:" + Model + ", Potrosnja:" + Potrosnja + ", ";

            if (Kubikaza.Equals(null)) vozilo += "Kubikaza:None, "; else vozilo += " Kubikaza:" + Kubikaza + ", ";
            if (Kilometraza.Equals(null)) vozilo += "Kilometraza:None, "; else vozilo += "Kilometraza:" + Kilometraza + ", ";
            if (Snaga.Equals(null)) vozilo += "Snaga:None, "; else vozilo += "Snaga:" + Snaga + ", ";

            vozilo += "Tip:" + tip+"]";
            return vozilo;
        }
    }
}
