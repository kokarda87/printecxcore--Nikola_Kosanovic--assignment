using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using printecxcore__Nikola_Kosanovic__assignment.Models;

namespace printecxcore__Nikola_Kosanovic__assignment.Controllers
{
    public class UtilityTemplateController : Controller
    {
        public IActionResult Template(UtilityTemplate utilityTemplate)
        {
            // Fill DropDownList Items with predefined Debtors Accounts so any instance of a model in the end does the populating of items
            utilityTemplate.DebtorAccountList = new List<SelectListItem>
            {
                new SelectListItem { Text = "0011225333665", Value = "1" },
                new SelectListItem { Text = "0065533554433", Value = "2" },
                new SelectListItem { Text = "00668855441122", Value = "3" }
            };
            return View(utilityTemplate);
        }
    }
}
