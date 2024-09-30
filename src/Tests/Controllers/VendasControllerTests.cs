using Bogus;
using Domain;
using NSubstitute;
using API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Domain.Repositorios.Interfaces;
using NSubstitute.ExceptionExtensions;

public class VendasControllerTests
{
    private readonly IVendaRepository _vendaRepository;
    private readonly VendasController _controller;

    public VendasControllerTests()
    {
        _vendaRepository = Substitute.For<IVendaRepository>();
        _controller = new VendasController(_vendaRepository);
    }

    [Fact]
    public async Task Get_DeveRetornarListaDeVendas_QuandoExistiremVendas()
    {
        // Arrange
        var vendasFalsas = new Faker<Venda>()
            .RuleFor(v => v.Id, f => f.Random.Guid())
            .RuleFor(v => v.NumeroVenda, f => f.Commerce.Ean13())
            .RuleFor(v => v.DataVenda, f => f.Date.Past())
            .RuleFor(v => v.ValorTotal, f => f.Finance.Amount())
            .Generate(3);

        _vendaRepository.ObterTodasAsync().Returns(Task.FromResult(vendasFalsas));

        // Act
        var resultado = await _controller.Get();

        // Assert
        var okResult = resultado as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        var vendas = okResult.Value as List<Venda>;
        vendas.Should().HaveCount(3);
    }

    [Fact]
    public async Task Get_DeveRetornar500_QuandoOcorrerErro()
    {
        // Arrange
        _vendaRepository.ObterTodasAsync().Throws(new Exception("Erro ao acessar banco de dados"));

        // Act
        var resultado = await _controller.Get();

        // Assert
        var result = resultado as ObjectResult;
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task GetById_DeveRetornarVenda_QuandoIdExistir()
    {
        // Arrange
        var vendaFalsa = new Faker<Venda>()
            .RuleFor(v => v.Id, f => f.Random.Guid())
            .RuleFor(v => v.NumeroVenda, f => f.Commerce.Ean13())
            .RuleFor(v => v.DataVenda, f => f.Date.Past())
            .Generate();

        _vendaRepository.ObterPorIdAsync(vendaFalsa.Id).Returns(Task.FromResult(vendaFalsa));

        // Act
        var resultado = await _controller.Get(vendaFalsa.Id);

        // Assert
        var okResult = resultado as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        var venda = okResult.Value as Venda;
        venda.Should().NotBeNull();
        venda.Id.Should().Be(vendaFalsa.Id);
    }

    [Fact]
    public async Task GetById_DeveRetornar404_QuandoIdNaoExistir()
    {
        // Arrange
        var idInvalido = Guid.NewGuid();
        _vendaRepository.ObterPorIdAsync(idInvalido).Returns(Task.FromResult<Venda>(null));

        // Act
        var resultado = await _controller.Get(idInvalido);

        // Assert
        var notFoundResult = resultado as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Post_DeveRetornarVendaCriada_QuandoForValida()
    {
        // Arrange
        var novaVenda = new Faker<Venda>()
            .RuleFor(v => v.NumeroVenda, f => f.Commerce.Ean13())
            .RuleFor(v => v.DataVenda, f => f.Date.Recent())
            .Generate();

        _vendaRepository.AdicionarAsync(novaVenda).Returns(Task.CompletedTask);

        // Act
        var resultado = await _controller.Post(novaVenda);

        // Assert
        var createdResult = resultado as CreatedAtActionResult;
        createdResult.Should().NotBeNull();
        createdResult.StatusCode.Should().Be(201);
        var venda = createdResult.Value as Venda;
        venda.Should().NotBeNull();
        venda.NumeroVenda.Should().Be(novaVenda.NumeroVenda);
    }

    [Fact]
    public async Task Post_DeveRetornarBadRequest_QuandoVendaForNula()
    {
        // Act
        var resultado = await _controller.Post(null);

        // Assert
        var badRequestResult = resultado as BadRequestResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Put_DeveRetornarNoContent_QuandoAtualizacaoForValida()
    {
        // Arrange
        var vendaAtualizada = new Faker<Venda>()
            .RuleFor(v => v.Id, f => f.Random.Guid())
            .RuleFor(v => v.NumeroVenda, f => f.Commerce.Ean13())
            .Generate();

        _vendaRepository.AtualizarAsync(vendaAtualizada).Returns(Task.CompletedTask);

        // Act
        var resultado = await _controller.Put(vendaAtualizada.Id, vendaAtualizada);

        // Assert
        var noContentResult = resultado as NoContentResult;
        noContentResult.Should().NotBeNull();
        noContentResult.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task Put_DeveRetornarBadRequest_QuandoIdsNaoCorrespondem()
    {
        // Arrange
        var venda = new Faker<Venda>().Generate();
        var idDiferente = Guid.NewGuid();

        // Act
        var resultado = await _controller.Put(idDiferente, venda);

        // Assert
        var badRequestResult = resultado as BadRequestResult;
        badRequestResult.Should().NotBeNull();
        badRequestResult.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Delete_DeveRetornarNoContent_QuandoVendaForDeletada()
    {
        // Arrange
        var idVenda = Guid.NewGuid();
        _vendaRepository.ObterPorIdAsync(idVenda).Returns(new Faker<Venda>().Generate());

        // Act
        var resultado = await _controller.Delete(idVenda);

        // Assert
        var noContentResult = resultado as NoContentResult;
        noContentResult.Should().NotBeNull();
        noContentResult.StatusCode.Should().Be(204);
    }

    [Fact]
    public async Task Delete_DeveRetornarNotFound_QuandoVendaNaoExistir()
    {
        // Arrange
        var idInvalido = Guid.NewGuid();
        _vendaRepository.ObterPorIdAsync(idInvalido).Returns(Task.FromResult<Venda>(null));

        // Act
        var resultado = await _controller.Delete(idInvalido);

        // Assert
        var notFoundResult = resultado as NotFoundResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult.StatusCode.Should().Be(404);
    }
}
