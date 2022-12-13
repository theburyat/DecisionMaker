using DecisionMakerAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Xunit;

namespace DecisionMakerAPITests;

public class DecisionMakerApiTests
{
    private readonly Mock<ILogger<DecisionsController>> _logger = new ();

    [Fact]
    public void FlipCoin_Should_BeOk()
    {
        // arrange
        var controller = new DecisionsController(_logger.Object);
        
        // act
        Coin? coin = Enum.TryParse<Coin>(controller.FlipCoin(), out var coinEnum) ? coinEnum: null;
        
        // assert
        coin.Should().NotBeNull().And.BeOneOf(Coin.Eagle, Coin.Tails);
    }

    [Theory]
    [InlineData(Cuefa.Paper)]
    [InlineData(Cuefa.Scissors)]
    [InlineData(Cuefa.Rock)]
    public void PlayCuefa_Should_BeOk(Cuefa playerChoice)
    {
        // arrange
        var controller = new DecisionsController(_logger.Object);

        // act
        var gameResult = controller.PlayCuefa(playerChoice);
        
        // assert
        gameResult.Should().NotBeNull();
        gameResult.Result.Should().BeOneOf(CuefaResult.Draw, CuefaResult.Lose, CuefaResult.Win);
        gameResult.EnemyChoice.Should().BeOneOf(Cuefa.Rock, Cuefa.Paper, Cuefa.Scissors);
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(10, 10)]
    public void GetRandom_Should_BeOk(int minValue, int maxValue)
    {
        // arrange
        var controller = new DecisionsController(_logger.Object);

        // act
        var result = controller.GetRandomNumber(minValue, maxValue);
        
        // assert
        result.Should().NotBeNull();
        ((OkObjectResult)result.Result!).Value.Should().BeOfType<int>();
        ((int)((OkObjectResult)result.Result!).Value!).Should().BeInRange(minValue, maxValue);
    }
    
    [Fact]
    public void GetRandom_Should_ThrowExceptionWithBadInterval()
    {
        // arrange
        var controller = new DecisionsController(_logger.Object);

        // act
        var result = controller.GetRandomNumber(20, 10);
        
        // assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeOfType<string>().And.Be("Bad interval");
    }
    
    [Fact]
    public void GetRandom_Should_ThrowExceptionWithBadMaxValue()
    {
        // arrange
        var controller = new DecisionsController(_logger.Object);

        // act
        var result = controller.GetRandomNumber(20, int.MaxValue);
        
        // assert
        result.Should().NotBeNull();
        result.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeOfType<string>().And.Be("To big max value");
    }
}