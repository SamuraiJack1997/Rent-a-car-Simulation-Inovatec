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

            switch (vozilo.marka)
            {
                case Marka.Harley:
                    konacnaCena += pocetnaCena * 0.15;
                    if (vozilo.Kubikaza > 1200)
                        konacnaCena += pocetnaCena * 0.10;
                    else konacnaCena -= pocetnaCena * 0.05;
                    break;
                case Marka.Yamaha:
                    konacnaCena += pocetnaCena * 0.10;
                    if (vozilo.Snaga > 180)
                        konacnaCena += pocetnaCena * 0.05;
                    else konacnaCena -= pocetnaCena * 0.10;

                    if (vozilo.tip.Equals(Tip.Heritage))
                        konacnaCena += 50;
                    else if (vozilo.tip.Equals(Tip.Sport))
                        konacnaCena += 100;
                    else konacnaCena -= 10;
                    break;
            }

            if (vozilo.oprema != null)
                foreach (Oprema o in vozilo.oprema)
                if (!(o is null))
                    if (o.PovecavaCenu.Equals(1))
                        konacnaCena += o.Cena;

            if (kupac != null)
                if (kupac.clanarina.Equals(Clanarina.VIP))
                    konacnaCena -= pocetnaCena * 0.20;
                else if (kupac.clanarina.Equals(Clanarina.Basic))
                    konacnaCena -= pocetnaCena * 0.10;

            return konacnaCena*BrojDana;
        }
    }
}
