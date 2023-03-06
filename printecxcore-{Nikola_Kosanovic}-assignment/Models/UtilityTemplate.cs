using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace printecxcore__Nikola_Kosanovic__assignment.Models
{
    public class UtilityTemplate
    {
        public UtilityTemplate()
        {
            //Customer’s data(name, address…) should be populated by default.I have done this through a Constructor of this Model
            DebtorName = "John Do";
            DebtorAddressLine = "Joke Street 69";
            DebtorCity = "10009 Joke City";
        }
        public string DebtorName { get; set; }
        public string DebtorAddressLine { get; set; }
        public string DebtorCity { get; set; }
        public string CreditorName { get; set; }
        public string CreditorAddressLine { get; set; }
        public string CreditorCity { get; set; }
        public string PaymentDescription { get; set; }
        public string PurposeCode { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string DebtorAccount { get; set; }
        public List<SelectListItem> DebtorAccountList { get; set; }
        public string CreditorAccount { get; set; }
        public string CreditorReference { get; set; }
        public string CreditorReferenceModel { get; set; }

    }
}
