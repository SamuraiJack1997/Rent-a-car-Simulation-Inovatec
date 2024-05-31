using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.Interfaces;
using Rent_a_car_Simulation_Inovatec.PriceStrategy;
using Rent_a_car_Simulation_Inovatec.VehicleBuilder;
using System.Runtime.InteropServices;

namespace Rent_a_car_Simulation_Inovatec
{
    public enum TipVozila
    {
        Automobil,
        Motor
    }
    public enum Marka
    {
        Mercedes,
        BMW,
        Peugeot,
        Yamaha,
        Harley
    }
    public enum Tip
    {
        Limuzina,
        Hecbek,
        Karavan,
        Kupe,
        Kabriolet,
        Minivan,
        SUV,
        Pickup,
        Adventure,
        Heritage,
        Tour,
        Roadster,
        UrbanMobility,
        Sport
    }
    public enum Clanarina
    {
        VIP,
        Basic,
        None
    }

    public class Program
    {
        static void Main(string[] args)
        {
            CSVManager csvManager = new CSVManager();
            List<Vozilo> vozila = csvManager.loadVozila();
            List<Kupac> kupci = csvManager.loadKupci();
            List<Oprema> oprema = csvManager.loadOprema();
            List<Rezervacija> rezervacije = csvManager.loadRezervacija();
            List<VoziloOprema> vozilo_oprema = csvManager.loadVozilaOprema();
            List<ZahtevRezervacija> zahtevi_za_rezervacije=csvManager.loadZahteviRezervacije();
            csvManager.izlistajVozila(vozila);
            csvManager.izlistajKupce(kupci);
            csvManager.izlistajOpremu(oprema);
            csvManager.izlistajRezervacije(rezervacije);
            csvManager.izlistajOpremuPoVozilu(vozilo_oprema);
            csvManager.izlistajZahteveZaRezervaciju(zahtevi_za_rezervacije);
        }
    }
}

