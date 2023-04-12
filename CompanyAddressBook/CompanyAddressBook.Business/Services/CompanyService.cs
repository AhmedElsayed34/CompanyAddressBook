using CompanyAddressBook.Core.Interfaces;
using CompanyAddressBook.Core.Entities;
using CompanyAddressBook.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace CompanyAddressBook.Business.Services
{
        /// <inheritdoc />
    public class CompanyService : ICompanyService
    {
        private readonly IBaseRepository<Company> _repository;
        private readonly ILogger _logger;

        /// <inheritdoc />
        public CompanyService(IBaseRepository<Company> repository, ILogger<CompanyService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public List<Company> GetAll()
        {
            return _repository.GetAll(includes: c => c.Contacts).ToList();
        }

        /// <inheritdoc />
        public Company GetById(int id)
        {
            return _repository.GetById(id,includes: c => c.Contacts);
        }

        /// <inheritdoc />
        public Company AddCompany(Company company)
        {
            if (ValidateCompanyName(company.CompanyName))
                return _repository.Add(company, "CompanyName");
            else
                return null;
        }

        /// <inheritdoc />
        public IEnumerable<Company> UploadCompanyFile(IFormFile file)
        {
            var companies = GetCompaniesListFromFile(file);
            return _repository.AddRange(companies, "CompanyName");
        }

        /// <inheritdoc />
        private List<Company> GetCompaniesListFromFile(IFormFile file)
        {
            var companies = new List<Company>();
            try
            {
                if (file != null && file.Length > 0)
                {

                    // Read the file content and parse the data
                    using (var streamReader = new StreamReader(file.OpenReadStream()))
                    {
                        string companyString = streamReader.ReadToEnd();
                        var fields = companyString.Split("\t\t");
                        var company = new Company();
                        var numContacts = 0;
                        var maxContactAge = 0;
                        for (int i = 0; i < fields.Length; i++)
                        {
                            try
                            {
                                company = new Company();
                                company.CompanyName = fields[i];

                                if (!ValidateCompanyName(company.CompanyName))
                                    continue;

                                int.TryParse(fields[++i], out numContacts);
                                company.NumContacts = numContacts;
                                var match = Regex.Match(fields[++i], @"^\d+");
                                if (match.Success)
                                    int.TryParse(match.Value, out maxContactAge);
                                else
                                    int.TryParse(fields[i--], out maxContactAge);

                                company.MaxContactAge = maxContactAge;

                                if (!companies.Any(s => s.CompanyName.Trim().ToLower() == company.CompanyName.Trim().ToLower()))
                                    companies.Add(company);
                            }
                            catch  (Exception ex) { 
                                _logger.LogError(ex.ToString());
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

            }
            return companies;
        }

        /// <inheritdoc />
        private bool ValidateCompanyName(string companyName)
        {
            var regex = new Regex(@"\d[§®™©ʬ@]");
            return regex.IsMatch(companyName);
        }

    }
}
