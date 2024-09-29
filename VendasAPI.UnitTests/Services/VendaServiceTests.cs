using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using VendasAPI.Domain.Dtos;
using VendasAPI.Domain.Entities;
using VendasAPI.Domain.Repositories;
using VendasAPI.Domain.Services;
using Xunit;

public class VendaServiceTests
{
    private readonly IVendaRepository _vendaRepository;
    private readonly IVendaService _vendaService;
    private readonly ILogger<VendaService> _logger;

    public VendaServiceTests()
    {
        _vendaRepository = Substitute.For<IVendaRepository>();
        _vendaService = new VendaService(_vendaRepository, _logger);
    }

    [Fact]
    public void Create_Should_Add_New_Venda()
    {
        // Arrange
        var vendaDto = new VendaDto
        {
            ClienteId = "123",
            ClienteNome = "Cliente Teste",
            Itens = new List<ItemVendaDto>
            {
                new ItemVendaDto { ProdutoId = "p1", ProdutoNome = "Produto 1", Quantidade = 2, ValorUnitario = 10, Desconto = 0, ValorTotal = 20 }
            }
        };

        // Act
        var venda = _vendaService.Create(vendaDto);

        // Assert
        venda.Should().NotBeNull();
        venda.ClienteId.Should().Be(vendaDto.ClienteId);
        venda.ValorTotal.Should().Be(20);
        _vendaRepository.Received(1).Add(Arg.Any<Venda>());
    }

    [Fact]
    public void Update_Should_Update_Venda()
    {
        // Arrange
        var existingVenda = new Venda { VendaId = 1, ClienteId = "123", ClienteNome = "Cliente Teste", ValorTotal = 20 };
        _vendaRepository.GetById(1).Returns(existingVenda);

        var vendaDto = new VendaDto
        {
            ClienteId = "123",
            ClienteNome = "Cliente Atualizado",
            Itens = new List<ItemVendaDto>()
        };

        // Act
        _vendaService.Update(1, vendaDto);

        // Assert
        existingVenda.ClienteNome.Should().Be("Cliente Atualizado");
        _vendaRepository.Received(1).Update(existingVenda);
    }

    [Fact]
    public void Cancel_Should_Set_Venda_As_Cancelada()
    {
        // Arrange
        var existingVenda = new Venda { VendaId = 1, Cancelado = false };
        _vendaRepository.GetById(1).Returns(existingVenda);

        // Act
        _vendaService.Cancel(1);

        // Assert
        existingVenda.Cancelado.Should().BeTrue();
        _vendaRepository.Received(1).Update(existingVenda);
    }
}
