using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.Interfaces;

namespace Rent_a_car_Simulation_Inovatec
{
    public class Program
    {
        public enum CsvFilePath
        {
            KupciCSV,
            OpremaCSV,
            RezervacijeCSV,
            VozilaCSV,
            VoziloOpremaCSV,
            ZahteviZaRezervacijeCSV,
            NoveRezervacijeCSV
        }

        static string GetFilePath(CsvFilePath filePath)
        {
            switch (filePath)
            {
                case CsvFilePath.KupciCSV:
                    return "CSV/kupci.csv";
                case CsvFilePath.OpremaCSV:
                    return "CSV/oprema.csv";
                case CsvFilePath.RezervacijeCSV:
                    return "CSV/rezervacije.csv";
                case CsvFilePath.VozilaCSV:
                    return "CSV/vozila.csv";
                case CsvFilePath.VoziloOpremaCSV:
                    return "CSV/vozilo_oprema.csv";
                case CsvFilePath.ZahteviZaRezervacijeCSV:
                    return "CSV/zahtevi_za_rezervacije.csv";
                case CsvFilePath.NoveRezervacijeCSV:
                    return "CSV/nove_rezervacije.csv";
                default:
                    throw new ArgumentException("Nevalidna vrednost putanja za enum.");
            }
        }
        static void Main(string[] args)
        {
            Kupci kupci = new Kupci(GetFilePath(CsvFilePath.KupciCSV));
        }
    }
}

