namespace CompanyAddressBook.Core.Entities
{
    /// <summary>
    /// Represents the base entity class for all entities in the application.
    /// </summary>
    /// <typeparam name="T">The type of the entity's primary key.</typeparam>
    public abstract class BaseEntity<T>
    {
        /// <summary>
        /// Gets or sets the entity's primary key.
        /// </summary>
        public virtual T Id { get; set; }
    }
}
