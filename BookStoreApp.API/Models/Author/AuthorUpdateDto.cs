using BookStoreApp.API.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author
{
    [RequireNameOrAlias]
    public class AuthorUpdateDto : BaseDto
    {
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Alias { get; set; }

        [StringLength(250)]
        public string? Bio { get; set; }
    }
}
