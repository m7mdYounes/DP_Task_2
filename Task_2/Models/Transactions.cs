using System.ComponentModel.DataAnnotations;

namespace Task_2.Models
{
    public class Transactions
    {
        [Display(Name = "Remittance Bank")]
        public string? remittanceBank { get; set; }
        [Display(Name = "Receipt Number")]
        public string? receiptNumber { get; set; }
        [Display(Name = "Receipt Amount")]
        public decimal? receiptAmount { get; set; }
        [Display(Name = "Invoice Number Reference")]
        public string? invoiceNumberReference { get; set; }
        [Display(Name = "Invoice Amount")]
        public decimal? invoiceAmount { get; set; }
    }
}
