using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Data.Data.Entities
{
    public class Fees
    {
        public int Id { get; set; }
        public string AmountofFees { get; set; }
        public string RecieptNo { get; set; }
        public DateTime DateofReceipt { get; set; }
        
    }
}
