using CompanyAddressBook.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAddressBook.Core.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>List of companies</returns>
        List<Company> GetAll();

        /// <summary>
        /// Retrieves a single company by its Id.
        /// </summary>
        /// <param name="id">Id of the company to retrieve</param>
        /// <returns>Company object</returns>
        Company GetById(int id);

        /// <summary>
        /// Adds a new company to the database.
        /// </summary>
        /// <param name="company">Company object to add</param>
        /// <returns>Added Company object</returns>
        Company AddCompany(Company company);

        /// <summary>
        /// Uploads a file containing company data.
        /// </summary>
        /// <param name="file">IFormFile object containing the data to upload</param>
        /// <returns>Enumerable collection of Company objects added</returns>
        IEnumerable<Company> UploadCompanyFile(IFormFile file);
    }
}



