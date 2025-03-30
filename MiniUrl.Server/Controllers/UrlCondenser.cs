using Microsoft.AspNetCore.Mvc;
using MiniUrl.Dal.Interfaces;
using MiniUrl.Lib;
using MiniUrl.Lib.Requests;

namespace MiniUrl.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UrlCondenserController : ControllerBase {
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IUrlCondenserService _urlCondenserService;
    //public WeatherForecastController(ILogger<WeatherForecastController> logger) {
    //    _logger = logger;
    //}

    public UrlCondenserController(
        ILogger<WeatherForecastController> logger,
        IUrlCondenserService urlCondenserService
    ) {
        _logger = logger;
        _urlCondenserService = urlCondenserService;
    }

    [Route("/minify")]
    [HttpPost]
    public string GetCondensedUrl([FromBody] ShortUrlRequest request) => _urlCondenserService.Condense(request.LongUrl) ?? "";

    [Route("/{shortCode}")]
    [HttpGet]
    public IActionResult RedirectToLongUrl(string shortCode) {
        var result = _urlCondenserService.Get(shortCode);

        return Redirect(result);
    }

    //[Route("/counter")]
    //[HttpGet]
    //public int GetCounter() {
    //    return _urlCondenserService.updateCounter();
    //}

}
