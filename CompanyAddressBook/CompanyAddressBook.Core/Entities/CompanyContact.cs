using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAddressBook.Core.Entities
{
    public class CompanyContact : BaseEntity<int>
    {

        /// <summary>
        /// The age of the contact.
        /// </summary>
        [Required]
        public int ContactAge { get; set; }

        /// <summary>
        /// The phone number of the contact.
        /// </summary>
        [Required]
        public string ContactNumber { get; set; }


        /// <summary>
        /// Gets or sets Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Contact Company
        /// </summary>
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}
