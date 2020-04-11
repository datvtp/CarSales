using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.dtos
{
    public class InvoiceDTO
    {
        public int ID { get; set; }
        public DateTime Date_Of_Purcharse { get; set; }
        public int Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public double Total { get; set; }
    }
}
