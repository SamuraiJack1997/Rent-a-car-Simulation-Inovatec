using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.PriceStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.StrategijaCena
{
    public class MotorStrategijaCena : IStrategijaCena
    {
        public double IzracunajCenu(Vozilo vozilo, Kupac kupac, int BrojDana)
        {
            double pocetnaCena = 100;
            double konacnaCena = pocetnaCena;

            switch (vozilo.markaMotora)
            {
                case MarkaMotora.Harley:
                    konacnaCena += konacnaCena * 0.15;
                    if (vozilo.Kubikaza > 1200)
                        konacnaCena += konacnaCena * 0.10;
                    else konacnaCena -= konacnaCena * 0.05;
                    break;
                case MarkaMotora.Yamaha:
                    konacnaCena += konacnaCena * 0.10;
                    if (vozilo.Snaga > 180)
                        konacnaCena += konacnaCena * 0.05;
                    else konacnaCena -= konacnaCena * 0.10;
                    if (vozilo.tipMotora.Equals(TipMotora.Heritage))
                        konacnaCena += 50;
                    else if (vozilo.tipMotora.Equals(TipMotora.Sport))
                        konacnaCena += 100;
                    else konacnaCena -= 10;
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
