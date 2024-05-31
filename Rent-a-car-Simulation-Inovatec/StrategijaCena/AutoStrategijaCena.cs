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

            switch (vozilo.marka)
            {
                case Marka.Mercedes:
                    if (vozilo.Kilometraza < 50000)
                        konacnaCena += pocetnaCena * 0.06;
                    if (vozilo.tip.Equals(Tip.Limuzina))
                        konacnaCena += pocetnaCena * 0.07;
                    else if (vozilo.tip.Equals(Tip.Hecbek) && vozilo.Kilometraza > 100000)
                        konacnaCena -= pocetnaCena * 0.03;
                    break;
                case Marka.BMW:
                    if (vozilo.Potrosnja < 7)
                        konacnaCena += pocetnaCena * 0.15;
                    else if (vozilo.Potrosnja > 7)
                        konacnaCena -= pocetnaCena * 0.10;
                    else konacnaCena -= pocetnaCena * 0.15;
                    break;
                case Marka.Peugeot:
                    if (vozilo.tip.Equals(Tip.Limuzina))
                        konacnaCena += pocetnaCena * 0.15;
                    else if (vozilo.tip.Equals(Tip.Karavan))
                        konacnaCena += pocetnaCena * 0.20;
                    else konacnaCena -= pocetnaCena * 0.05;
                    break;
            }

            if(vozilo.oprema!=null)
                foreach (Oprema o in vozilo.oprema)
                    if (!(o is null))
                        if (o.PovecavaCenu.Equals(1))
                            konacnaCena += o.Cena;

            if(kupac!=null)
            if (kupac.clanarina.Equals(Clanarina.VIP))
                konacnaCena -= pocetnaCena * 0.20;
            else if (kupac.clanarina.Equals(Clanarina.Basic))
                konacnaCena -= pocetnaCena * 0.10;

            return konacnaCena*BrojDana;
        }
    }
}
