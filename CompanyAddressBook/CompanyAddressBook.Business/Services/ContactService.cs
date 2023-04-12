using CompanyAddressBook.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using CompanyAddressBook.Core.Entities;
using CompanyAddressBook.Core.Interfaces;

namespace CompanyAddressBook.Business.Services
{
        /// <inheritdoc />
    public class ContactService : IContactService
    {
        private readonly IBaseRepository<CompanyContact> _repository;
        private readonly ICompanyService _CompanyService;
        private readonly ILogger _logger;

        /// <inheritdoc />
        public ContactService(IBaseRepository<CompanyContact> repository, ICompanyService CompanyService, ILogger<ContactService> logger)
        {
            _repository = repository;
            _CompanyService = CompanyService;
            _logger = logger;
        }

        /// <inheritdoc />
        public List<CompanyContact> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        /// <inheritdoc />
        public CompanyContact AddContact(CompanyContact Contact)
        {
            Contact.Company = _CompanyService.GetById(Contact.CompanyId);
            if (ValidateContact(Contact))
                return _repository.Add(Contact,"ContactNumber");
            else
                return null;
        }

        /// <inheritdoc />
        private bool ValidateContact(CompanyContact Contact)
        {
            var regex = new Regex(@"^\d{7,13}$");
            var isContactNumberValid = regex.IsMatch(Contact.ContactNumber);
            var isContactAgeValid = Contact.ContactAge <= Contact.Company.MaxContactAge;
            var isNumContactsValid = Contact.Company.NumContacts >= (Contact.Company.Contacts.Count+1);
            return isContactNumberValid && isContactAgeValid && isNumContactsValid;

        }


    }
}
