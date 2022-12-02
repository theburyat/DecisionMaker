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

    [HttpGet("cuefa")]
    public CuefaGameResult PlayCuefa([FromBody] Cuefa playerChoice)
    {
        var enemyChoice = (Cuefa)_random.Next(0, 3);
        var gameResult = new CuefaGameResult { EnemyChoice = enemyChoice};
        switch (playerChoice)
        {
            case Cuefa.Paper:
                switch (enemyChoice)
                {
                    case Cuefa.Paper:
                        gameResult.Result = CuefaResult.Draw;
                        break;
                    case Cuefa.Rock:
                        gameResult.Result = CuefaResult.Win;
                        break;
                    case Cuefa.Scissors:
                        gameResult.Result = CuefaResult.Lose;
                        break;
                }
                break;

            case Cuefa.Rock:
                switch (enemyChoice)
                {
                    case Cuefa.Paper:
                        gameResult.Result = CuefaResult.Lose;
                        break;
                    case Cuefa.Rock:
                        gameResult.Result = CuefaResult.Draw;
                        break;
                    case Cuefa.Scissors:
                        gameResult.Result = CuefaResult.Win;
                        break;
                }
                break;
            
            case Cuefa.Scissors:
                switch (enemyChoice)
                {
                    case Cuefa.Paper:
                        gameResult.Result = CuefaResult.Win;
                        break;
                    case Cuefa.Rock:
                        gameResult.Result = CuefaResult.Lose;
                        break;
                    case Cuefa.Scissors:
                        gameResult.Result = CuefaResult.Draw;
                        break;
                }
                break;
        }

        return gameResult;
    }

    [HttpGet("time")]
    public string GetCurrentTime()
    {
        return DateTime.Now.ToString("h:mm:ss tt");
    }
}