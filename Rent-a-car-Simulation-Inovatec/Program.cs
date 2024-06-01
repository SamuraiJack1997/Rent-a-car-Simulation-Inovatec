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

            CSVManager csvManager = CSVManager.GetInstance();
            OutputManager outputManager = new OutputManager();

            List<Vozilo> vozila = csvManager.loadVozila();
            List<Kupac> kupci = csvManager.loadKupci();
            List<Oprema> oprema = csvManager.loadOprema();
            List<Rezervacija> rezervacije = csvManager.loadRezervacija();
            vozila = csvManager.loadVozilaOprema(vozila, oprema);
            List<ZahtevRezervacija> zahtevi_za_rezervacije = csvManager.loadZahteviRezervacije();

            outputManager.izlistajVozila(vozila);
            outputManager.izlistajKupce(kupci);
            outputManager.izlistajOpremu(oprema);
            outputManager.izlistajZahteveZaRezervaciju(zahtevi_za_rezervacije, kupci, vozila);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Neuspesne rezervacije:");
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();

            simulacijaIznajmljivanja(vozila, kupci, oprema, rezervacije, zahtevi_za_rezervacije);

            List<Rezervacija> nove_rezervacije = csvManager.loadNoveRezervacije();
            outputManager.izlistajNoveRezervacije(nove_rezervacije, kupci, vozila);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Kraj simulacije.");
            Console.WriteLine("-------------------------------------------------");
            Console.ResetColor();

            Console.WriteLine("Press any key to close this window.");
            Console.ReadKey();
        }

        static void simulacijaIznajmljivanja(List<Vozilo> vozila, List<Kupac> kupci, List<Oprema> oprema, List<Rezervacija> rezervacije, List<ZahtevRezervacija> zahtevi_za_rezervacije)
        {
            //Putanja do nove_rezervacije.csv -> \Rent-a-car-Simulation-Inovatec\bin\Debug\net8.0\CSV
            inicijalizujCSVFajl();

            var zahteviGrupisaniPoDatumuDolaska = zahtevi_za_rezervacije.GroupBy(x => x.DatumDolaska).OrderBy(y => y.Key);

            foreach (var grupa in zahteviGrupisaniPoDatumuDolaska)
            {
                var sortiraniZahtevi = grupa.OrderBy(x =>
                {
                    var kupac = kupci.First(y => y.Id == x.KupacID);
                    return kupac.clanarina == Clanarina.VIP ? 1 : kupac.clanarina == Clanarina.Basic ? 2 : 3;
                }).ToList();

                foreach (var zahtev in sortiraniZahtevi)
                {
                    Vozilo vozilo = vozila.FirstOrDefault(v => v.Id == zahtev.VoziloID);
                    Kupac kupac = kupci.FirstOrDefault(k => k.Id == zahtev.KupacID);

                    if (vozilo != null && kupac != null)
                    {
                        rezervisiVozilo(vozilo, kupac, zahtev.PocetakRezervacije, zahtev.PocetakRezervacije.AddDays(zahtev.BrojDana), rezervacije);
                    }
                    else
                    {
                        Console.WriteLine($"Neuspesno iznajmljivanje:\n-Kupac: {kupac?.Ime} {kupac?.Prezime}\n-Vozilo: {vozilo?.tipVozila}[{vozilo?.marka} {vozilo?.Model}] Tip[{vozilo?.tip}]\n-Razlog: Kupac ili vozilo nisu pronadjeni\n");
                    }
                }
            }
        }

        static void rezervisiVozilo(Vozilo vozilo, Kupac kupac, DateOnly pocetakRezervacije, DateOnly krajRezervacije, List<Rezervacija> rezervacije)
        {
            CSVManager csvManager = CSVManager.GetInstance();

            bool voziloDostupno = !rezervacije.Any(r => r.VoziloId == vozilo.Id &&
                                                  ((pocetakRezervacije <= r.KrajRezervacije && pocetakRezervacije >= r.PocetakRezervacije) ||
                                                  (krajRezervacije <= r.KrajRezervacije && krajRezervacije >= r.PocetakRezervacije) ||
                                                  (pocetakRezervacije <= r.PocetakRezervacije && krajRezervacije >= r.KrajRezervacije)));

            if (voziloDostupno)
            {
                int brojDana = (int)(DateTime.Parse(krajRezervacije.ToString()) - DateTime.Parse(pocetakRezervacije.ToString())).TotalDays;

                if (kupac.Budzet >= vozilo.IzracunajCenu(kupac, brojDana))
                {
                    double cena = vozilo.IzracunajCenu(kupac, brojDana);
                    kupac.Budzet -= cena;

                    csvManager.sacuvajNoveRezervacije(new Rezervacija
                    {
                        VoziloId = vozilo.Id,
                        KupacId = kupac.Id,
                        PocetakRezervacije = pocetakRezervacije,
                        KrajRezervacije = krajRezervacije
                    });
                }
                else
                {
                    Console.WriteLine($"Neuspesno iznajmljivanje:\n-Kupac: {kupac.Ime} {kupac.Prezime}\n-Vozilo: {vozilo.tipVozila}[{vozilo.marka} {vozilo.Model}] Tip[{vozilo.tip}]\n-Razlog: Nedovoljno sredstava u budzetu kupca\n");
                }
            }
            else
            {
                Console.WriteLine($"Neuspesno iznajmljivanje:\n-Kupac: {kupac.Ime} {kupac.Prezime}\n-Vozilo: {vozilo.tipVozila}[{vozilo.marka} {vozilo.Model}] Tip[{vozilo.tip}]\n-Razlog: Vozilo nije dostupno\n");
            }
        }

        static void inicijalizujCSVFajl()
        {
            CSVManager csvManager = CSVManager.GetInstance();
            string filePath = csvManager.nove_rezervacijeCSV;
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.WriteLine("VoziloId,KupacId,PocetakRezervacije,KrajRezervacije");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greska prilikom inicijalizacije CSV fajla: {ex.Message}");
            }
        }
    }
}

