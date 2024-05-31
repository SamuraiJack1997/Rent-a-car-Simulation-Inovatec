using CsvHelper.Configuration;
using CsvHelper;
using Rent_a_car_Simulation_Inovatec.Interfaces;
using Rent_a_car_Simulation_Inovatec.Models;
using Rent_a_car_Simulation_Inovatec.VehicleBuilder;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec
{
    public class CSVManager : ICSVManager
    {
        private static CSVManager instance;
        private CSVManager() { }
        public static CSVManager GetInstance()
        {
            if (instance == null)
            {
                instance = new CSVManager();
            }
            return instance;
        }

        public string kupciCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\kupci.csv");
        public string opremaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\oprema.csv");
        public string rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\rezervacije.csv");
        public string vozilaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozila.csv");
        public string vozilo_opremaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozilo_oprema.csv");
        public string zahtevi_za_rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\zahtevi_za_rezervacije.csv");
        public string nove_rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\nove_rezervacije.csv");

        public List<Kupac> loadKupci()
        {
            var kupci = new List<Kupac>();

            using (var reader = new StreamReader(kupciCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var kupac = new Kupac();
                    kupac.Id = int.Parse(values[0]);
                    kupac.Ime = values[1];
                    kupac.Prezime = values[2];
                    kupac.Budzet = double.Parse(values[3]);
                    if (string.IsNullOrEmpty(values[4]))
                    {
                        kupac.clanarina = Clanarina.None;
                    }
                    else
                    {
                        kupac.clanarina = Enum.Parse<Clanarina>(values[4]);
                    }

                    kupci.Add(kupac);
                }
            }

            return kupci;
        }
        public List<Oprema> loadOprema()
        {
            var oprema = new List<Oprema>();

            using (var reader = new StreamReader(opremaCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var o = new Oprema();
                    o.Id = int.Parse(values[0]);
                    o.Naziv = values[1];
                    o.Cena = double.Parse(values[2]);
                    o.PovecavaCenu = values[3] == "1";


                    oprema.Add(o);
                }
            }

            return oprema;
        }
        public List<Rezervacija> loadRezervacija()
        {
            var rezervacije = new List<Rezervacija>();

            using (var reader = new StreamReader(rezervacijeCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var rezervacija = new Rezervacija();
                    rezervacija.VoziloId = int.Parse(values[0]);
                    rezervacija.KupacId = int.Parse(values[1]);
                    rezervacija.PocetakRezervacije = DateOnly.Parse(values[2]);
                    rezervacija.KrajRezervacije = DateOnly.Parse(values[3]);

                    rezervacije.Add(rezervacija);
                }
            }

            return rezervacije;
        }
        public List<Vozilo> loadVozila()
        {
            var vozila = new List<Vozilo>();

            using (var reader = new StreamReader(vozilaCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var builder = new VoziloBuilder()
                        .setId(int.Parse(values[0]))
                        .setTipVozila(Enum.Parse<TipVozila>(values[1]))
                        .setMarka(Enum.Parse<Marka>(values[2]))
                        .setModel(values[3])
                        .setPotrosnja(double.Parse(values[4], CultureInfo.InvariantCulture));

                    if (!string.IsNullOrWhiteSpace(values[5]))
                    {
                        builder.setKubikaza(int.Parse(values[5]));
                    }
                    if (!string.IsNullOrWhiteSpace(values[6]))
                    {
                        builder.setKilometraza(int.Parse(values[6]));
                    }
                    if (!string.IsNullOrWhiteSpace(values[7]))
                    {
                        builder.setSnaga(int.Parse(values[7]));
                    }

                    builder.setTip(Enum.Parse<Tip>(values[8]));

                    vozila.Add(builder.Build());
                }
            }

            return vozila;
        }
        public List<Vozilo> loadVozilaOprema(List<Vozilo> vozila,List<Oprema> oprema)
        {
            var vozilo_oprema = new List<VoziloOprema>();

            using (var reader = new StreamReader(vozilo_opremaCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var vo = new VoziloOprema();
                    vo.VoziloID = int.Parse(values[0]);
                    vo.OpremaID = int.Parse(values[1]);

                    vozilo_oprema.Add(vo);
                }
            }

            foreach (var vo in vozilo_oprema)
            {
                foreach (var vozilo in vozila.Where(v => v.Id.Equals(vo.VoziloID) && v.oprema is null))
                {
                    vozilo.oprema = oprema.Where(o => vo.OpremaID.Equals(o.Id)).ToList();
                }
            }

            return vozila;
        }
        public List<ZahtevRezervacija> loadZahteviRezervacije()
        {
            var zahtevi_za_rezervaciju = new List<ZahtevRezervacija>();

            using (var reader = new StreamReader(zahtevi_za_rezervacijeCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var zr = new ZahtevRezervacija();
                    zr.VoziloID = int.Parse(values[0]);
                    zr.KupacID = int.Parse(values[1]);
                    zr.DatumDolaska=DateOnly.Parse(values[2]);
                    zr.PocetakRezervacije=DateOnly.Parse(values[3]);
                    zr.BrojDana=int.Parse(values[4]);

                    zahtevi_za_rezervaciju.Add(zr);
                }
            }

            return zahtevi_za_rezervaciju;
        }
        public void sacuvajNoveRezervacije(Rezervacija novaRezervacija)
        {

            string newLine = $"{novaRezervacija.VoziloId},{novaRezervacija.KupacId},{novaRezervacija.PocetakRezervacije},{novaRezervacija.KrajRezervacije}";
            try
            {
                using (StreamWriter sw = new StreamWriter(nove_rezervacijeCSV, true))
                {
                    sw.WriteLine(newLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom čuvanja rezervacije: {ex.Message}");
            }
        }
        public List<Rezervacija> loadNoveRezervacije()
        {
            var nove_rezervacije = new List<Rezervacija>();

            using (var reader = new StreamReader(nove_rezervacijeCSV))
            {
                string headerLine = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var rezervacija = new Rezervacija();
                    rezervacija.VoziloId = int.Parse(values[0]);
                    rezervacija.KupacId = int.Parse(values[1]);
                    rezervacija.PocetakRezervacije = DateOnly.Parse(values[2]);
                    rezervacija.KrajRezervacije = DateOnly.Parse(values[3]);

                    nove_rezervacije.Add(rezervacija);
                }
            }

            return nove_rezervacije;
        }
    }
}
