﻿using Rent_a_car_Simulation_Inovatec.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Models
{
    public class Kupac : IReadCSV<Kupac>
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public float Budzet { get; set; }
        public string Clanarina { get; set; }
    }
}
