using CompanyAddressBook.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAddressBook.Web.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// The age of the contact.
        /// </summary>
        [Required]
        public int ContactAge { get; set; }

        /// <summary>
        /// The phone number of the contact.
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{7,13}$", ErrorMessage = "Contact number should only contain digits and should be between 7 and 13 digits long")]
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets Company Id
        /// </summary>
        public int CompanyId { get; set; }


    }
}