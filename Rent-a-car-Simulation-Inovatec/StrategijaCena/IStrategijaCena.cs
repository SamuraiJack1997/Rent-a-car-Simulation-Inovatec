using Rent_a_car_Simulation_Inovatec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.PriceStrategy
{
    public interface IStrategijaCena
    {
        double IzracunajCenu(Vozilo vozilo, Kupac kupac, int BrojDana);
    }
}
