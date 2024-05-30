using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.Interfaces;
using Rent_a_car_Simulation_Inovatec.PriceStrategy;

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
        Sport,
        None
    }
    public enum Clanarina
    {
        VIP,
        Basic,
        None
    }
    public class Program : IReadCSV<Vozilo>
    {

        public static List<Vozilo> UcitajVozilaIzCsv(string filePath)
        {
            var vozila = new List<Vozilo>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');

                var vozilo = new Vozilo
                {
                    Id = int.Parse(values[0]),
                    tipVozila = Enum.TryParse<TipVozila>(values[1], out var tipVozila) ? tipVozila : throw new Exception($"Invalid TipVozila: {values[1]}"),
                    marka = Enum.TryParse<Marka>(values[2], out var marka) ? marka : throw new Exception($"Invalid Marka: {values[2]}"),
                    Model = values[3],
                    Potrosnja = string.IsNullOrEmpty(values[4])? 0.0 : double.Parse(values[4]),
                    Kubikaza = string.IsNullOrEmpty(values[5]) ? 0 : int.Parse(values[5]),
                    Kilometraza = string.IsNullOrEmpty(values[6]) ? 0 : int.Parse(values[6]),
                    Snaga = string.IsNullOrEmpty(values[7]) ? 0 : int.Parse(values[7]),
                    tip = string.IsNullOrEmpty(values[8]) ? Tip.None : Enum.TryParse<Tip>(values[8], out var tip) ? tip : throw new Exception($"Invalid Tip: {values[8]}")
                };

                vozila.Add(vozilo);
            }

            return vozila;
        }

        static void Main(string[] args)
        {
            Kupac k = new Kupac();
            k.Id = 1;
            k.clanarina = Clanarina.VIP;
            k.Budzet = 500;
            k.Ime = "Stefan";
            k.Prezime = "Aleksandric";

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozila.csv");
            var vozila = UcitajVozilaIzCsv(filePath);
            foreach (var vozilo in vozila)
            {
                Console.WriteLine(
                    $"ID:{vozilo.Id}" +
                    $",TipVozila:{vozilo.tipVozila}" +
                    $",Marka:{vozilo.marka}," +
                    $"Model:{vozilo.Model}," +
                    $"Potrosnja:{vozilo.Potrosnja}L/100KM," +
                    $"Kubikaza:{vozilo.Kubikaza}," +
                    $"Kilometraza:{vozilo.Kilometraza}," +
                    $"Snaga:{vozilo.Snaga}," +
                    $"Tip:{vozilo.tip}" +
                    $"Cena:{vozilo.IzracunajCenu(k, 10)}"
                    );
            }

            //List<Vozilo> vozila = ucitajVozila("CSV/vozila.csv");
            //List<Kupac> kupci = ucitajKupce("CSV/kupci.csv");
            //List<Rezervacija> rezervacije = ucitajRezervacije("CSV/rezervacije.csv");
            //List<Oprema> oprema = ucitajOpremu("CSV/oprema.csv");
        }
    }
}

