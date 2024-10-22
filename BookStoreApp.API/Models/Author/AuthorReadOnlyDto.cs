using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author
{
    /*
     * Read-only class
     * Not for any CRUD operations
     */
    public class AuthorReadOnlyDto : BaseDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }

        public string Bio { get; set; } = string.Empty;
    }
}
