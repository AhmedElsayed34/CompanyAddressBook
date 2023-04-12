using CompanyAddressBook.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAddressBook.Core.Interfaces
{
    /// <summary>
    /// Interface for managing company contacts.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Get all company contacts.
        /// </summary>
        /// <returns>List of CompanyContact objects.</returns>
        List<CompanyContact> GetAll();

        /// <summary>
        /// Add a new company contact.
        /// </summary>
        /// <param name="Contact">The CompanyContact object to add.</param>
        /// <returns>The added CompanyContact object.</returns>
        CompanyContact AddContact(CompanyContact Contact);

    }
}
