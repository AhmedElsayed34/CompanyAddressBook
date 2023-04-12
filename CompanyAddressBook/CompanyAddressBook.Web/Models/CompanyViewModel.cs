using System.ComponentModel.DataAnnotations;

namespace CompanyAddressBook.Web.Models
{
    /// <summary>
    /// Represents a view model for a company entity.
    /// </summary>
    public class CompanyViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the company.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        [Required]
        [RegularExpression(@"\d[§®™©ʬ@]", ErrorMessage = "Company Name should have special characters (§,®,™,©,ʬ,@) preceded by a digit at names")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of contacts that the company can have.
        /// </summary>
        [Required]
        public int NumContacts { get; set; }

        /// <summary>
        /// Gets or sets the maximum age of contacts that the company can have.
        /// </summary>
        [Required]
        public int MaxContactAge { get; set; }

        /// <summary>
        /// Gets or sets a collection of contacts that belong to the company.
        /// </summary>
        public virtual ICollection<ContactViewModel> Contacts { get; set; }
    }
}
