using AutoMapper;
using CompanyAddressBook.Core.Entities;
using CompanyAddressBook.Core.Interfaces;
using CompanyAddressBook.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAddressBook.Web.Controllers
{
    /// <summary>
    /// Controller for managing contact-related actions
    /// </summary>
    public class ContactController : Controller
    {
        private readonly ICompanyService _CompanyService;
        private readonly IContactService _ContactService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactController"/> class.
        /// </summary>
        /// <param name="ContactService">The contact service.</param>
        /// <param name="CompanyService">The company service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public ContactController(IContactService ContactService, ICompanyService CompanyService, IMapper mapper, ILogger<ContactController> logger)
        {
            _ContactService = ContactService;
            _CompanyService = CompanyService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Displays the list of all contacts.
        /// </summary>
        /// <returns>The index view with the list of all contacts.</returns>
        public IActionResult Index()
        {
            var companies = _ContactService.GetAll();
            var ContactViewModels = _mapper.Map<List<ContactViewModel>>(companies);

            return View(ContactViewModels);
        }

        /// <summary>
        /// Displays the form to add a new contact.
        /// </summary>
        /// <returns>The add contact view.</returns>
        [HttpGet]
        public IActionResult AddContact()
        {
            ViewBag.Companies = _CompanyService.GetAll();
            return View();
        }

        /// <summary>
        /// Adds a new contact to the database.
        /// </summary>
        /// <param name="ContactViewModel">The contact view model.</param>
        /// <returns>The index view of the company controller.</returns>
        [HttpPost]
        public IActionResult AddContact(ContactViewModel ContactViewModel)
        {
            try
            {
                var Contact = _ContactService.AddContact(_mapper.Map<CompanyContact>(ContactViewModel));
                if (Contact == null)
                {
                    ModelState.AddModelError("ContactNumber", "Contact Is Not Valid");
                    ViewBag.Companies = _CompanyService.GetAll();
                    return View(ContactViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return RedirectToAction("Index", "Company");
        }

    }
}
