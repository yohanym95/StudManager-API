using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Core.Models
{
    public class FeesViewModel
    {
        public int Id { get; set; }
        public string FeesType { get; set; }
        public string AmountofFees { get; set; }
        public string RecieptNo { get; set; }
        public string FeesDescription { get; set; }
        public int StuId { get; set; }
        public string studName { get; set; }
        public string studRegNo { get; set; }
    }
}
