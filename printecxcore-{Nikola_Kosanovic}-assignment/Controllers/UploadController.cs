using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using printecxcore__Nikola_Kosanovic__assignment.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace printecxcore__Nikola_Kosanovic__assignment.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Uploader()
        {
            return View();
        }

        public async Task<IActionResult> FileUpload( IFormFile file)
        {
            byte[] fileBytes = null;
            
            if ( file is not null && file.Length > 0)
                using (var memoryStream = new MemoryStream())
                {
                    //Read uploaded QR Code
                    file.CopyTo(memoryStream);

                    Bitmap img = new Bitmap(memoryStream);

                    var barcodeReader = new ZXing.Windows.Compatibility.BarcodeReader();
                    var QRtext = barcodeReader.Decode(img).ToString();
                
                    //Do the parsing of data loaded through QR Code - first take the values according to | symbol, then parse them by important letters and semicolon 
                    var data = QRtext.Split("|").ToList().Select(x => new QrCodeValue { Key = x.Split(":")[0], Value = x.Split(":")[1] }).ToDictionary(x => x.Key, x => x.Value);

                    _ = data.TryGetValue("R", out string myValue);
                    _ = data.TryGetValue("N", out string myValue1);
                    _ = data.TryGetValue("P", out string myValue2);
                    _ = data.TryGetValue("SF", out string myValue3);
                    _ = data.TryGetValue("S", out string myValue4);
                    _ = data.TryGetValue("RO", out string myValue5);
                    _ = data.TryGetValue("I", out string myValue6);
                
                    //Do the rest of the parsing in Dictionary and assign values to Model properties, because there happened to be multiple Model properties in one Dictionary value

                    UtilityTemplate utilityTemplate = new UtilityTemplate();
                    utilityTemplate.CreditorAccount = myValue ?? "";

                    string creditorPersonalData = myValue1 ?? "";
                    string[] splitCreditorPersonalData = creditorPersonalData.Split('\n');
                    utilityTemplate.CreditorName = splitCreditorPersonalData[0];
                    utilityTemplate.CreditorAddressLine = splitCreditorPersonalData[1];
                    utilityTemplate.CreditorCity = splitCreditorPersonalData[2];

                    string debtorPersonalData = myValue2 ?? "";
                    string[] splitDebtorPersonalData = debtorPersonalData.Split('\n');
                    utilityTemplate.DebtorName = splitDebtorPersonalData[0];
                    utilityTemplate.DebtorAddressLine = splitDebtorPersonalData[1];
                    utilityTemplate.DebtorCity = splitDebtorPersonalData[2];

                    utilityTemplate.PurposeCode = myValue3 ?? "";
                    utilityTemplate.PaymentDescription = myValue4 ?? "";

                    string creditorReferences = myValue5 ?? "";
                    utilityTemplate.CreditorReferenceModel = creditorReferences.Substring(0, 2);
                    utilityTemplate.CreditorReference = creditorReferences.Substring(2);
                
                    string baseAmountString = myValue6 ?? "";
                    utilityTemplate.Amount = decimal.Parse(baseAmountString.Substring(3));
                    string currencyString = baseAmountString.Substring(0, 3);
                    utilityTemplate.CurrencyCode = currencyString.Equals("EUR") ? "978" : "";
                
                    //Send the request to display the Template page with the model populated with QR Code data

                    return RedirectToAction("Template", "UtilityTemplate", utilityTemplate);
                }
            return NotFound();
        }
    }
}
