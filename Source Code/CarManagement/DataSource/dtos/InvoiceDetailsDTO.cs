using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.dtos
{
    public class InvoiceDetailsDTO
    {
        public int ID { get; set; }
        public int Car_ID { get; set; }
        public int Invoice_ID { get; set; }
        public double Unit_Price { get; set; }
        public int Quantity { get; set; }
    }
}
