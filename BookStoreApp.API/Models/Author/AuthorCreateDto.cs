using BookStoreApp.API.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author
{
    [RequireNameOrAlias]
    public class AuthorCreateDto
    {
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(50)]
        public string? Alias { get; set; }

        [StringLength(250)]
        public string? Bio { get; set; } = string.Empty;
    }
}
