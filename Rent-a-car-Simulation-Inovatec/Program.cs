using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.Interfaces;
using Rent_a_car_Simulation_Inovatec.PriceStrategy;
using Rent_a_car_Simulation_Inovatec.VehicleBuilder;

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
            Kupac k = new Kupac();
            k.Id = 1;
            k.clanarina = Clanarina.VIP;
            k.Budzet = 500;
            k.Ime = "Stefan";
            k.Prezime = "Aleksandric";

            CSVManager csvManager = new CSVManager();

            List<Vozilo> list = csvManager.loadVozila();

            csvManager.izlistajVozila(list);
            
        }
    }
}

