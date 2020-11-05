using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirper.Server.Pages
{
    [Authorize]
    public class TestAuthModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
