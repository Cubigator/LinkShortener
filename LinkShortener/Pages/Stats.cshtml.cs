using LinkShortenerDatabaseLib.Entities;
using LinkShortenerDatabaseLib.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Pages;

public class StatsModel : PageModel
{
    private readonly ILinkRepository _linkRepository;

    public int NumberOfTransitions;

    public StatsModel(ILinkRepository linkRepository)
    {
        _linkRepository = linkRepository;
    }
    public void OnGet()
    {

    }

    public async Task OnPost(string shortUrl)
    {
        if (shortUrl is null)
            return;

        int slashIndex = shortUrl.LastIndexOf("/");

        Link? link = (slashIndex == -1) ? 
            (await _linkRepository.GetByNewLinkAsync("/" + shortUrl)) : 
            (await _linkRepository.GetByNewLinkAsync(shortUrl[slashIndex..]));

        if(link is null)
        {
            ModelState.TryAddModelError("notFound", "Такой ссылки не существует");
            return;
        }
        NumberOfTransitions = link!.NumberOfTransitions;
    }
}
