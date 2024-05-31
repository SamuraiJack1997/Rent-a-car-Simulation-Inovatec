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
        string kupciCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\kupci.csv");
        string opremaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\oprema.csv");
        string rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\rezervacije.csv");
        string vozilaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozila.csv");
        string vozilo_opremaCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\vozilo_oprema.csv");
        string zahtevi_za_rezervacijeCSV = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CSV\zahtevi_za_rezervacije.csv");

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
        public List<VoziloOprema> loadVozilaOprema()
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

            return vozilo_oprema;
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

        public void sacuvajNoveRezervacije(List<Rezervacija> noveRezervacije)
        {
            //TODO sacuvajNoveRezervacije
            throw new NotImplementedException();
        }

        public void izlistajVozila(List<Vozilo> vozila)
        {
            foreach (var vozilo in vozila) 
            {
                string v;
                v="Id:"+vozilo.Id+", TipVozila:"+vozilo.tipVozila+", Marka:"+vozilo.marka+", Model:"+vozilo.Model+", Potrosnja:"+vozilo.Potrosnja+", ";

                if (vozilo.Kubikaza.Equals(null)) v += "Kubikaza:None, "; else v += " Kubikaza:"+vozilo.Kubikaza+", ";
                if (vozilo.Kilometraza.Equals(null)) v += "Kilometraza:None, "; else v += "Kilometraza:"+vozilo.Kilometraza + ", ";
                if (vozilo.Snaga.Equals(null)) v += "Snaga:None, "; else v += "Snaga:"+vozilo.Snaga + ", ";

                v += "Tip:"+vozilo.tip;
                Console.WriteLine(v);
            }
        }

        public void izlistajKupce(List<Kupac> kupci)
        {
            foreach (var kupac in kupci)
                Console.WriteLine($"Id:{kupac.Id}, Ime:{kupac.Ime}, Prezime:{kupac.Prezime}, Budzet:{kupac.Budzet}, Clanarina:{kupac.clanarina}");
        }

        public void izlistajOpremu(List<Oprema> oprema)
        {
            foreach (var o in oprema)
                Console.WriteLine($"Id:{o.Id}, Naziv:{o.Naziv}, Cena:{o.Cena}, PovecavaCenu:{o.PovecavaCenu}");
        }
        public void izlistajRezervacije(List<Rezervacija> rezervacije)
        {
            foreach (var rezervacija in rezervacije)
                Console.WriteLine($"VoziloId: {rezervacija.VoziloId}, KupacId:{rezervacija.KupacId}, PocetakRezervacije:{rezervacija.PocetakRezervacije}, KrajRezervacije:{rezervacija.KrajRezervacije}");
        }
        public void izlistajOpremuPoVozilu(List<VoziloOprema> vozilo_oprema)
        {
            foreach(var vo in vozilo_oprema)
                Console.WriteLine($"VoziloId:{vo.VoziloID}, OpremaId:{vo.OpremaID}");
        }
        public void izlistajZahteveZaRezervaciju(List<ZahtevRezervacija> zahteviRezervacija)
        {
            foreach (var zr in zahteviRezervacija)
                Console.WriteLine($"VoziloId:{zr.VoziloID}, KupacId:{zr.KupacID}, DatumDolaska:{zr.DatumDolaska}, PocetakRezervacije:{zr.PocetakRezervacije}, BrojDana:{zr.BrojDana}");
        }
        public void izlistajNoveRezervacije(List<Rezervacija> noveRezervacije)
        {
            //TODO izlistajNoveRezervacije
            throw new NotImplementedException();
        }

    }
}
