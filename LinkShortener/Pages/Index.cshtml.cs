using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LinkShortenerDatabaseLib.Repositories;
using LinkShortenerDatabaseLib.Entities;
using IronBarCode;

namespace LinkShortener.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILinkGenerator _linkGenerator;
        private readonly ILinkRepository _linkRepository;

        public string NewUrl { get; set; } = null!;
        public string QR { get; set; } = null!;

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
                await _linkRepository.AddLinkTransitionByIdAsync(link!.Id);
                return Redirect(link!.OldLink);
            }
            NewUrl = string.Empty;
            return Page();
        }

        public async Task OnPost([FromForm] string? url, [FromForm] int transitions, [FromForm] int? duration, [FromForm(Name = "user_duration")] int? userDuration)
        {
            if(url is null || (duration is null && userDuration is null))
            {
                return;
            }

            if(duration != null && (duration < 1 || duration > 30))
            {
                return;
            }

            if (userDuration != null && (userDuration < 1 || userDuration > 30))
            {
                return;
            }

            if (transitions <= 0 || transitions > 100_000)
            {
                return;
            }

            string generatedLink = _linkGenerator.GenerateLink(url);
            NewUrl = $"{HttpContext.Request.Host}/{generatedLink}";
            double resultDuration = (double)((duration is null) ? userDuration : duration)!;
            Link link = new()
            {
                CreationAt = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(resultDuration),
                NewLink = "/" + generatedLink,
                OldLink = url,
                NumberOfTransitions = 0,
                MaximumTransitionsCount = transitions,
            };
            await _linkRepository.AddLinkAsync(link);

            GeneratedBarcode qr = IronBarCode.BarcodeWriter.CreateBarcode(NewUrl, BarcodeEncoding.QRCode);
            byte[] buffer = qr.ToPngBinaryData();
            var base64 = Convert.ToBase64String(buffer);
            QR = "data:image/png;base64," + base64;
        }
    }
}