using Rent_a_car_Simulation_Inovatec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Interfaces
{
    public interface IOutputManager
    {
        public void izlistajVozila(List<Vozilo> vozila);
        public void izlistajKupce(List<Kupac> kupci);
        public void izlistajOpremu(List<Oprema> oprema);
        public void izlistajRezervacije(List<Rezervacija> rezervacije);
        public void izlistajZahteveZaRezervaciju(List<ZahtevRezervacija> zahteviRezervacija,List<Kupac> kupci,List<Vozilo> vozila);
        public void izlistajNoveRezervacije(List<Rezervacija> nove_rezervacije, List<Kupac> kupci, List<Vozilo> vozila);
    }
}
