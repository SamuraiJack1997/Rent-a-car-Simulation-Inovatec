using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent_a_car_Simulation_Inovatec.Interfaces;
using static Rent_a_car_Simulation_Inovatec.Program;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class Kupci
    {
        
        public IReadCSV<Kupac> IKupciCSV { get; set; }
        public List<Kupac> listaKupaca { get; set; }

        public Kupci(String filepath) {
            Kupac kupac = new Kupac();
            IKupciCSV = kupac;
            try
            {
                listaKupaca = IKupciCSV.ReadCSV<Kupac>(filepath);
            }
            catch 
            {
                Console.WriteLine("Neuspesno procitani Kupci iz CSV fajla.");
            }
            izlistajKupce();
        }

        public void izlistajKupce()
        {
            try
            {
                Console.WriteLine("Lista kupaca:");
                foreach (var kupac in listaKupaca)
                    Console.WriteLine(
                        "ID:" + kupac.Id + ", " +
                        "Ime:" + kupac.Ime + ", " +
                        "Prezime:" + kupac.Prezime + ", " +
                        "Budzet:" + kupac.Budzet + ", " +
                        "Clanarina:" + kupac.Clanarina
                        );
            }
            catch
            {
                Console.WriteLine("Neuspesno procitani Kupci iz CSV fajla.");
            }
        }
    }
}
