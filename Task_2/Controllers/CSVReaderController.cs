using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Transactions;
using Task_2.DTO;
using Task_2.Models;

namespace Task_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVReaderController : ControllerBase
    {
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a valid CSV file.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    //{
                    //    HasHeaderRecord = false 
                    //};


                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap(new ReceiptsMap());
                        var records = csv.GetRecords<Receipts>().ToList();
                        var receipts = new ReceiptDTO();


                        #region oragnize json
                        receipts.BusinessUnit = records[0].businessUnit;
                        receipts.ReceiptMethodId = records[0].receiptMethodId;
                        List<Transactions> transactions = new List<Transactions>();
                        foreach (Receipts record in records)
                        {
                            Transactions trans = new Transactions();
                            trans.receiptNumber = record.receiptNumber;
                            trans.invoiceNumberReference = record.invoiceNumberReference;
                            trans.remittanceBank = record.remittanceBank;
                            trans.receiptAmount = record.receiptAmount;
                            trans.invoiceAmount = record.receiptAmount;
                            transactions.Add(trans);
                        }
                        receipts.Transactions = transactions;
                        #endregion


                        return Ok(receipts);
                    }
                }
            }
        }


    }
}
