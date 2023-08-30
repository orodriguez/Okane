using System.ComponentModel.DataAnnotations;

namespace Okane.Contracts
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
