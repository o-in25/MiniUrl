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
    public Transaction<string> GetCondensedUrl([FromBody] ShortUrlRequest request) => _urlCondenserService.Condense(request.LongUrl, request?.CustomShortUrl);

    [Route("/{shortUrl}")]
    [HttpGet]
    public IActionResult RediectFrom(string shortUrl) {
        var result = _urlCondenserService.Get(shortUrl);
        if(result.HasError) {
            return NotFound("No URL found for entry");
        }

        return Redirect(result.Data);
    }

    [Route("/{shortUrl}/count")]
    [HttpGet]
    public IActionResult GetRetrievalCount(string shortUrl) {
        var result = _urlCondenserService.GetRetrievalCount(shortUrl);
        if(result.HasError) {
            return NotFound(result.StatusMessage);
        }

        return Ok(result.Data);
    }

    //[Route("/counter")]
    //[HttpGet]
    //public int GetCounter() {
    //    return _urlCondenserService.updateCounter();
    //}

}
