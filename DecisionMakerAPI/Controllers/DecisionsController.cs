using Microsoft.AspNetCore.Mvc;
using Models;

namespace DecisionMakerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class DecisionsController : ControllerBase
{
    private readonly ILogger<DecisionsController> _logger;
    private readonly Random _random = new ();

    public DecisionsController(ILogger<DecisionsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("coin")]
    public string FlipCoin()
    {
        return ((Coin)_random.Next(0, 2)).ToString();
    }
}