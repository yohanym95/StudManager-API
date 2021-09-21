using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudManager.Data.Data.Entities
{
    public class Fees
    {
        public int Id { get; set; }
        public string FeesType { get; set; }
        public string AmountofFees { get; set; }
        public string RecieptNo { get; set; }
        public string FeesDescription { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [ForeignKey("Student")]
        public int StuId { get; set; }


    }
}
