using System.Transactions;
using Task_2.Models;

namespace Task_2.DTO
{
    public class ReceiptDTO
    {
        public string? BusinessUnit { get; set; }
        public string? ReceiptMethodId { get; set; }
        public List<Transactions>? Transactions { get; set; }
    }
}
