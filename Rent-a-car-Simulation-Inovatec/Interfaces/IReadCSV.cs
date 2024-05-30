using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_a_car_Simulation_Inovatec.Interfaces
{
    public interface IReadCSV<T>
    {
        public List<T> ReadCSV<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                string formattedContent = fileContent.Replace(", ", "\n");
                using (var reader = new StringReader(formattedContent))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                }))
                {
                    var records = csv.GetRecords<T>().ToList();
                    return records;
                }
            }
            else
            {
                Console.WriteLine("Fajl nije pronadjen: " + filePath);
                return null;
            }
        }
    }
}
