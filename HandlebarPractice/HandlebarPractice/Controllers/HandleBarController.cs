using HandlebarPractice.Interfaces;
using HandlebarPractice.Models;
using Microsoft.AspNetCore.Mvc;
using HandlebarsDotNet;
using HandlebarPractice.Services;

namespace HandlebarPractice.Controllers
{
    public class HandleBarController : Controller
    {
        private readonly ICustomer _customerService;
        private readonly IPdf _pdfService;
        private readonly ITemplateResolver _templateResolver;
        private readonly ITemplate _templateService;

        public HandleBarController(ICustomer customerService, IPdf pdfService, ITemplate templateService,ITemplateResolver templateResolver)
        {
            _customerService = customerService;
            _pdfService = pdfService;
            _templateService = templateService;
            _templateResolver = templateResolver;
        }

        #region Index (List + Form)

        public IActionResult Index(long? id)
        {
            var vm = new CustomerVM
            {
                CustomerList = _customerService.GetAllCustomers()
            };

            // 🔹 Edit mode
            if (id.HasValue && id.Value > 0)
            {
                var customer = _customerService.GetCustomerById(id.Value);
                if (customer != null)
                {
                    vm.Customer = customer;
                }
            }

            return View(vm);
        }
        #endregion

        #region Save (Insert / Update)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCustomer(CustomerVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CustomerList = _customerService.GetAllCustomers();
                return View("Index", vm);
            }

            try
            {
                _customerService.SaveCustomer(vm.Customer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong while saving customer.");
            }
        }
        #endregion

        #region Delete (Soft Delete)
        public IActionResult Delete(long id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong while deleting customer.");
            }
        }
        #endregion

        public IActionResult CustomerReport(string lang = "en", string variant = "default")
        {
            var vm = new CustomerVM
            {
                CustomerList = _customerService.GetAllCustomers(),
                GeneratedOn = DateTime.Now

            };

            var templatePath = _templateResolver.Resolve("customer-report", lang, variant);

            var html = _templateService.Render(templatePath, vm);

            return Content(html, "text/html");
        }

        public IActionResult CustomerReportPdf(string lang = "en", string variant = "default")
        {
            var vm = new CustomerVM
            {
                CustomerList = _customerService.GetAllCustomers(),
                GeneratedOn = DateTime.Now
            };

            var templatePath = _templateResolver.Resolve("customer-report", lang, variant);

            var html = _templateService.Render(templatePath, vm);

            var pdf = _pdfService.GeneratePdf(html);

            return File(pdf, "application/pdf", "CustomerReport.pdf");
        }

        //public IActionResult CustomerPdf(long id, string lang = "en")
        //{
        //    var customer = _customerService.GetCustomerById(id);

        //    if (customer == null)
        //        return NotFound();

        //    var templatePath = $"Templates/{lang}/customer-report.hbs";

        //    var html = _templateService.Render(templatePath, customer);

        //    var pdf = _pdfService.GeneratePdf(html);

        //    return File(pdf, "application/pdf", $"Customer_{id}.pdf");
        //}
    }
}
