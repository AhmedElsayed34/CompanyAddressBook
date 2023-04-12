using AutoMapper;
using CompanyAddressBook.Core.Entities;
using CompanyAddressBook.Core.Interfaces;
using CompanyAddressBook.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CompanyAddressBook.Web.Controllers
{
    /// <summary>
    /// Controller class for managing company entities.
    /// </summary>
    public class CompanyController : Controller
    {
        private readonly ICompanyService _CompanyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyController"/> class.
        /// </summary>
        /// <param name="CompanyService">The company service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public CompanyController(ICompanyService CompanyService, IMapper mapper, ILogger<CompanyController> logger)
        {
            _CompanyService = CompanyService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Displays a list of all companies.
        /// </summary>
        /// <returns>The view containing the list of all companies.</returns>
        public IActionResult Index()
        {
            var companies = _CompanyService.GetAll();
            var companyViewModels = _mapper.Map<List<CompanyViewModel>>(companies);

            return View(companyViewModels);
        }

        /// <summary>
        /// Displays the view for adding a new company.
        /// </summary>
        /// <returns>The view for adding a new company.</returns>
        [HttpGet]
        public IActionResult AddCompany()
        {
            return View();
        }

        /// <summary>
        /// Adds a new company.
        /// </summary>
        /// <param name="companyViewModel">The view model for the new company.</param>
        /// <returns>If the addition is successful, redirects to the Index view; otherwise, returns the AddCompany view with an error message.</returns>
        [HttpPost]
        public IActionResult AddCompany(CompanyViewModel companyViewModel)
        {
            try
            {
                var company = _CompanyService.AddCompany(_mapper.Map<Company>(companyViewModel));
                if (company == null)
                {
                    ModelState.AddModelError("CompanyName", "Company Name is not valid");
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Adds multiple companies from a file.
        /// </summary>
        /// <param name="file">The file containing the companies.</param>
        /// <returns>If the addition is successful, redirects to the Index view; otherwise, returns the Index view with an error message.</returns>
        [HttpPost]
        public IActionResult AddCompanies(IFormFile file)
        {
            try
            {
                _CompanyService.UploadCompanyFile(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return RedirectToAction("Index");
        }

    }
}
