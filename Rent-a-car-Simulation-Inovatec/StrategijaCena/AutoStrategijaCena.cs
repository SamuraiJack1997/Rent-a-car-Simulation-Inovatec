using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.PriceStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.StrategijaCena
{
    public class AutoStrategijaCena : IStrategijaCena
    {
        public double IzracunajCenu(Vozilo vozilo, Kupac kupac, int BrojDana)
        {
            double pocetnaCena = 200;
            double konacnaCena = pocetnaCena;

            switch (vozilo.markaAuta)
            {
                case MarkaAuta.Mercedes:
                    if (vozilo.Kilometraza < 50000)
                        konacnaCena += konacnaCena * 0.06;
                    if (vozilo.tipAuta.Equals(TipAuta.Limuzina))
                        konacnaCena += konacnaCena * 0.07;
                    if (vozilo.tipAuta.Equals(TipAuta.Hecbek) && vozilo.Kilometraza > 100000)
                        konacnaCena -= konacnaCena * 0.03;
                    break;
                case MarkaAuta.BMW:
                    if (vozilo.Potrosnja < 7)
                        konacnaCena += konacnaCena * 0.15;
                    else if (vozilo.Potrosnja > 7)
                        konacnaCena -= konacnaCena * 0.10;
                    else konacnaCena -= konacnaCena * 0.15;
                    break;
                case MarkaAuta.Peugeot:
                    if (vozilo.tipAuta.Equals(TipAuta.Limuzina))
                        konacnaCena += konacnaCena * 0.15;
                    else if (vozilo.tipAuta.Equals(TipAuta.Karavan))
                        konacnaCena += konacnaCena * 0.20;
                    else konacnaCena -= konacnaCena * 0.05;
                    break;
            }

            //TODO URACUNATI OPREMU

            if (kupac.Clanarina.Equals(Bonus.VIP))
                pocetnaCena -= pocetnaCena * 0.20;
            else if (kupac.Clanarina.Equals(Bonus.Basic))
                pocetnaCena -= pocetnaCena * 0.10;

            return konacnaCena;
        }
    }
}
