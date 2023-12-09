using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LinkShortenerDatabaseLib.Repositories;
using LinkShortenerDatabaseLib.Entities;

namespace LinkShortener.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILinkGenerator _linkGenerator;
        private readonly ILinkRepository _linkRepository;

        public string NewUrl { get; set; } = null!;

        public IndexModel(ILogger<IndexModel> logger,
                          ILinkGenerator linkGenerator,
                          ILinkRepository linkRepository)
        {
            _logger = logger;
            _linkGenerator = linkGenerator;
            _linkRepository = linkRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            string inputPath = HttpContext.Request.Path;
            if(inputPath != "/")
            {
                Link? link = await _linkRepository.GetByNewLinkAsync(inputPath);
                if(link is null)
                {
                    return NotFound();
                }
                return Redirect(link!.OldLink);
            }
            NewUrl = string.Empty;
            return Page();
        }

        public async Task OnPost([FromForm] string url)
        {
            string generatedLink = _linkGenerator.GenerateLink(url);
            NewUrl = $"{HttpContext.Request.Host}/{generatedLink}";
            Link link = new()
            {
                CreationAt = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(3),
                NewLink = "/" + generatedLink,
                OldLink = url,
                NumberOfTransitions = 0
            };
            await _linkRepository.AddLinkAsync(link);
        }
    }
}