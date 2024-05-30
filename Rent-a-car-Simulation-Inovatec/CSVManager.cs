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
        string kupciCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\kupci.csv");
        string opremaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\oprema.csv");
        string rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\rezervacije.csv");
        string vozilaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozila.csv");
        string vozilo_opremaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozilo_oprema.csv");
        string zahtevi_za_rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\zahtevi_za_rezervacije.csv");

        public List<Kupac> loadKupci()
        {
            throw new NotImplementedException();
        }

        public List<Oprema> loadOprema()
        {
            throw new NotImplementedException();
        }

        public List<Rezervacija> loadRezervacija()
        {
            throw new NotImplementedException();
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
        public List<VoziloOprema> loadVozilaOprema()
        {
            throw new NotImplementedException();
        }

        public List<ZahtevRezervacija> loadZahteviRezervacije()
        {
            throw new NotImplementedException();
        }

        public void createNoveRezervacije(List<Rezervacija> noveRezervacije)
        {
            throw new NotImplementedException();
        }

        public void izlistajVozila(List<Vozilo> vozila)
        {
            foreach (var vozilo in vozila)
            {
                Console.WriteLine($"{vozilo.Id},{vozilo.tipVozila},{vozilo.marka},{vozilo.Model},{vozilo.Potrosnja},{vozilo.Kubikaza},{vozilo.Kilometraza},{vozilo.Snaga},{vozilo.tip}");
            }
        }
    }
}
