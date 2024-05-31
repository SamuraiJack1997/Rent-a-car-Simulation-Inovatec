using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent_a_car_Simulation_Inovatec.Models;

namespace Rent_a_car_Simulation_Inovatec.Interfaces
{
    public interface ICSVManager
    {
        public List<Kupac> loadKupci();
        public List<Oprema> loadOprema();
        public List<Rezervacija> loadRezervacija();
        public List<Vozilo> loadVozila();
        public List<Vozilo> loadVozilaOprema(List<Vozilo> vozila,List<Oprema> oprema);
        public List<ZahtevRezervacija> loadZahteviRezervacije();
        public void sacuvajNoveRezervacije(Rezervacija noveRezervacije);
    }
}
