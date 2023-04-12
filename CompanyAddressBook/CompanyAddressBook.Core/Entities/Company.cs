using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAddressBook.Core.Entities
{
    public class Company : BaseEntity<int>
    {
        /// <summary>
        /// The name of the company.
        /// </summary>
        [Required]
        public string CompanyName { get; set; }

        /// <summary>
        /// Maximum number of contacts
        /// </summary>
        [Required]
        public int NumContacts { get; set; }

        /// <summary>
        /// The maximum age in years that a contact can have
        /// </summary>
        [Required]
        public int MaxContactAge { get; set; }

        [InverseProperty(nameof(CompanyContact.Company))]
        public virtual ICollection<CompanyContact> Contacts { get; set; }
    }
}
