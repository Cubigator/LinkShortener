using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LinkShortener.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILinkGenerator _linkGenerator;

        public string NewUrl { get; set; } = null!;

        public IndexModel(ILogger<IndexModel> logger,
                          ILinkGenerator linkGenerator)
        {
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        public async Task OnGet()
        {
            NewUrl = string.Empty;
        }

        public async Task OnPost([FromForm] string url)
        {
            NewUrl = _linkGenerator.GenerateLink(url);
        }
    }
}