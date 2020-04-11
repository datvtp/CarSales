using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.dtos
{
    public class CarDTO
    {
        public int ID { get; set; }
        public string Model_Name { get; set; }
        public double Price { get; set; }
        public int Produced_Year { get; set; }
        public DateTime Accquired_Date { get; set; }
        public int Engine { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer_Name { get; set; }
        public string Tranmission_Description { get; set; }
        public string Type_Description { get; set; }
        public string Category_Description { get; set; }
        public string Fuel_Description { get; set; }
        public string Status_Description { get; set; }
    }
}
