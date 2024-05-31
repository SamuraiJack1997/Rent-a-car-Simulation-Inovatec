using Rent_a_car_Simulation_Inovatec.Interfaces;
using Rent_a_car_Simulation_Inovatec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec
{
    public class OutputManager : IOutputManager
    {
        CSVManager csvManager=CSVManager.GetInstance();

        string breaks="-------------------------------------------------";
        public void izlistajVozila(List<Vozilo> vozila)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(breaks);
            Console.WriteLine("Vozila:");
            Console.WriteLine(breaks+"\n");
            Console.ResetColor();

            int brojac = 0;
            foreach (var vozilo in vozila)
            {
                brojac++;
                Console.WriteLine(brojac+".vozilo"+breaks+breaks+":");
                Console.WriteLine("Vozilo: "+vozilo.ToString());
                if (!(vozilo.oprema is null))
                {
                    Console.WriteLine("Oprema u vozilu:");
                    foreach (var o in vozilo.oprema)
                    {
                        Console.WriteLine("-"+o.ToString());
                    }
                }
                else Console.WriteLine("Oprema u vozilu: Nema dodatne opreme");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n-Cena iznajmljivanja po danu: "+vozilo.IzracunajCenu(null,1)+"e\n");
                Console.ResetColor();
            }
            Console.WriteLine("\n");
        }
        public void izlistajKupce(List<Kupac> kupci)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(breaks);
            Console.WriteLine("Kupci:");
            Console.WriteLine(breaks);
            Console.ResetColor();

            int brojac = 0;
            foreach (var kupac in kupci)
            {
                brojac++;
                Console.WriteLine(brojac + ".kupac: " + kupac.ToString());
            }
            Console.WriteLine("\n");
        }
        public void izlistajOpremu(List<Oprema> oprema)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(breaks);
            Console.WriteLine("Oprema:");
            Console.WriteLine(breaks);
            Console.ResetColor();

            int brojac = 0;
            foreach (var o in oprema)
            {
                brojac++;
                Console.WriteLine(brojac+".oprema: "+o.ToString());
            }   
            Console.WriteLine("\n");
        }
        public void izlistajRezervacije(List<Rezervacija> rezervacije)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(breaks);
            Console.WriteLine("Rezervacije:");
            Console.WriteLine(breaks);
            Console.ResetColor();

            foreach (var rezervacija in rezervacije)
                Console.WriteLine(rezervacija.ToString());
            Console.WriteLine("\n");
        }
        public void izlistajZahteveZaRezervaciju(List<ZahtevRezervacija> zahteviRezervacija,List<Kupac> kupci,List<Vozilo> vozila)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(breaks);
            Console.WriteLine("Zahtevi za rezervacije:");
            Console.WriteLine(breaks);
            Console.ResetColor();

            Vozilo vozilo = null;
            Kupac kupac = null;
            int brojac = 0;
            foreach (var zr in zahteviRezervacija)
            {
                brojac++;
                foreach(var k in kupci)
                    if(k.Id.Equals(zr.KupacID))
                    {
                        kupac = k;
                        break;
                    }
                foreach(var v in vozila)
                {
                    if (v.Id.Equals(zr.VoziloID))
                    {
                        vozilo = v;
                        break;
                    }
                }

                double pocetnaCena = vozilo.IzracunajCenu(null, zr.BrojDana);
                double cenaSaPopustom = vozilo.IzracunajCenu(kupac, zr.BrojDana);
                double ostvareniPopust = pocetnaCena - cenaSaPopustom;

                Console.WriteLine(brojac+". zahtev:\n"+$"Vozilo: {vozilo.tipVozila}[{vozilo.marka} {vozilo.Model}]\nKupac: {kupac.Ime} {kupac.Prezime}\n-Budzet: {kupac.Budzet}e\nInfo:\n-Datum Dolaska: {zr.DatumDolaska}\n-Pocetak Rezervacije: {zr.PocetakRezervacije}\n-Zahtevan broj dana: {zr.BrojDana}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"-Clanarina kupca: {kupac.clanarina}\n-Cena zahteva: " + cenaSaPopustom + "e\n-Ostvaren popust kupca: "+ ostvareniPopust+"e\n");
                Console.ResetColor();
            }
            Console.WriteLine("\n");
        }
        public void izlistajNoveRezervacije(List<Rezervacija> nove_rezervacije, List<Kupac> kupci, List<Vozilo> vozila)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n"+breaks);
            Console.WriteLine("Uspesno sacuvane rezervacije:");
            Console.WriteLine(breaks);
            Console.ResetColor();

            Vozilo vozilo = null;
            Kupac kupac = null;
            int brojac = 0;
            foreach (var rezervacija in nove_rezervacije)
            {
                brojac++;
                foreach (var k in kupci)
                    if (k.Id.Equals(rezervacija.KupacId))
                    {
                        kupac = k;
                        break;
                    }
                foreach (var v in vozila)
                {
                    if (v.Id.Equals(rezervacija.VoziloId))
                    {
                        vozilo = v;
                        break;
                    }
                }

                Console.WriteLine(brojac + ". rezervacija:\n" + $"Vozilo: {vozilo.tipVozila}[{vozilo.marka} {vozilo.Model}]  Tip[{vozilo.tip}]\nKupac: {kupac.Ime} {kupac.Prezime}\nInfo:\n-Pocetak rezervacije: {rezervacija.PocetakRezervacije}\n-Kraj rezervacije: {rezervacija.KrajRezervacije}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-Budzet kupca nakon rezervacije: " + kupac.Budzet + "e\n");
                Console.ResetColor();
            }

            Console.WriteLine("\n");
        }
    }
}
