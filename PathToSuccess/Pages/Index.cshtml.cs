using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace PathToSuccess.Pages
{
    public class IndexModel : PageModel
    {
        public string ThePath { get; set; }
        public void OnGet()
        {
            ThePath = Path.Combine("one", "two", "three", "four");
        }
    }
}
